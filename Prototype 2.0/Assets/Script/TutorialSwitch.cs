 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSwitch : MonoBehaviour {
	public bool tutorialSwitch;
	public string SelectField;
	public int tutorialCount;
	public bool switchChange;

	public  bool textCoin_1;
	public  bool textCoin_2;
	public  bool textPaku_1;
	public  bool textPenandaJurang_1;
    public Image textCoin_2Img;
	// Use this for initialization
	void Start () 
	{
      //  PlayerPrefs.DeleteKey("Tutorial");
        //       PlayerPrefs.SetInt("Tutorial", 0);
        textCoin_1 = false;
		textCoin_2 = false;
		textPaku_1 = false;
		textPenandaJurang_1 = false;
        tutorialCount = 0;
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Tutorial", 0);
        }


        if (PlayerPrefs.GetInt("Tutorial") == 1)
        {
            switchChange = false; 
        }
        else
        {
            switchChange = true;
        }
        tutorialSwitch = switchChange;
    }

    // Update is called once per frame
    void Update () 
	{
		GameObject[] tc1 = GameObject.FindGameObjectsWithTag ("TextCoin_1");
		if (textCoin_1) {
			foreach (GameObject go in tc1) {
				go.GetComponent<SpriteRenderer>().enabled = true;
			}
		} else {
			foreach (GameObject go in tc1) {
				go.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		
		if (textCoin_2) {

            textCoin_2Img.enabled = true;

        }
        else {


            textCoin_2Img.enabled = false;

        }

        GameObject[] tp1 = GameObject.FindGameObjectsWithTag ("TextPaku_1");
		if (textPaku_1) {
			foreach (GameObject go in tp1) {
				go.GetComponent<SpriteRenderer>().enabled = true;
			}
		} else {
			foreach (GameObject go in tp1) {
				go.GetComponent<SpriteRenderer>().enabled = false;
			}
		}

		GameObject[] tpj1 = GameObject.FindGameObjectsWithTag ("TextPenandaJurang_1");
		if (textPenandaJurang_1) {
			foreach (GameObject go in tpj1) {
				go.GetComponent<SpriteRenderer>().enabled = true;
			}
		} else {
			foreach (GameObject go in tpj1) {
				go.GetComponent<SpriteRenderer>().enabled = false;
			}
		}


		tutorialCount = PlatformGeneration.tutorialCount;

		//State Switching

		if (tutorialCount == 0) 
		{
			SelectField = "Kosong";
		}
		if (tutorialCount == 1) 
		{
			SelectField = "Coin";
		}
		if (tutorialCount == 2) 
		{
			SelectField = "Kosong";
		}
		if (tutorialCount == 3) 
		{
			SelectField = "Paku";
		}
		if (tutorialCount == 4) 
		{
			SelectField = "Penanda";
		}
		if (tutorialCount == 5) 
		{
			SelectField = "Jurang";
		}
		if (tutorialCount == 6) {
			
			SelectField = "Kosong2";

				
		}
        if (tutorialCount == 7)
        {

            SelectField = "Kosong";
            

        }

    }
}
