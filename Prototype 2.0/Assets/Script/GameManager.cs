using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private UIManager _UIManager;
	public KarakterSkrip _Karakter;
    public SoundManager _SoundManager;
	public ScoreManager _ScoreManager;
    public CameraControl Camera;
    public Transform StartCam;
    public bool landMarkStoreOke;
    public bool changeToFirstMain;
    private Kaget[] theKageters;
    private int reviveCounter;
    private InteractableOff theInteractable;
    //private float timer = 3f;


    /*Algoritama death counterCounter jika di tekan revive maka akan bertambah
     * jika lebih dari 2 maka akan disable button nya
     * ketika di tekan tombol death dan Pause home menu untuk melihat score maka akan kembali ke 0
     *  
     *===========================Pause life-road========
     * 
     * 1. Non aktif ketika, death menu ON dan InfoPanel keluar
     * 2. Aktif lagi ketika Restart dan respawn serta balik ke main menu
         */

    // Use this for initialization
    void Start () {
        _UIManager = GameObject.FindObjectOfType<UIManager>();
        _SoundManager = GameObject.FindObjectOfType<SoundManager>();
        _Karakter = GameObject.FindObjectOfType<KarakterSkrip>();
        //_SoundManager = FindObjectOfType<SoundManager>();
        theKageters = FindObjectsOfType<Kaget>();
        theInteractable = FindObjectOfType<InteractableOff>();

        reviveCounter = 0;

        if (_UIManager._soundFXstat == 1) {
			_UIManager._mainSoundFX.isOn = true;
			_UIManager._soundFX.isOn = true;
		} else {
			_UIManager._mainSoundFX.isOn = false;
			_UIManager._soundFX.isOn = false;
		}

		if (_UIManager._BGMstat == 1) {
			_UIManager._mainBGM.isOn = true;
			_UIManager._bgm.isOn = true;
			_SoundManager.BGM.Play ();
			_SoundManager.BGM.loop = true;
		} else {
			_UIManager._mainBGM.isOn = false;
			_UIManager._bgm.isOn = false;
		}
		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.SetInt("CollectedCoin", 10000000);
	}

	// Update is called once per frame
	void Update () {
		_UIManager._soundFXstat = PlayerPrefs.GetInt ("SFXToggleIsOn");
		if (_UIManager._soundFXstat == 0) {
			_SoundManager.MuteSoundFX ();
		} else {
			_SoundManager.UnmuteSoundFX ();
		}

		_UIManager._BGMstat = PlayerPrefs.GetInt ("BGMToggleIsOn");
		if (_UIManager._BGMstat == 0) {
			_SoundManager.MuteBGM ();
		} else {
			_SoundManager.UnmuteBGM ();
		}

        if (_UIManager._deathMenu.activeInHierarchy || _UIManager._scoreBoard.activeInHierarchy || _UIManager._tapToPlay.activeSelf)
        {
            _UIManager._pauseButton.interactable = false;
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.Escape))
            {
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.Escape))
            {
                if (_Karakter.isPlaying == true) PauseGame();
                else _UIManager.showAlertQuit();
            }
        }





        //Tambah arief
        if (!_SoundManager.deathSwitch)
        {
            _SoundManager.BGMDeath();
        }
	}

	public void PlayGame(){
		_UIManager.hideMainMenu ();
		_Karakter.isPlaying = true;
        _SoundManager.BGMMain();
        landMarkStoreOke = false;
        foreach (Kaget theKaget in theKageters)
        {

            theKaget.animTimer = 3f;
        }
        _Karakter.isCamFollowing = true;//Nambah ole
    }

	public void ResetGame(){
		DestroyGeneratedObjects ();
	}

	public void NoRevive(){
        reviveCounter = 0; // Jika no revive maka akan kembali ke 0
		_UIManager.hideDeathMenu ();
	}

	public void Death(){
		_UIManager.timer = 10f;
		_UIManager.showDeathMenu (); //menampilkan panel Death Menu
        _SoundManager.deathSwitch = false; // Play Death Switch

        _ScoreManager.setCollectedCoin();
	}

	public void PauseGame(){
		Time.timeScale = 0f;
		_UIManager.showPauseMenu ();	
	}

	public void ResumeGame(){
		_UIManager.hidePauseMenu ();
		Time.timeScale = 1f;
	}

	public void RestartGame(){
		_UIManager.hideScoreBoard ();
		DestroyGeneratedObjects ();
		_Karakter.ResetKarakterScript ();
        _UIManager.ResetUI ();
        _Karakter.isPlaying = false;
        Camera.ResetCamera();
        _ScoreManager.saveScore();
        landMarkStoreOke = true;
        reviveCounter = 0; // Revive Counter restart
        _UIManager.restart = true;
		_UIManager.delay = 3f;
        foreach (Kaget theKaget in theKageters)
        {
            theKaget.animTimer = 4f;
        }
        _ScoreManager.resetScore();
    }

    //Tambah Untuk DeathRespawn__________________________________________________________________________________________________________________________
    public void DeathRespawn()
    {
        reviveCounter++;
        _UIManager._death = false;
        _UIManager._deathMenu.SetActive(false);
        _Karakter.noAds.SetActive(false);
        //_UIManager.hideScoreBoard();
        _Karakter.gameObject.SetActive(true);
     //   _Karakter.isPlaying = false;
        _Karakter.blackScreen.SetActive(false);
        _UIManager.respawnRestart = true;
        _UIManager.delay = 3f;
    }

    public void GoToMainMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainGame");
        reviveCounter = 0;

	}
		
	public void QuitGame(){
		Application.Quit ();
	}

	//Not used 
	public void ShowHideSetting(){
		if (_UIManager._settingPanel.activeSelf == true) {
			_UIManager._settingPanel.SetActive (false);
		} else {
			_UIManager._settingPanel.SetActive (true);
		}
	}

	#region Sound Setting
	public void MuteOrUnmuteSoundFX(){
        _UIManager 		= FindObjectOfType<UIManager> ();

		if (_UIManager._soundFX.isOn == false) {
			PlayerPrefs.SetInt ("SFXToggleIsOn", 0);
		} else {
			PlayerPrefs.SetInt ("SFXToggleIsOn", 1);
		}
		_UIManager._mainSoundFX.isOn = _UIManager._soundFX.isOn;
	}

	public void MainMuteOrUnmuteSoundFX(){
		_UIManager 		= FindObjectOfType<UIManager> ();

		if (_UIManager._mainSoundFX.isOn == false) {
			PlayerPrefs.SetInt ("SFXToggleIsOn", 0);
		} else {
			PlayerPrefs.SetInt ("SFXToggleIsOn", 1);
		}
		_UIManager._soundFX.isOn = _UIManager._mainSoundFX.isOn;
	}

	public void MuteOrUnmuteBGM(){
        _UIManager = FindObjectOfType<UIManager>();

        if (_UIManager._bgm.isOn == false) {
			PlayerPrefs.SetInt ("BGMToggleIsOn", 0);
		} else {
			PlayerPrefs.SetInt ("BGMToggleIsOn", 1);
		}
		_UIManager._mainBGM.isOn = _UIManager._bgm.isOn;
	}

	public void MainMuteOrUnmuteBGM(){
        _UIManager = FindObjectOfType<UIManager>();

        if (_UIManager._mainBGM.isOn == false) {
			PlayerPrefs.SetInt ("BGMToggleIsOn", 0);
		} else {
			PlayerPrefs.SetInt ("BGMToggleIsOn", 1);
		}
		_UIManager._bgm.isOn = _UIManager._mainBGM.isOn;
	}
	#endregion

	public void DestroyGeneratedObjects(){
		PlatformDestroyer[]  _fields = FindObjectsOfType<PlatformDestroyer>();
		PlatformCoinDestroyer[] _fieldCoin = FindObjectsOfType<PlatformCoinDestroyer>();
        LandmarkDestroyer[] LandmarkBG = FindObjectsOfType<LandmarkDestroyer>();
		for (int n = 0; n < _fields.Length; n++)
		{

			_fields[n].gameObject.SetActive(false);
		}


		for (int n = 0; n < _fieldCoin.Length; n++)
		{
			_fieldCoin[n].gameObject.SetActive(false); //karena fungsi reset dijalankan setelah objek tidak aktif
			_fieldCoin[n].Reset();
		}

        for (int n = 0; n < LandmarkBG.Length; n++)
        {
            LandmarkBG[n].gameObject.GetComponent<LandMarkGerak>().awanKembali();
            LandmarkBG[n].gameObject.SetActive(false); //karena fungsi reset dijalankan setelah objek tidak aktif
        }
    }

    public int getReviveCounter()
    {
        return reviveCounter;
    }


    private void OnApplicationPause(bool pause)
    {
        if (pause && _Karakter.isPlaying && !theInteractable.getInfoPanelStatus() && !_UIManager._deathMenu.activeSelf && !_UIManager._scoreBoard.activeSelf) //Lagi pause dan sendang playing dan infopanel sedang off
        {
            PauseGame();
        }
    }
}
