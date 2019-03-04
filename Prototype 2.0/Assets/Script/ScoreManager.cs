using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private KarakterSkrip _Karakter;	//karakter var

	private int _currentCoinPoints; 	//variable penampung nilai jumlah poin saat ini
	private int _highestCoinPoints; 	//variable penampung total poin tertinggi yang pernah dicapai
	public int _collectedCoinPoints;	//variable sebagai bank koin yg telah dikumpulkan


	private float _currentDistance; 	//variable penampung nilai jarak saat ini
	private float _fartestDistance; 	//variable penampung nilai jarak terjauh yang pernah ditempuh

	// Use this for initialization
	void Start () {

		_Karakter = GameObject.FindObjectOfType<KarakterSkrip> ();

		//Jika ada key HighCoin dan HighJark maka high jarak dan coin di set sesuai nial
		if (PlayerPrefs.HasKey("HighCoin") != false)
        {
			_highestCoinPoints = PlayerPrefs.GetInt("HighCoin");
		}

		if (PlayerPrefs.HasKey("HighJarak") != false)
		{
			_fartestDistance = PlayerPrefs.GetFloat("HighJarak");
		}


		if (PlayerPrefs.HasKey ("CollectedCoin") != false) {
			if (PlayerPrefs.GetInt ("CollectedCoin") <= 0) {
				_collectedCoinPoints = 0;
			} else {
				_collectedCoinPoints = PlayerPrefs.GetInt ("CollectedCoin");
			}
		} else {
			_collectedCoinPoints = 0;
		}

		PlayerPrefs.SetInt("TempHighCoin", 0);
		PlayerPrefs.SetFloat("TempHighJarak", 0f);
		PlayerPrefs.SetInt ("TempCollectedCoin", 0);
	}
    
	
	// Update is called once per frame
	void Update () {
		setCurrentDistance (_Karakter.getDistance ());
		if (_currentCoinPoints > _highestCoinPoints) {
			PlayerPrefs.SetInt("TempHighCoin", _currentCoinPoints);
			setHighestPoint (PlayerPrefs.GetInt ("TempHighCoin"));
		}
		if (_currentDistance > _fartestDistance) {
			PlayerPrefs.SetFloat ("TempHighJarak", _currentDistance);
			setFartestDistance (PlayerPrefs.GetFloat("TempHighJarak"));
		}

		setCollectedCoin ();
	}


	public void resetScore(){
		_currentCoinPoints 	= 0;
		_currentDistance 	= 0;

		setHighestPoint (PlayerPrefs.GetInt ("HighCoin"));
		setFartestDistance (PlayerPrefs.GetFloat("HighJarak"));
	}

	public void saveScore(){
		PlayerPrefs.SetInt("HighCoin", _highestCoinPoints);
		PlayerPrefs.SetFloat ("HighJarak", _fartestDistance);
		PlayerPrefs.SetInt ("CollectedCoin", PlayerPrefs.GetInt("TempCollectedCoin"));


		PlayerPrefs.SetInt("TempHighCoin", 0);
		PlayerPrefs.SetFloat("TempHighJarak", 0f);
		PlayerPrefs.SetInt ("TempCollectedCoin", 0);
	}


	#region Coin 
	public void addPoint(int pointsOfCoin) {
		_currentCoinPoints += pointsOfCoin;
	}

	public int getCurrentPoint(){
		return _currentCoinPoints;
	}

	public void setHighestPoint(int _hiPoint){
		_highestCoinPoints = _hiPoint;
	}
	public int getHighestPoint(){
		return _highestCoinPoints;
	}

	public void setCollectedCoin(){
		_collectedCoinPoints = _currentCoinPoints + PlayerPrefs.GetInt ("CollectedCoin");
		PlayerPrefs.SetInt ("TempCollectedCoin", _collectedCoinPoints);
	}
	public int getCollectedCoin(){
		return _collectedCoinPoints;
	}
	#endregion

	#region Distance
	public void setCurrentDistance(float _distance){
		_currentDistance = _distance;
	}

	public float getCurrentDistance(){
		return Mathf.Round (_currentDistance);
	}
	public void setFartestDistance(float _fartest){
		_fartestDistance = _fartest;
	}
	public float getFartestDistance(){
		return Mathf.Round (_fartestDistance);
	}
	#endregion
}
