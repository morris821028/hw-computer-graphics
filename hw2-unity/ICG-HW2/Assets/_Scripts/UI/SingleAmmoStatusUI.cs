using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SingleAmmoStatusUI : MonoBehaviour
{
    public GameObject target = null;
    public int mAmmoIndex = 0;
    private Text mAmmoAmount;
    private tankShoot mTankShoot = null;
    // Use this for initialization
    void Start()
    {
        mAmmoAmount = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mTankShoot == null)
            mTankShoot = target.GetComponent<tankShoot>();
        else
            mAmmoAmount.text = "" + mTankShoot.mAmmoAmount[mAmmoIndex];
    }
}
