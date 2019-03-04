using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerUp : MonoBehaviour {

    public GameObject magneticObject;
    public bool hasMagnet;
    private float magneticForce;
    private float magneticRange;
    private float myDuration;
    //private string _target;
    private GameObject[] targetObjects;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetObjects = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject target in targetObjects)
        {
            if (hasMagnet == true)
            {
                if (Vector3.Distance(magneticObject.transform.position, target.transform.position) <= magneticRange)
                {
                    target.transform.Translate((magneticObject.transform.position - target.transform.position).normalized * magneticForce * Time.deltaTime, Space.Self);
                }
            }
        }
    }

    public void setMagnet(bool Status)
    {
        hasMagnet = Status;
    }

    public void setMagneticForce(float Force)
    {
        magneticForce = Force;
    }

    public void setMagneticRange(float Range)
    {
        magneticRange = Range;
    }
}
