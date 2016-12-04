using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour
{
    public bool showPath = true;
    public Color pathColor = Color.red;
    public bool loop = true;
    public float radius = 2f;
    public Transform[] wpoints;
    public float Length
    {
        get
        {
            return wpoints.Length;
        }
    }
    void Reset()
    {
        wpoints = new Transform[GameObject.FindGameObjectsWithTag("ccPath").Length];
        for (int i = 0; i < wpoints.Length; i++)
        {
            wpoints[i] = GameObject.Find("wpoint" + i).transform;
        }
    }

    public Vector3 GetPosition(int index)
    {
        return wpoints[index].position;
    }

    void OnDrawGizmos()
    {
        if (!showPath)
            return;

        for (int i = 0; i < wpoints.Length; i++)
        {
            if (i + 1 < wpoints.Length)
            {
                Debug.DrawLine(wpoints[i].position, wpoints[i + 1].position, pathColor);
            }
            else
            {
                if (loop)
                {
                    Debug.DrawLine(wpoints[i].position, wpoints[0].position, pathColor);
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
