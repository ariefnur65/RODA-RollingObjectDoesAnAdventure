using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunungGerak : MonoBehaviour {
    private float Speed;
    public KarakterSkrip thePlayer;
    public float speedPercent;
    public float konstantaSpeed;
    public float kecepatan;
    public float kecepatanmax;
	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<KarakterSkrip>();
	}
	
	// Update is called once per frame
	void Update () {
        if (thePlayer.isActiveAndEnabled)
        {
            if (thePlayer.isPlaying)
            {
                if (Time.timeScale != 0)
                {
                    Speed = thePlayer.getMyVelocity() ;
                    kecepatan = Speed * speedPercent * Time.deltaTime * konstantaSpeed;
                    if (kecepatan >= kecepatanmax)
                    {
                        kecepatan = kecepatanmax;
                    }
                    transform.Translate(-kecepatan, 0, 0);

                }
            }
            
        }
          
    }
}
