using UnityEngine;
using System.Collections;

public class AmmoStatusUI : MonoBehaviour
{
    public GameObject mAmmoUI;
    public GameObject mTarget;
    // Use this for initialization
    void Start()
    {
        tankShoot shootStatus = mTarget.GetComponent<tankShoot>();

        Vector3 pos = new Vector3(100, 60, 0);
        for (int i = 0; i < shootStatus.mShellObjects.Length; i++)
        {
            GameObject ui = (GameObject) Instantiate(mAmmoUI, pos, Quaternion.identity);
            SingleAmmoStatusUI uiScript = ui.GetComponent<SingleAmmoStatusUI>();
            uiScript.mAmmoIndex = i;
            uiScript.target = mTarget;
            ui.transform.SetParent(transform);
            pos = pos + new Vector3(50, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
