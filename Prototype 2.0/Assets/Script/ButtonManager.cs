using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
	/*
	#region Main Menu Buttons
	public Button btnPlay;
	public Button btnQuit;
	public Button btnLandmark;
	public Button btnUpgrade;
	public Button btnCredits;
	public Button btnSettingMain;
	#endregion

	#region Credits Menu Buttons
	public Button btnBackCredits;
	#endregion

	#region Landmark Menu Buttons
	public Button btnBackLandmark;
	public Button btn;
	#endregion
	*/
	public Button[] UIbtns;
	public UIManager UIman;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Button btn in UIbtns){
			if (UIman._alerts.activeInHierarchy) {
				btn.interactable = false;
			} else {
				btn.interactable = true;
			}
		}
	}
}
