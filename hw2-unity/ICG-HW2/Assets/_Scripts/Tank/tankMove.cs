using UnityEngine;
using System.Collections;

public class tankMove : MonoBehaviour
{
    // public 
    public int mPlayerID = 1;
    public float mMinSpeed = 6f;
    public float mMaxSpeed = 12f;
    public float mAccSpeed = 0.2f;
    public float mTurnSpeed = 50f;

    // private
    private float mSpeed;
    private string mMovementAxisName;
    private string mTurnAxisName;
    private Rigidbody mRigidbody;
    private float mMovementInputValue;
    private float mTurnInputValue;

    private float mBarrelAngle;

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mSpeed = mMinSpeed;
    }
    private void OnEnable()
    {
        mRigidbody.isKinematic = false;
        mMovementInputValue = 0f;
        mTurnInputValue = 0f;
    }
    // Use this for initialization
    private void Start()
    {
        mMovementAxisName = "Vertical";
        mTurnAxisName = "Horizontal";
    }

    // Update is called once per frame
    private void Update()
    {
        // 獲取水平軸向按鍵
        mMovementInputValue = Input.GetAxis(mMovementAxisName);
        // 獲取垂直軸向按鍵
        mTurnInputValue = Input.GetAxis(mTurnAxisName);
    }
    private void FixedUpdate()
    {
        // 根據水平軸向按鍵來前進或後退
        Move();
        // 根據垂直軸向按鍵來旋轉
        Turn();
    }

    private void Move()
    {
        if (Mathf.Abs(mMovementInputValue) >= 0.1f && mSpeed <= mMinSpeed)
        {
            mSpeed = mMinSpeed + mAccSpeed;
        } else if (Mathf.Abs(mMovementInputValue) >= 0.1f)
        {
            mSpeed = mSpeed + mAccSpeed;
            mSpeed = Mathf.Clamp(mSpeed, mMinSpeed, mMaxSpeed);
        } else
        {
            mSpeed = mMinSpeed;
        }
        Vector3 moveVec = transform.forward * mMovementInputValue * (-mSpeed) * Time.deltaTime;
        mRigidbody.MovePosition(mRigidbody.position + moveVec);
    }

    private void Turn()
    {
        float turnAngle = mTurnInputValue * mTurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turnAngle, 0f);

        mRigidbody.MoveRotation(mRigidbody.rotation * turnRotation);
    }
}
