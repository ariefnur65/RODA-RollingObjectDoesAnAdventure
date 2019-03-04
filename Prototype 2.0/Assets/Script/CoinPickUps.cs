using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickUps : MonoBehaviour {


    //private Text coinPoints;
	private ScoreManager scoreManager;
    public int pointOfCoin;
    public int totalScore;
    // Use this for initialization
    private AudioSource   coinSound;

    private KarakterSkrip thePlayer;
	void Start () {
        totalScore = 0;
        //coinPoints.text = totalScore.ToString();
		scoreManager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("CoinSound2").GetComponent<AudioSource>();
        thePlayer = FindObjectOfType<KarakterSkrip>();

    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (thePlayer.isPlaying)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //int.TryParse(coinPoints.text, out totalScore);
                //coinPoints.text = (pointOfCoin + totalScore).ToString();
                coinSound.Play();
                scoreManager.addPoint(pointOfCoin);
                gameObject.SetActive(false);
            }
        }
        
    }
}
