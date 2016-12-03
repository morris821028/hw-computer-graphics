using UnityEngine;
using System.Collections;

public class mineEffect : MonoBehaviour
{

    // Use this for initialization
    public GameObject mCountdownExpolsion;
    public GameObject mMineEffect;

    private float effectTime;
    private GameObject mMineEffectInst;
    void Start()
    {
        effectTime = Time.time;
        mMineEffectInst = Instantiate(mMineEffect, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        mMineEffectInst.transform.localPosition = new Vector3(0f, 0f, 0f);
        mMineEffectInst.transform.position = gameObject.transform.position;
        mMineEffectInst.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - effectTime > 2)
        {
            effectTime = Time.time;
            Destroy(mMineEffectInst);
            mMineEffectInst = Instantiate(mMineEffect, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            mMineEffectInst.transform.localPosition = new Vector3(0f, 0f, 0f);
            mMineEffectInst.transform.position = gameObject.transform.position;
            mMineEffectInst.transform.parent = gameObject.transform;
        }
    }
    void OnDestroy()
    {
        Destroy(mMineEffectInst);
    }
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(mCountdownExpolsion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
