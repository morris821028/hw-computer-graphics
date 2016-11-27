using UnityEngine;
using System.Collections;

public class trackMove : MonoBehaviour
{

    public float mSpeed = 1;
    public float rSpeed = 1;

    float towerSpeed = 1;

    public GameObject barrel;// 砲管
    float barrelSpeed = 30;
    float maxAngle = 80;
    float minAngle = -5;
    float angle;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("UnitySelectMonitor", 1);
        // 獲取水平軸向按鍵
        float h = Input.GetAxis("Horizontal");
        // 獲取垂直軸向按鍵
        float v = Input.GetAxis("Vertical");
        // 用滑鼠移動控制砲台方向
        float x = Input.GetAxis("Mouse X");
        this.transform.Rotate(0, 0, towerSpeed * x);
        // 得到砲管角度
        angle += Input.GetAxis("Mouse ScrollWheel") * barrelSpeed;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        Vector3 temp = barrel.transform.localEulerAngles;
        temp.x = angle;
        barrel.transform.localEulerAngles = temp;//上下旋轉砲管
    }
}
