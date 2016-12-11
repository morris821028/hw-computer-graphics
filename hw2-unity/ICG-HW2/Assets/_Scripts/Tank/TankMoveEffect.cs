using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TankMoveEffect : MonoBehaviour {
    private List<Transform> Lgear = new List<Transform>();
    private List<Transform> Rgear = new List<Transform>();
    private Transform Ltrack, Rtrack;
    // Use this for initialization
    void Start () {
        Transform[] gChildren = GetComponentsInChildren<Transform>();
        Regex LgearExp1 = new Regex(@"gearM-L-\d+");
        Regex RgearExp1 = new Regex(@"gearM-R-\d+");
        Regex LgearExp2 = new Regex(@"gearL-\w+");
        Regex RgearExp2 = new Regex(@"gearR-\w+");
        Regex LgearExp3 = new Regex(@"gearS-L-\d+");
        Regex RgearExp3 = new Regex(@"gearS-R-\d+");
        foreach (Transform go in gChildren)
        {
            // self
            if (go.name == gameObject.name)
                continue;
            if (LgearExp1.IsMatch(go.name) || LgearExp2.IsMatch(go.name) || LgearExp3.IsMatch(go.name))
                Lgear.Add(go);
            else if (RgearExp1.IsMatch(go.name) || RgearExp2.IsMatch(go.name) || RgearExp3.IsMatch(go.name))
                Rgear.Add(go);
            else if (go.name == "track-L")
                Ltrack = go;
            else if (go.name == "track-R")
                Rtrack = go;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private float shiftLSum = 0f;
    private float shiftRSum = 0f;
    public void Move(float shift)
    {
        shiftLSum += shift;
        shiftRSum += shift;
        Renderer lrender = Ltrack.GetComponent<Renderer>();
        lrender.material.SetTextureOffset("_MainTex", new Vector2(-(shiftLSum / 100) % 1, 0));
        Renderer rrender = Rtrack.GetComponent<Renderer>();
        rrender.material.SetTextureOffset("_MainTex", new Vector2(-(shiftRSum / 100) % 1, 0));
        Vector3 rotation = new Vector3(shift, 0f, 0f);
        foreach (Transform go in Lgear)
        {
            go.Rotate(rotation);
        }
        foreach (Transform go in Rgear)
        {
            go.Rotate(rotation);
        }
    }
    public void Turn(float shift)
    {
        float lshift, rshift;
        float gap = 0.3f, moveSpeed = 2;
        if (shift > 0)
        {
            shiftLSum += shift;
            shiftRSum += -shift * gap;
            lshift = shift;
            rshift = -shift * gap;
        } else
        {
            shiftLSum += -shift * gap;
            shiftRSum += shift;
            lshift = -shift * gap;
            rshift = shift;
        }
        lshift *= moveSpeed;
        rshift *= moveSpeed;
        Renderer lrender = Ltrack.GetComponent<Renderer>();
        lrender.material.SetTextureOffset("_MainTex", new Vector2(-shiftLSum / 100 % 1, 0));
        Renderer rrender = Rtrack.GetComponent<Renderer>();
        rrender.material.SetTextureOffset("_MainTex", new Vector2(-shiftRSum / 100 % 1, 0));
        Vector3 rotationL = new Vector3(lshift, 0f, 0f);
        Vector3 rotationR = new Vector3(rshift, 0f, 0f);
        foreach (Transform go in Lgear)
        {
            go.Rotate(rotationL);
        }
        foreach (Transform go in Rgear)
        {
            go.Rotate(rotationR);
        }
    }
}
