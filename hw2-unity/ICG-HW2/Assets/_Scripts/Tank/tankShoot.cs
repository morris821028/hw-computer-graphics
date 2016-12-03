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

    // private
    private Rigidbody[] mShells;
    private float mBarrelAngle = 0f;
    private string mMovementAxisName;
    private string mBarrelAngleAxisName;
    private string mFireButton;

    private float mMovementInputValue;
    private float mTurnInputValue;

    private float mCurrentLaunchForce = 30f;
    private bool mFired;
    // Use this for initialization
    void Start()
    {
        mMovementAxisName = "Mouse X";
        mBarrelAngleAxisName = "Mouse ScrollWheel";
        mFireButton = "Fire1";
        GameObject[] shellObjects = Resources.LoadAll<GameObject>("Shell");
        Debug.Log(shellObjects.Length);

        mShells = new Rigidbody[shellObjects.Length];
        for (int i = 0; i < mShells.Length; i++)
            mShells[i] = shellObjects[i].GetComponent<Rigidbody>();
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

        Rigidbody shellInstance = (Rigidbody)
                Instantiate(mShells[1], mFirePoint.transform.position, mFirePoint.transform.rotation);

        shellInstance.velocity = mCurrentLaunchForce * mFirePoint.transform.TransformDirection(new Vector3(0, 1, 0));
        Physics.IgnoreCollision(mFirePoint.transform.root.GetComponent<Collider>(), shellInstance.GetComponent<Collider>());
        
    }
}
