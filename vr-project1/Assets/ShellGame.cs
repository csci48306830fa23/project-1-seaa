using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ShellGame : MonoBehaviour
{
    [SerializeField]
    GameObject[] cups;
    [SerializeField]
    GameObject[] cupPositions;
    [SerializeField]
    GameObject ball;

    int curBallPos = 0;
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
            yield return new WaitForSeconds(0.5f);
        }
        currentlyShuffling = false;
    }

    /*private IEnumerator moveCups(GameObject cup1, GameObject cup2, Transform pos1, Transform pos2)
    {
        float xpos1 = cup1.transform.position.x;
        var tweener = DOTween.To(() => xpos1, k => xpos1 = k, pos2.position.x, 1f)
            .OnUpdate(() => cup1.transform.position = new Vector3(xpos1, cup1.transform.position.y, cup1.transform.position.z));
        float xpos2 = cup1.transform.position.x;
        var tweener2 = DOTween.To(() => xpos2, k => xpos2 = k, pos1.position.x, 1f)
            .OnUpdate(() => cup2.transform.position = new Vector3(xpos2, cup2.transform.position.y, cup2.transform.position.z));

        while (tweener.IsActive() || tweener2.IsActive()) { yield return null; }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !currentlyShuffling)
        {
            StartCoroutine(fullShuffle());
        }
    }
}
