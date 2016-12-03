using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FastAmmoAmountUI : MonoBehaviour
{
    public GameObject target = null;
    private Text mAmmoAmount;
    private tankShoot mTankShoot = null;
    // Use this for initialization
    void Start()
    {
        mAmmoAmount = GetComponentInChildren<Text>();
        mTankShoot = target.GetComponent<tankShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        mAmmoAmount.text = "" + mTankShoot.mAmmoAmount[0];
    }
}
