using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TestUiHP : MonoBehaviour {
    public GameObject target;
    public Slider hpSlider;
    public Vector2 offsetPos;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        tankHealth targetHealth = target.GetComponent<tankHealth>();
        if (!targetHealth)
            return;
        hpSlider.value = targetHealth.HPpercent();

        if (hpSlider.value <= 0f)
            hpSlider.gameObject.SetActive(false);
        else
            hpSlider.gameObject.SetActive(true);
        
        Vector3 tarPos;
        tarPos = target.transform.position;
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, tarPos);
        hpSlider.transform.position = pos + offsetPos;
	}
}
