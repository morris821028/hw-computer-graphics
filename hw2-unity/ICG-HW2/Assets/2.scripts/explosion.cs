using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour
{
    // 特效
    public GameObject effect;

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
        else
        {
            Instantiate(effect, transform.position, transform.rotation);
            // 刪除砲彈
            Destroy(gameObject);
        }
    }
}
