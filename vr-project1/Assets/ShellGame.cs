/*
 * todo:
 * - when grabbing cup, lift the other two cups
 * - reset rotation of cups when restarting the game
 * - just a general "restart game" thing
 * - correct cup detection
 * - scoring system (get 3 correct?)
 * */

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using VelUtils;

public class ShellGame : MonoBehaviour
{
    [SerializeField]
    GameObject[] cups;
    [SerializeField]
    GameObject[] cupPositions;
    [SerializeField]
    GameObject ball;

    //[SerializeField]
    //Cup cupObj;

    [SerializeField]
    AudioClip winSound;
    [SerializeField]
    AudioClip lossSound;

    public int curBallPos = 0;
    float switchSpeed = 0.5f;
    bool currentlyShuffling = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cups.Length; i++)
        {
            cups[i].transform.position = cupPositions[i].transform.position;
        }
        curBallPos = Random.Range(0, cups.Length);
        ball.transform.position = cupPositions[curBallPos].transform.position; 
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
        
        // swap positions of the cups in array
        GameObject c = a;
        cups[mod((pivot - 1)+3, cups.Length)] = b;
        cups[mod((pivot + 1)+3, cups.Length)] = c;

        // handling ball stuff
        if (curBallPos == mod((pivot - 1) + 3, cups.Length))
        {
            ball.transform.DOMove(cupPositions[mod((pivot + 1) + 3, cups.Length)].transform.position, switchSpeed);
            curBallPos = mod((pivot + 1) + 3, cups.Length);
        } else if (curBallPos == mod((pivot + 1) + 3, cups.Length))
        {
            ball.transform.DOMove(cupPositions[mod((pivot - 1) + 3, cups.Length)].transform.position, switchSpeed);
            curBallPos = mod((pivot - 1) + 3, cups.Length);
        }
    }

    private IEnumerator fullShuffle()
    {
        currentlyShuffling = true;
        int shuffleTimes = 10;
        while (shuffleTimes > 0)
        {
            shuffle();
            shuffleTimes--;
            yield return new WaitForSeconds(switchSpeed);
        }
        currentlyShuffling = false;

        for (int i = 0; i < cups.Length; i++)
        {
            cups[i].GetComponent<Cup>().cupNum = i; // what
        }
    }

    public void win()
    {
        AudioSource.PlayClipAtPoint(winSound, this.transform.position);
    }

    public void loss()
    {
        AudioSource.PlayClipAtPoint(lossSound, this.transform.position);
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
