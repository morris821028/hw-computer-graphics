using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour
{
    public GameObject effect;//特效

    void Start()
    {
    }
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        // 碰撞發生時呼叫
        // 碰撞後產生爆炸

        if (collision.gameObject.tag == "enemy")
        {
            // 當撞到的 collider 具有 enemy tag 時
            Instantiate(effect, transform.position, transform.rotation);
            // 刪除砲彈
            Destroy(gameObject);
        }
    }
}

