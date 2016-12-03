using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyUI : MonoBehaviour {
    public GameObject target;
    public GameObject from;
    public Slider hpSlider;
    public Vector2 offsetPos;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tankHealth targetHealth = target.GetComponent<tankHealth>();
        if (!targetHealth)
            return;
        hpSlider.value = targetHealth.HPpercent();

        Vector3 tarPos;
        tarPos = target.transform.position + new Vector3(offsetPos.x, offsetPos.y, 0f);
        Vector3 lookAtSame = Camera.main.transform.TransformDirection(tarPos - from.transform.position);
        
        if (hpSlider.value <= 0f || lookAtSame.z >= 0f)
            hpSlider.gameObject.SetActive(false);
        else
            hpSlider.gameObject.SetActive(true);

        Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, tarPos);
        hpSlider.transform.position = pos;
    }
}
