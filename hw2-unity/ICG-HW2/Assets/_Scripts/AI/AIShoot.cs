using UnityEngine;
using System.Collections;

public class AIShoot : MonoBehaviour
{
    // public
    public GameObject mTargetObject;
    public GameObject mBarrelObject;
    public GameObject mFirePoint;
    public float mBarrelSpeed = 30f;
    public float mBarrelMaxAngle = 80f;
    public float mBarrelMinAngle = -5f;

    public float mBaseAngle = 0;
    public float mBaseSpeed = 1f;
    public float mBaseMaxAngle = 5f; 

    public float mTowerSpeed = 6;
    public float mExplosionRadius = 100f;
    public float mExplosionForce = 1e+20f;
    public float mDetectRange = 50f;
    public float mFiredRange = 30f;
    public int[] mAmmoAmount;
    // private
    private Rigidbody[] mShells;
    private float mBarrelAngle = 0f;
    /*
    private string mMovementAxisName;
    private string mBarrelAngleAxisName;
    private string mAmmoInputPrefixName;
    private string mFireButton;
    */

    private float mMovementInputValue;
    private float mTurnInputValue;
    private int mAmmoSelectIndex;

    private float mCurrentLaunchForce = 30f;
    private bool mFired;
    private float mFireTime;
    // Use this for initialization
    void Start()
    {
        GameObject[] shellObjects = Resources.LoadAll<GameObject>("Shell");

        mShells = new Rigidbody[shellObjects.Length];
        mAmmoAmount = new int[shellObjects.Length];
        for (int i = 0; i < mShells.Length; i++)
        {
            mShells[i] = shellObjects[i].GetComponent<Rigidbody>();
            mAmmoAmount[i] = 30;
        }
        mAmmoSelectIndex = 0;
        mFireTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - mFireTime > 5 && mFired)
        {
            mFired = false;
        }
        if (!mFired && Vector3.Distance(gameObject.transform.position, mTargetObject.transform.position) < mFiredRange)
        {
            Fired();
        }

        Turn();
    }

    private void Turn()
    {
        if (Vector3.Distance(gameObject.transform.position, mTargetObject.transform.position) > mDetectRange)
            return;

        // Calcuate turn angle by relative position
        Vector3 dV = mTargetObject.transform.position - gameObject.transform.position;
        Quaternion rotation = Quaternion.LookRotation(dV);
        rotation.x = 0f;
        rotation.z = 0f;

        float turnAngle = rotation.y;

        this.transform.rotation = rotation;
        this.transform.Rotate(-90f, 180f, 0f);
        
        /*
        mBarrelAngle += mTurnInputValue * mBarrelSpeed;
        mBarrelAngle = Mathf.Clamp(mBarrelAngle, mBarrelMinAngle, mBarrelMaxAngle);
        Vector3 temp = mBarrelObject.transform.localEulerAngles;
        temp.x = mBarrelAngle;
        mBarrelObject.transform.localEulerAngles = temp;//上下旋轉砲管
        */
    }

    void Fired()
    {
        mFired = true;
        mFireTime = Time.time;
        int t = mAmmoSelectIndex;
        /*
        if (mAmmoAmount[t] <= 0)
            return;
        */
        Rigidbody shellInstance = (Rigidbody)
                Instantiate(mShells[t], mFirePoint.transform.position, mFirePoint.transform.rotation);
        mAmmoAmount[t]--;
        shellInstance.velocity = mCurrentLaunchForce * mFirePoint.transform.TransformDirection(new Vector3(0, 1, 0));
        Physics.IgnoreCollision(mFirePoint.transform.root.GetComponent<Collider>(), shellInstance.GetComponent<Collider>());
    }
}
