using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaget : MonoBehaviour {
	Animator anim;
    private KarakterSkrip thePlayer;
	public float animTimer;
	// Use this for initialization
	void Start () {
		anim = GetComponent< Animator > ();
        thePlayer = FindObjectOfType<KarakterSkrip>();
    }
	
	// Update is called once per frame
	void Update () 
	{

		if (thePlayer.isPlaying) {
			anim.SetBool ("bool", true);
			animTimer -= Time.deltaTime;

			if(animTimer <= 0 ){
				animTimer = 0f;
				anim.SetBool ("bool", false);
			}

		} else {
			anim.SetBool ("bool", false);
		}	
	}
}
