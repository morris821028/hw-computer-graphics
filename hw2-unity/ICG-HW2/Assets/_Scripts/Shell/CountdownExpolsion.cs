using UnityEngine;
using System.Collections;

public class CountdownExpolsion : MonoBehaviour {

    // 特效
    public int mCountTime = 20;
    public GameObject mExplosionEffect;
    public GameObject mMineEffect;
    public LayerMask mTankMask;
    public float mExplosionRadius = 50f;
    public float mExplosionForce = 1e+6f;

    private float countTime;
    private float effectTime;
    private GameObject mMineEffectInst;
    void Start()
    {
        countTime = Time.time;
        effectTime = Time.time;
    }
    
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
        if (Time.time - countTime > mCountTime)
        {
            Instantiate(mExplosionEffect, transform.position, transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(transform.position, mExplosionRadius, mTankMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
                if (!targetRigidbody)
                    continue;
                targetRigidbody.AddExplosionForce(mExplosionForce, transform.position, mExplosionRadius);

                tankHealth targetHealth = targetRigidbody.GetComponent<tankHealth>();
                if (!targetHealth)
                    continue;
                float damage = 5f;

                targetHealth.TakeDamage(damage);
            }

            DestroyObject(gameObject);
        }
    }
    void OnDestroy()
    {
        Destroy(mMineEffectInst);
    }
    void OnCollisionEnter(Collision collision)
    {
       
    }
}
