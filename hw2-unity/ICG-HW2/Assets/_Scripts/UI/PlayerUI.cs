using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public GameObject target;
    public Slider hpSlider;
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
    }
}
