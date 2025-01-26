using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTes : MonoBehaviour {

    [SerializeField] private int TimerTest;
    [SerializeField] private GameObject show1;
    [SerializeField] private GameObject show2;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(CountdownTime());
    }

    public IEnumerator CountdownTime() {
        TimerTest--;
        yield return new WaitForSeconds(1);
        if (TimerTest == 0)
        {
            show1.SetActive(true);
            show2.SetActive(true) ;
            StopCoroutine(CountdownTime() );
        }
        StartCoroutine(CountdownTime() );
    }

}
