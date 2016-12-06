using UnityEngine;
using System.Collections;

public class tankShoot : MonoBehaviour
{
    // public
    public GameObject mBarrelObject;
    public GameObject mFirePoint;
    public float mBarrelSpeed = 30f;
    public float mBarrelMaxAngle = 80f;
    public float mBarrelMinAngle = -5f;
    public float mTowerSpeed = 6;
    public float mExplosionRadius = 100f;
    public float mExplosionForce = 1e+20f;

    public int[] mAmmoAmount;
    // private
    private Rigidbody[] mShells;
    private GameObject[] mShellObjects;
    private float mBarrelAngle = 0f;
    private string mMovementAxisName;
    private string mBarrelAngleAxisName;
    private string mAmmoInputPrefixName;
    private string mFireButton;

    private float mMovementInputValue;
    private float mTurnInputValue;
    private int mAmmoSelectIndex;

    private float mCurrentLaunchForce = 100f;
    private bool mFired;
    // Use this for initialization
    void Start()
    {
        mMovementAxisName = "Mouse X";
        mBarrelAngleAxisName = "Mouse ScrollWheel";
        mAmmoInputPrefixName = "AmmoType";
        mFireButton = "Fire1";
        mShellObjects = Resources.LoadAll<GameObject>("Shell");

        mShells = new Rigidbody[mShellObjects.Length];
        mAmmoAmount = new int[mShellObjects.Length];
        for (int i = 0; i < mShells.Length; i++)
        {
            mShells[i] = mShellObjects[i].GetComponent<Rigidbody>();
            mAmmoAmount[i] = 30;
        }
        mAmmoSelectIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 用滑鼠移動控制砲台方向
        mMovementInputValue = Input.GetAxis(mMovementAxisName);
        // 得到砲管角度
        mTurnInputValue = Input.GetAxis(mBarrelAngleAxisName);

        if (Input.GetButtonDown(mFireButton))
        {
            mFired = false;
        } else if (Input.GetButtonUp(mFireButton) && !mFired)
        {
            Fire();
        }
        for (int i = 0; i < mShells.Length; i++)
        {
            if (Input.GetButtonDown(mAmmoInputPrefixName+i))
            {
                mAmmoSelectIndex = i;
            }
        }
    }
    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        this.transform.Rotate(0, 0, mTowerSpeed * mMovementInputValue);
    }

    private void Turn()
    {
        mBarrelAngle += mTurnInputValue * mBarrelSpeed;
        mBarrelAngle = Mathf.Clamp(mBarrelAngle, mBarrelMinAngle, mBarrelMaxAngle);
        Vector3 temp = mBarrelObject.transform.localEulerAngles;
        temp.x = mBarrelAngle;
        mBarrelObject.transform.localEulerAngles = temp;//上下旋轉砲管
    }

    private void Fire()
    {
        mFired = true;
        int t = mAmmoSelectIndex;
        if (mAmmoAmount[t] <= 0)
            return;
        Rigidbody shellInstance = (Rigidbody)
                Instantiate(mShells[t], mFirePoint.transform.position, mFirePoint.transform.rotation);
        shellInstance.transform.rotation = Quaternion.LookRotation(mFirePoint.transform.TransformDirection(new Vector3(0, 1, 0)));
        mAmmoAmount[t]--;
        shellInstance.velocity = mCurrentLaunchForce * mFirePoint.transform.TransformDirection(new Vector3(0, 1, 0));
        Physics.IgnoreCollision(mFirePoint.transform.root.GetComponent<Collider>(), shellInstance.GetComponent<Collider>());
    }
}
