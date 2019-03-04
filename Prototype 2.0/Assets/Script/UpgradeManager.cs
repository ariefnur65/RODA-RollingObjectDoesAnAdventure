using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

	public KarakterSkrip karakter;
	private ScoreManager score;
	private GameManager GM;
	private UIManager UIM;
	public int hargaSlowMo;
	public int hargaBounce;
	public int hargaAero;
	public int hargaMagnet;
	public int hargaSteel;

	// Use this for initialization
	void Start () {
		karakter = FindObjectOfType<KarakterSkrip> ();
		score = FindObjectOfType<ScoreManager> ();
		GM = FindObjectOfType<GameManager> ();
		UIM = FindObjectOfType<UIManager> ();
		CekPUTimer ();
		CekPUHarga ();

		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.SetInt("CollectedCoin", 1000000);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void SlowMoUpgrade(){
		//mengurangi coin yg dimiliki dengan harga upgrade
		score._collectedCoinPoints = PlayerPrefs.GetInt("CollectedCoin");
		score._collectedCoinPoints -= hargaSlowMo;
		PlayerPrefs.SetInt ("CollectedCoin", score._collectedCoinPoints);
		//menaikkan harga upgrade 1,5 kali dari harga sebelumnya
		hargaSlowMo = hargaSlowMo * 2;
		PlayerPrefs.SetInt ("hargaSlowMo",hargaSlowMo);
		//menambah level
		karakter.slowMoTime += 1.0f;
		PlayerPrefs.SetFloat ("slowMoTime", karakter.slowMoTime);
	}

	public void BouncenessUpgrade(){
		//mengurangi coin yg dimiliki dengan harga upgrade
		score._collectedCoinPoints = PlayerPrefs.GetInt("CollectedCoin");
		score._collectedCoinPoints -= hargaBounce;
		PlayerPrefs.SetInt ("CollectedCoin", score._collectedCoinPoints);
		//menaikkan harga upgrade 1,5 kali dari harga sebelumnya
		hargaBounce = hargaBounce * 2;
		PlayerPrefs.SetInt ("hargaBounce",hargaBounce);
		//menambah level
		karakter.bouncingTime += 1.0f;
		PlayerPrefs.SetFloat ("bouncingTime", karakter.bouncingTime);
	}

	public void AeroUpgrade(){
		//mengurangi coin yg dimiliki dengan harga upgrade
		score._collectedCoinPoints = PlayerPrefs.GetInt("CollectedCoin");
		score._collectedCoinPoints -= hargaAero;
		PlayerPrefs.SetInt ("CollectedCoin", score._collectedCoinPoints);
		//menaikkan harga upgrade 1,5 kali dari harga sebelumnya
		hargaAero = hargaAero * 2;
		PlayerPrefs.SetInt ("hargaAero",hargaAero);
		//menambah level
		karakter.aeroTime += 1.0f;
		PlayerPrefs.SetFloat ("aeroTime", karakter.aeroTime);
	}

	public void MagnetUpgrade (){
		//mengurangi coin yg dimiliki dengan harga upgrade
		score._collectedCoinPoints = PlayerPrefs.GetInt("CollectedCoin");
		score._collectedCoinPoints -= hargaMagnet;
		PlayerPrefs.SetInt ("CollectedCoin", score._collectedCoinPoints);
		//menaikkan harga upgrade 1,5 kali dari harga sebelumnya
		hargaMagnet = hargaMagnet * 2;
		PlayerPrefs.SetInt ("hargaMagnet",hargaMagnet);
		//menambah level
		karakter.magnetTime += 1.0f;
		PlayerPrefs.SetFloat ("magnetTime", karakter.magnetTime);
	}

	public void SteelUpgrade(){
		//mengurangi coin yg dimiliki dengan harga upgrade
		score._collectedCoinPoints = PlayerPrefs.GetInt("CollectedCoin");
		score._collectedCoinPoints -= hargaSteel;
		PlayerPrefs.SetInt ("CollectedCoin", score._collectedCoinPoints);
		//menaikkan harga upgrade 1,5 kali dari harga sebelumnya
		hargaSteel = hargaSteel * 2;
		PlayerPrefs.SetInt ("hargaSteel",hargaSteel);
		//menambah level
		karakter.steelTime += 1.0f;
		PlayerPrefs.SetFloat ("steelTime", karakter.steelTime);
	}

	void CekPUTimer(){
		if (PlayerPrefs.HasKey ("slowMoTime") != false) {
			karakter.slowMoTime = PlayerPrefs.GetFloat ("slowMoTime");
		} else {
			karakter.slowMoTime = karakter.slowMoTime;
		}

		if (PlayerPrefs.HasKey ("bouncingTime") != false) {
			karakter.bouncingTime = PlayerPrefs.GetFloat ("bouncingTime");
		} else {
			karakter.bouncingTime = karakter.bouncingTime;
		}

		if (PlayerPrefs.HasKey ("aeroTime") != false) {
			karakter.aeroTime = PlayerPrefs.GetFloat ("aeroTime");
		} else {
			karakter.aeroTime = karakter.aeroTime;
		}

		if (PlayerPrefs.HasKey ("magnetTime") != false) {
			karakter.magnetTime = PlayerPrefs.GetFloat ("magnetTime");
		} else {
			karakter.magnetTime = karakter.magnetTime;
		}

		if (PlayerPrefs.HasKey ("steelTime") != false) {
			karakter.steelTime = PlayerPrefs.GetFloat ("steelTime");
		} else {
			karakter.steelTime = karakter.steelTime;
		}
	}

	void CekPUHarga(){
		if (PlayerPrefs.HasKey ("hargaSlowMo") != false) {
			hargaSlowMo = PlayerPrefs.GetInt ("hargaSlowMo");
		} else {
			hargaSlowMo = hargaSlowMo;
		}

		if (PlayerPrefs.HasKey ("hargaBounce") != false) {
			hargaBounce = PlayerPrefs.GetInt ("hargaBounce");
		} else {
			hargaBounce = hargaBounce;
		}

		if (PlayerPrefs.HasKey ("hargaAero") != false) {
			hargaAero = PlayerPrefs.GetInt ("hargaAero");
		} else {
			hargaAero = hargaAero;
		}

		if (PlayerPrefs.HasKey ("hargaMagnet") != false) {
			hargaMagnet = PlayerPrefs.GetInt ("hargaMagnet");
		} else {
			hargaMagnet = hargaMagnet;
		}

		if (PlayerPrefs.HasKey ("hargaSteel") != false) {
			hargaSteel = PlayerPrefs.GetInt ("hargaSteel");
		} else {
			hargaSteel = hargaSteel;
		}
	}

	public int getHarga (string obj){
		switch(obj){
		case "slowmo":
			return hargaSlowMo;
			break;
		case "bounce":
			return hargaBounce;
			break;
		case "aero":
			return hargaAero;
			break;
		case "magnet":
			return hargaMagnet;
			break;
		case "steel":
			return hargaSteel;
			break;
		default :
			return 0;
			break;
		}
	}

	public float getLevel(string obj){
        switch (obj){
		case "slowmo":
			return karakter.slowMoTime - 5f;
			break;
		case "bounce":
			return karakter.bouncingTime - 5f;
			break;
		case "aero":
			return karakter.aeroTime - 5f;
			break;
		case "magnet":
			return karakter.magnetTime - 5f;
			break;
		case "steel":
			return karakter.steelTime - 5f;
			break;
		default :
			return 0;
			break;
		}
	}
}
