using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour
{
    // 特效
    public GameObject effect;
    public LayerMask mTankMask;
    public float mExplosionRadius = 20f;
    public float mExplosionForce = 1e+20f;

    void Start()
    {

    }
    void Update()
    {

    }
    void OnDestroy()
    {
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
            float damage = 10f;
            
            targetHealth.TakeDamage(damage);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        // 碰撞發生時呼叫
        // 碰撞後產生爆炸

        // if (collision.gameObject.tag == "enemy")
        Instantiate(effect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
