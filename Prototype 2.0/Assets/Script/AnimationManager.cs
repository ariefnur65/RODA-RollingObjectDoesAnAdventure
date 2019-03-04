using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	//private Animator settingBtnAnim;

	// Use this for initialization
	void Start () {
		//settingBtnAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SlideSettingPanel() {
		/*	Pakai get component saat melakukan interaksi dengan Animator Controller,
			Kalo gak pake, ada BUG di Unity dimana parameter yang digunakan tidak terdeteksi,
			Akan muncul Warning "Parameter _parameter_ does not exist." pada Console 
			This BUG is quite frustating :(
		*/

		if (GetComponent<Animator> ().GetBool("isSlideDown") == true) {
			GetComponent<Animator> ().SetBool ("isSlideDown", false);
		} else {
			GetComponent<Animator> ().SetBool ("isSlideDown", true);
		}
	}
}
