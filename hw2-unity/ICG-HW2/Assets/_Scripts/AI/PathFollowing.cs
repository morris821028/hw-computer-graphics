using UnityEngine;
using System.Collections;

public class PathFollowing : MonoBehaviour {
    public Path path;

    public float speed = 20f;
    public float mass = 5f;

    public bool isLooping = true;
    public float mTurnSpeed = 10f;

    private float curSpeed;
    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPosition;
    private Vector3 curVelocity;
    
    private Rigidbody mRigidbody;

    // Use this for initialization
    void Start () {
        pathLength = path.Length;
        curPathIndex = 0;
        curVelocity = transform.forward;
        mRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        curSpeed = speed * Time.deltaTime;
        targetPosition = path.GetPosition(curPathIndex);

        if (Vector3.Distance(transform.position, targetPosition) < path.radius)
        {
            if (curPathIndex < pathLength - 1)
                curPathIndex++;
            else if (isLooping)
                curPathIndex = 0;
            else
                return;
        }

        // Vector3 dV = Accelerate(targetPosition);
        Vector3 dV = targetPosition - mRigidbody.position;
        dV.Normalize();
        dV.y = 0;
        curVelocity = dV * 0.1f;

        // Debug.Log(dV.ToString("F5"));

        // mRigidbody.MovePosition(mRigidbody.position + curVelocity);

        Vector3 forward = gameObject.transform.forward;
        float turnAngle = 0f;
        turnAngle = Vector2.Angle(new Vector2(-forward.x, -forward.z), new Vector2(dV.x, dV.z));
        if (Vector3.Cross(new Vector2(-forward.x, -forward.z), new Vector2(dV.x, dV.z)).z < 0)
            turnAngle = -turnAngle;
        turnAngle = Mathf.Clamp(turnAngle, -180f, 180f);

        if (180f - Mathf.Abs(turnAngle) > 5 && Mathf.Abs(turnAngle) > 5)
        {
            // Debug.Log(turnAngle);
            turnAngle /= 180f;
            turnAngle *= mTurnSpeed;
            Quaternion turnRotation = Quaternion.Euler(0f, -turnAngle, 0f);
            mRigidbody.MoveRotation(mRigidbody.rotation * turnRotation);
        } else
        {
            mRigidbody.MovePosition(mRigidbody.position + curVelocity);
        }
	}

    public Vector3 Accelerate(Vector3 target)
    {
        Vector3 desiredVelocity = target - transform.position;

        desiredVelocity.Normalize();

        desiredVelocity *= curSpeed;

        Vector3 sterringForce = desiredVelocity - curVelocity;
        Vector3 acceleration = sterringForce / mass;
        return acceleration;
    }
}


