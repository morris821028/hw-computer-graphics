using UnityEngine;
using System.Collections;

public class DisplayScript : MonoBehaviour
{
    public GameObject mFirstPlayerCamera;
    public GameObject mThirdPlayerCamera;

    private bool mToggleFlag;
    void Awake()
    {
        // 預設先開啟第一部攝影機
        mToggleFlag = true;
        mThirdPlayerCamera.SetActive(!mToggleFlag);
        mFirstPlayerCamera.SetActive(mToggleFlag);
    }

    void Update()
    {
        /*
        Debug.Log("Toogle " + mToggleFlag);
        Debug.Log("Press " + Input.GetButtonDown("Camera"));
        */
        if (Input.GetButtonDown("Camera") == true)
        {
            mToggleFlag = !mToggleFlag;
            mThirdPlayerCamera.SetActive(!mToggleFlag);
            mFirstPlayerCamera.SetActive(mToggleFlag);
        }
    }
}
