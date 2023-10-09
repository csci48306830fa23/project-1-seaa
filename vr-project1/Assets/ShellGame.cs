/*
 * todo:
 * - fix cups not being lifted up for long enough
 * - add reset button
 * - disable start button at score being 3
 * - disable cup vrmoveable during shuffle
 * */

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using VelUtils;
using VelUtils.VRInteraction;

public class ShellGame : MonoBehaviour
{
    [SerializeField]
    GameObject[] cups;
    [SerializeField]
    GameObject[] cupPositions;
    [SerializeField]
    GameObject[] ballPositions;
    [SerializeField]
    GameObject ball;

    //[SerializeField]
    //Cup cupObj;

    [SerializeField]
    AudioClip winSound;
    [SerializeField]
    AudioClip lossSound;
    [SerializeField]
    AudioClip shuffleSound;
    [SerializeField]
    AudioClip liftSound;
    [SerializeField]
    Transform soundSpawnLocation;

    [SerializeField]
    TMP_Text scoreText;
    [SerializeField]
    Button startButton;

    [SerializeField]
    GameObject tweenTimer; // bro......................

    public int curBallPos = 0;
    float switchSpeed = 0.5f;
    bool currentlyShuffling = false;

    int score = 0;

    bool afterShuffle = false;

    bool tokenWin = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cups.Length; i++)
        {
            cups[i].transform.position = cupPositions[i].transform.position;
        }
        curBallPos = Random.Range(0, cups.Length);
        ball.transform.position = ballPositions[curBallPos].transform.position; 
    }

    // unity default modulo can suck my butt
    // nvm it doesn't actually account for modulo'ing negatives. whatever too lazy to change things back
    private int mod(int a, int b)
    {
        return a - (int)Mathf.Floor(a / b) * b;
    }

    private void shuffle()
    {
        // swap two cups
        // assuming nothing breaks, cups.length should always be 3 lol
        // c = a, a = b, b, b = c
        int pivot = Random.Range(0, cups.Length);

        GameObject a = cups[mod((pivot - 1) + 3, cups.Length)];
        GameObject b = cups[mod((pivot + 1) + 3, cups.Length)];

        // swap physical positions of the two cups
        a.transform.DOMove(cupPositions[mod((pivot + 1) + 3, cups.Length)].transform.position, switchSpeed);
        b.transform.DOMove(cupPositions[mod((pivot - 1) + 3, cups.Length)].transform.position, switchSpeed);

        AudioSource.PlayClipAtPoint(shuffleSound, soundSpawnLocation.position);

        // swap positions of the cups in array
        GameObject c = a;
        cups[mod((pivot - 1)+3, cups.Length)] = b;
        cups[mod((pivot + 1)+3, cups.Length)] = c;

        // handling ball stuff
        if (curBallPos == mod((pivot - 1) + 3, cups.Length))
        {
            ball.transform.DOMove(ballPositions[mod((pivot + 1) + 3, cups.Length)].transform.position, switchSpeed);
            curBallPos = mod((pivot + 1) + 3, cups.Length);
        } else if (curBallPos == mod((pivot + 1) + 3, cups.Length))
        {
            ball.transform.DOMove(ballPositions[mod((pivot - 1) + 3, cups.Length)].transform.position, switchSpeed);
            curBallPos = mod((pivot - 1) + 3, cups.Length);
        }
    }

    public IEnumerator fullShuffle()
    {
        currentlyShuffling = true;

        // sequence of showing the ball to the player
        for (int i = 0; i < cups.Length; i++)
        {
            cups[i].GetComponent<VRMoveable>().enabled = false;

            cups[i].transform.DOMoveX(cupPositions[i].transform.position.x, 1f);
            cups[i].transform.DOMoveZ(cupPositions[i].transform.position.z, 1f);
            cups[i].transform.DOMoveY(cupPositions[i].transform.position.y + 0.5f, 1f);

            cups[i].transform.eulerAngles = (new Vector3(-90, 0, 0));
            cups[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            cups[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            cups[i].GetComponent<Rigidbody>().useGravity = false;
        }
        curBallPos = Random.Range(0, cups.Length);
        ball.transform.DOMove(ballPositions[curBallPos].transform.position, 1f);
        AudioSource.PlayClipAtPoint(liftSound, soundSpawnLocation.position);

        Tween tt = tweenTimer.transform.DOMoveX(1f, 2f); // lol??
        yield return tt.WaitForCompletion();

        for (int i = 0; i < cups.Length; i++)
        {
            cups[i].transform.DOMoveY(cups[i].transform.position.y-0.5f, 1f);
            cups[i].transform.eulerAngles = (new Vector3(-90, 0, 0)); // just in case LOL
            cups[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            cups[i].GetComponent<Rigidbody>().useGravity = true;
        }
        AudioSource.PlayClipAtPoint(liftSound, soundSpawnLocation.position);

        tt = tweenTimer.transform.DOMoveX(1f, 1.5f);
        yield return tt.WaitForCompletion();

        // start shuffling
        int shuffleTimes = Random.Range(10, 16);
        while (shuffleTimes > 0)
        {
            shuffle();
            shuffleTimes--;
            yield return new WaitForSeconds(switchSpeed);
        }

        for (int i = 0; i < cups.Length; i++)
        {
            cups[i].GetComponent<Cup>().cupNum = i; // what
            cups[i].GetComponent<VRMoveable>().enabled = true;
        }
        currentlyShuffling = false;
        afterShuffle = true;
    }

    public void win()
    {
        if (afterShuffle)
        {
            AudioSource.PlayClipAtPoint(winSound, soundSpawnLocation.position);
            score += 1;
            scoreText.text = "Score: " + score;
            switchSpeed -= 0.07f;

            if (score >= 3)
            {
                startButton.interactable = false;
                if (!tokenWin)
                {
                    spawnToken();
                    tokenWin = true;
                }
            }
        }
        afterShuffle = false;
    }

    public void loss(int cupNum)
    {
        if (afterShuffle)
        {
            AudioSource.PlayClipAtPoint(lossSound, soundSpawnLocation.position);
            GameObject a = cups[mod((cupNum - 1) + 3, cups.Length)];
            GameObject b = cups[mod((cupNum + 1) + 3, cups.Length)];

            StartCoroutine(liftCups(a, b));
        }
        afterShuffle = false;
    }

    private IEnumerator liftCups(GameObject a, GameObject b)
    {
        currentlyShuffling = true;
        a.transform.DOMoveY(a.transform.position.y + 0.5f, 1f);
        b.transform.DOMoveY(b.transform.position.y + 0.5f, 1f);
        a.GetComponent<Rigidbody>().useGravity = false;
        b.GetComponent<Rigidbody>().useGravity = false;
        AudioSource.PlayClipAtPoint(liftSound, soundSpawnLocation.position);

        Tween tt = tweenTimer.transform.DOMoveX(1f, 3f); // lol??
        yield return tt.WaitForCompletion();

        a.transform.DOMoveY(a.transform.position.y - 0.5f, 1f);
        b.transform.DOMoveY(b.transform.position.y - 0.5f, 1f);
        a.GetComponent<Rigidbody>().useGravity = true;
        b.GetComponent<Rigidbody>().useGravity = true;
        AudioSource.PlayClipAtPoint(liftSound, soundSpawnLocation.position);
        currentlyShuffling = false;
    }

    // lol can't call a coroutine from onClick
    public void startShuffle()
    {
        if (!currentlyShuffling) 
        { 
            StartCoroutine(fullShuffle());
        }
    }

    public void resetGame()
    {
        if (!currentlyShuffling)
        {
            score = 0;
            scoreText.text = "Score: " + score;
            switchSpeed = 0.5f;

            for (int i = 0; i < cups.Length; i++)
            {
                cups[i].transform.position = cupPositions[i].transform.position;
                cups[i].transform.eulerAngles = (new Vector3(-90, 0, 0));
                cups[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                cups[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            }
            curBallPos = Random.Range(0, cups.Length);
            ball.transform.position = ballPositions[curBallPos].transform.position;
            startButton.interactable = true;
        }
    }

    // for later, idk lol
    public void spawnToken()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("VR_Button2_Right") && !currentlyShuffling)
        {
            StartCoroutine(fullShuffle());
        }
    }
}
