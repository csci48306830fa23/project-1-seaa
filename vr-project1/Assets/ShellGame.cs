using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShellGame : MonoBehaviour
{
    [SerializeField]
    GameObject[] cups;
    [SerializeField]
    GameObject[] cupPositions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // unity default modulo can suck my butt
    // nvm it doesn't actually account for modulo'ing negatives. whatever too lazy to change things back
    private int mod(int a, int b)
    {
        return a - (int)Mathf.Floor(a / b) * b;
    }

    public void shuffle()
    {
        // swap two cups
        // assuming nothing breaks, cups.length should always be 3 lol
        // c = a, a = b, b, b = c
        int pivot = Random.Range(0, cups.Length);

        GameObject a = cups[mod((pivot - 1) + 3, cups.Length)];
        GameObject b = cups[mod((pivot + 1) + 3, cups.Length)];

        // swap physical positions of the two cups
        // todo - tween positions of the cups so it's not instant
        a.transform.position = cupPositions[mod((pivot + 1) + 3, cups.Length)].transform.position;
        b.transform.position = cupPositions[mod((pivot - 1) + 3, cups.Length)].transform.position;
        
        // swap positions of the cups in array
        GameObject c = a;
        cups[mod((pivot - 1)+3, cups.Length)] = b;
        cups[mod((pivot + 1)+3, cups.Length)] = c;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            shuffle();
        }
    }
}
