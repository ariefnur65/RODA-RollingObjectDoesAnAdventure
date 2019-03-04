using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalonGerak : MonoBehaviour {
    public float speedX;
    public float speedY;
    public float timerBalon;
    public float timerBalonStore;
    public bool naikKah;
    public float atasBawahThreshhold;

    public GameObject maxHeight;
    public GameObject minHeight;
	// Use this for initialization
	void Start () {
        minHeight = GameObject.Find("SkyBarrierBottom");
        maxHeight = GameObject.Find("SkyBarrierUp");
        naikKah = true;
        timerBalon = timerBalonStore;

	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
        {
            if (timerBalon < 0 || transform.position.y >= maxHeight.transform.position.y || transform.position.y <= minHeight.transform.position.y)
            {
                if (Random.Range(0, 100) < atasBawahThreshhold)
                { //Jika Kurang maka naik
                    naikKah = true;
                }
                else
                { // Jika lebih maka turun
                    naikKah = false;
                }

                if (transform.position.y >= maxHeight.transform.position.y) //Jika Melebihi max maka harus turun
                {
                    naikKah = false;
                }

                if (transform.position.y <= minHeight.transform.position.y) // Jika Dibawah min maka harus naik
                {
                    naikKah = true;
                }
                timerBalon = timerBalonStore;
            }
            if (naikKah)
            {
                balonNaik();
            }
            else
            {
                balonTurun();
            }
            timerBalon -= Time.deltaTime;

        }
    }
       

    public void balonNaik()
    {
        transform.Translate(new Vector3(speedX, speedY, 0));

    }

    public void balonTurun()
    {
        transform.Translate(new Vector3(speedX, -speedY, 0));
    }
}
