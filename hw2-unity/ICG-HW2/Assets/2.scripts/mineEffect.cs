using UnityEngine;
using System.Collections;

public class mineEffect : MonoBehaviour {

    // Use this for initialization
    public int mCountTime = 5;
    public GameObject mEffect;
    private float countTime;
    void Start () {
        countTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - countTime > mCountTime)
        {
            Instantiate(mEffect, transform.position, transform.rotation);
            DestroyObject(gameObject);
        }
    }
}
