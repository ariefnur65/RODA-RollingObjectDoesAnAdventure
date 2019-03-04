using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject _pauseMenu; 		//pause menu object
	public GameObject _deathMenu; 		//death menu object
	public GameObject _mainMenu; 		//main menu object
	public GameObject _upgradeMenu;		//upgrade menu object
	public GameObject _scoreBoard; 		//score board object
	public GameObject _settingPanel;    //setting panel object
	public GameObject _credits;
    public GameObject _landmarkMenu;
    public GameObject _alerts;
	public Button _pauseButton;
	public GameObject _CoinPoints;
	public GameObject _Distance;


    //tambah Death Respawn__________________________________________________________________________________________________________________________
    public GameObject _tapToPlay;
    public bool respawnRestart;
    public bool respawnTimer;

    public Toggle _soundFX;
	public Toggle _bgm;

	public Toggle _mainSoundFX;
	public Toggle _mainBGM;

	public Button _slowmoUp;
	public Button _bounceUp;
	public Button _aeroUp;
	public Button _magnetUp;
	public Button _steelUp;

	public Button _confirmBtn;
	public Button _cancelBtn;
	public Button _singleBtn;

	public int _soundFXstat;
	public int _BGMstat;

	private ScoreManager _scoreManager;	//score manager var
	private  UpgradeManager _upgradeManager; //upgrade manager var
	private GameManager _GM;
	private KarakterSkrip _Karakter;
    private InteractableOff Interactable;
    private SoundManager theSoundMan; // var Sound
    private CameraControl theCamera;
    public bool _death;


	public Image _steelPowerUp; 		//simbol memperoleh Steel power
	public Text _steelTime;				//text timer Steel power aktif
	public Image _bouncePowerUp; 	//simbol memperoleh Bounce power
	public Text _bounceTime;			//text timer Bounce power aktif
	public Image _aeroPowerUp; 		//simbol memperoleh Aero power
	public Text _aeroTime;				//text timer Aero power aktif
	public Image _magnetPowerUp; 	//simbol memperoleh Magnet power
	public Text _magnetTime;			//text timer Magnet power aktif
	public Image _sloMoPowerUp; 		//simbol memperoleh Slow Motion
	public Text _sloMoTime;				//text timer Slow Motion aktif

	public Text _currentDistance;		//text pencatat jarak yang telah ditempuh
	public Text _fartestDistance;		//text pencatat jarak terjauh yang pernah dicapai

	public Text _highestPoints; 		//text pencatat poin tertinggi yang pernah didapat
	public Text _currentPoints; 		//text pencatat poin saat ini

	public Text _yourPoints;			//text pencatat poin saat ini (ScoreBoard)
	public Text _yourDistance;			//text pencatat jarak yang telah ditempuh (ScoreBoard)
	public Text _yourBestPoints;		//text pencatat poin tertinggi yang pernah didapat (ScoreBoard)
	public Text _yourBestDistance;		//text pencatat jarak terjauh yang pernah dicapai (ScoreBoard)
	public Text _timeToContinue;		//text pencatat timer untuk memutuskan lanjut atau mengakhiri game saat death menu muncul
	public float timer;

	public Text _timerDelayRestart;
	public Text _collectedPoints;
	public bool restart;
	public float delay;

	public Text _hargaSlowMo;
	public Text _hargaBounce;
	public Text _hargaAero;
	public Text _hargaMagnet;
	public Text _hargaSteel;

	public Text _slowmoUpText;
	public Text _bounceUpText;
	public Text _aeroUpText;
	public Text _magnetUpText;
	public Text _steelUpText;

	public Text _alertText;
	public Text _confirmText;
	public Text _cancelText;

	public Text _completeSlowmo;
	public Text _completeBounce;
	public Text _completeAero;
	public Text _completeMagnet;
	public Text _completeSteel;

	public Text slowmoLv;
	public Text bounceLv;
	public Text aeroLv;
	public Text magnetLv;
	public Text steelLv;

	public Image coinSlowmo;
	public Image coinBounce;
	public Image coinAero;
	public Image coinMagnet;
	public Image coinSteel;

    public Sprite[] unlockedLandmarks;
    public Sprite lockedLandmark;
    public Image[] landmarkGrids;
    public Button[] landmarkBtns;
    public Button reviveButton;
    public Button resetButton;

    //Sound



    // Reset Button
    private LandMarkGenerator landmarks;
    // Use this for initialization
    void Start () {
		_scoreManager = GameObject.FindObjectOfType<ScoreManager> ();
		_Karakter = GameObject.FindObjectOfType<KarakterSkrip> ();
        _upgradeManager = GameObject.FindObjectOfType<UpgradeManager>();
		_GM = GameObject.FindObjectOfType<GameManager> ();
        Interactable = FindObjectOfType<InteractableOff>();
        theSoundMan = FindObjectOfType<SoundManager>();
        theCamera = FindObjectOfType<CameraControl>();
        landmarks = FindObjectOfType<LandMarkGenerator>();
        ResetUI ();
        Screen.sleepTimeout = SleepTimeout.NeverSleep; //Supaya gk mati
        _mainMenu.SetActive(true);

		if (PlayerPrefs.HasKey ("SFXToggleIsOn") != false) {
			_soundFXstat = PlayerPrefs.GetInt ("SFXToggleIsOn");
		} else {
			_soundFXstat = 1;
		} 
			
		if (PlayerPrefs.HasKey ("BGMToggleIsOn") != false) {
			_BGMstat = PlayerPrefs.GetInt ("BGMToggleIsOn");
		} else {
			_BGMstat = 1;
		}
			
		_timerDelayRestart.enabled = false;
		DisablingUpgradeBtn ();

	}

    private void FixedUpdate()
    {

        if (_Karakter.respawnkahjurang)
        {
            //Debug.Log(Time.deltaTime);
            _Karakter.WaktuBlackCounter -= 0.02f;

            if (_Karakter.WaktuBlackCounter < 0.1f)
            {
                AdManager.Instance.ShowVideo();
                _GM.DeathRespawn();
                _Karakter.blackScreen.SetActive(false);
                _Karakter.respawnkahjurang = false;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		_currentDistance.text 	= _scoreManager.getCurrentDistance().ToString() + " m";
		_currentPoints.text 	= _scoreManager.getCurrentPoint().ToString();
		_highestPoints.text 	= _scoreManager.getHighestPoint ().ToString ();
		_collectedPoints.text 	= _scoreManager.getCollectedCoin ().ToString();
		_fartestDistance.text 	= _scoreManager.getFartestDistance().ToString() + " m";
		_timeToContinue.text	= "YOU HAVE " + Mathf.Round(timer).ToString () + " SECOND";
		_timerDelayRestart.text = Mathf.Round (delay).ToString ();

		_hargaSlowMo.text 		=_upgradeManager.hargaSlowMo.ToString();
		_hargaBounce.text 		=_upgradeManager.hargaBounce.ToString();
		_hargaAero.text 		=_upgradeManager.hargaAero.ToString();
		_hargaMagnet.text 		=_upgradeManager.hargaMagnet.ToString();
		_hargaSteel.text 		=_upgradeManager.hargaSteel.ToString();

		slowmoLv.text 			= "LV. " +_upgradeManager.getLevel ("slowmo").ToString () + "  OF  5";
		bounceLv.text			= "LV. " +_upgradeManager.getLevel ("bounce").ToString () + "  OF  10";
		aeroLv.text 			= "LV. " +_upgradeManager.getLevel ("aero").ToString () + "  OF  10";
		magnetLv.text 			= "LV. " +_upgradeManager.getLevel ("magnet").ToString () + "  OF  10";
		steelLv.text 			= "LV. " +_upgradeManager.getLevel ("steel").ToString () + "  OF  10";

		DisablingUpgradeBtn ();

		if (_death) {
			timer -= Time.deltaTime;
			if (timer <= 0.1f) {
				hideDeathMenu ();
			}
		}
			
		if(restart){
			_timerDelayRestart.enabled = true;
			delay -= Time.deltaTime;
			if(delay <= 1f){
				delay = 0f;
				_timerDelayRestart.enabled = false;
				_Karakter.isPlaying = true;
                _Karakter.isCamFollowing = true; //tambmah Ole
                restart = false;
                _GM.landMarkStoreOke = false;
                theSoundMan.BGMMain();

            }
		}


        if (respawnTimer)
        {
            _timerDelayRestart.enabled = true;
            delay -= Time.deltaTime;
            if (delay <= 1f)
            {
                delay = 0f;
                _timerDelayRestart.enabled = false;
                _Karakter.isPlaying = true;
                _Karakter.isCamFollowing = true; //tambmah Ole
                respawnTimer = false;
                _GM.landMarkStoreOke = false;
                theSoundMan.BGM = theSoundMan.beforeDeathBGM;
                theSoundMan.BGM.Stop();
                theSoundMan.BGM.volume = 1.0f;

                theSoundMan.BGM.loop = true;
                theSoundMan.BGM.Play();
            }
        }


        //tambah__________________________________________________________________________________________________________________________
        if (respawnRestart)
        {
            _tapToPlay.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                theCamera.samplingOffset();
                _tapToPlay.SetActive(false);
               
                respawnTimer = true;
                respawnRestart = false;
            }
        }

        if (_Karakter.isPlaying == true)
        {
            _CoinPoints.SetActive(true);
            _Distance.SetActive(true);
            _pauseButton.gameObject.SetActive(true);
        }
        else
        {
            _CoinPoints.SetActive(false);
            _Distance.SetActive(false);
            _pauseButton.gameObject.SetActive(false);
        }

        if (!landmarks.landmarkChecker(0))
        {
            resetButton.interactable = false;
        }
       
	}

	public void DisablingUpgradeBtn(){
		if (_upgradeManager.getLevel ("slowmo") >= 5f) {
			coinSlowmo.gameObject.SetActive (false);
			_hargaSlowMo.gameObject.SetActive (false);
			_slowmoUpText.gameObject.SetActive (false);
			_completeSlowmo.gameObject.SetActive (true);
			_slowmoUp.enabled = false;
		} 

		if (_upgradeManager.getLevel ("bounce") >= 10f) {
			coinBounce.gameObject.SetActive (false);
			_hargaBounce.gameObject.SetActive (false);
			_bounceUpText.gameObject.SetActive (false);
			_completeBounce.gameObject.SetActive (true);
			_bounceUp.enabled = false;
		}  

		if (_upgradeManager.getLevel ("aero") >= 10f) {
			coinAero.gameObject.SetActive (false);
			_hargaAero.gameObject.SetActive (false);
			_aeroUpText.gameObject.SetActive (false);
			_completeAero.gameObject.SetActive (true);
			_aeroUp.enabled = false;
		} 

		if (_upgradeManager.getLevel ("magnet") >= 10f) {
			coinMagnet.gameObject.SetActive (false);
			_hargaMagnet.gameObject.SetActive (false);
			_magnetUpText.gameObject.SetActive (false);
			_completeMagnet.gameObject.SetActive (true);
			_magnetUp.enabled = false;
		} 

		if (_upgradeManager.getLevel ("steel") >= 10f) {
			coinSteel.gameObject.SetActive (false);
			_hargaSteel.gameObject.SetActive (false);
			_steelUpText.gameObject.SetActive (false);
			_completeSteel.gameObject.SetActive (true);
			_steelUp.enabled = false;
		} 
	}

	public void ResetUI(){
		//mengambil Best value dari distance dan point
		_fartestDistance.text 	= _scoreManager.getFartestDistance().ToString() + " m";
		_highestPoints.text 	= _scoreManager.getHighestPoint().ToString();

		_currentDistance.text 	= _scoreManager.getCurrentDistance().ToString() + " m";
		_currentPoints.text 	= _scoreManager.getCurrentPoint().ToString();

		_collectedPoints.text 	= _scoreManager.getCollectedCoin ().ToString();

		_hargaSlowMo.text 		=_upgradeManager.hargaSlowMo.ToString();
		_hargaBounce.text 		=_upgradeManager.hargaBounce.ToString();
		_hargaAero.text 		=_upgradeManager.hargaAero.ToString();
		_hargaMagnet.text 		=_upgradeManager.hargaMagnet.ToString();
		_hargaSteel.text 		=_upgradeManager.hargaSteel.ToString();

		slowmoLv.text 			= "LV. " + _upgradeManager.getLevel ("slowmo").ToString () + "  OF  5";
		bounceLv.text			= "LV. " +_upgradeManager.getLevel ("bounce").ToString () + "  OF  10";
		aeroLv.text 			= "LV. " +_upgradeManager.getLevel ("aero").ToString () + "  OF  10";
		magnetLv.text 			= "LV. " +_upgradeManager.getLevel ("magnet").ToString () + "  OF  10";
		steelLv.text 			= "LV. " +_upgradeManager.getLevel ("steel").ToString () + "  OF  10";

		//Menonaktifkan semua tanda get power up
		_steelPowerUp.enabled 	= false;
		_steelTime.enabled 		= false;
		_bouncePowerUp.enabled 	= false;
		_bounceTime.enabled 	= false;
		_aeroPowerUp.enabled 	= false;
		_aeroTime.enabled 		= false;
		_magnetTime.enabled 	= false;
		_magnetPowerUp.enabled 	= false;
		_sloMoPowerUp.enabled 	= false;
		_sloMoTime.enabled 		= false;

		_death 					= false;
	}
		
	#region Death Menu
	public void showDeathMenu(){
		_deathMenu.SetActive (true);
		_pauseButton.gameObject.SetActive(false);
        _death = true;

        if (_GM.getReviveCounter() > 2)
        {   
            reviveButton.animator.SetBool("Disabled", true);
            reviveButton.enabled = false;
            hideDeathMenu();
        }
        else
        {
            reviveButton.animator.SetBool("Disabled", false);
            reviveButton.enabled = true;
        }
	}

	public void hideDeathMenu(){
		timer = 0f;
		_death = false;
		_deathMenu.SetActive (false);
        _Karakter.noAds.SetActive(false);
		showScoreBoard ();
	}
	#endregion

	#region Scoreboard
	public void showScoreBoard(){
		_yourPoints.text	 	= _currentPoints.text;
		_yourBestPoints.text 	= _highestPoints.text;
		_yourDistance.text 		= _currentDistance.text;
		_yourBestDistance.text	= _fartestDistance.text;
        _scoreManager.saveScore();
		_scoreBoard.SetActive (true);
	}
	public void hideScoreBoard(){
		_scoreBoard.SetActive (false);
	}
	#endregion

	#region Pause Menu
	public void showPauseMenu(){
		_pauseMenu.SetActive (true);
        _pauseButton.interactable = false;
	}

	public void hidePauseMenu(){
		_pauseMenu.SetActive (false);
        _pauseButton.interactable = true;
	}
	#endregion

	#region Main Menu
	public void showMainMenu(){
		hideScoreBoard ();
		hidePauseMenu ();
		_mainMenu.SetActive (true);
	}

	public void hideMainMenu(){
		_mainMenu.SetActive (false);
		//ResetUI ();
	}
	#endregion

	#region Upgrade Menu
	public void showUpgradeMenu(){
		_mainMenu.SetActive (false);
		_upgradeMenu.SetActive (true);
	}

	public void hideUpgradeMenu(){
		_upgradeMenu.SetActive (false);
		_mainMenu.SetActive (true);
	}
	#endregion

	#region Credits 
	public void showCredits(){
		_mainMenu.SetActive (false);
		_credits.SetActive (true);
	}

	public void hideCredits(){
		_credits.SetActive (false);
		_mainMenu.SetActive (true);
	}
    #endregion

    #region Panel Alerts
    public void showGotoMainAlert() {
        _confirmBtn.onClick.RemoveAllListeners();
        ResetAlertBtn();
        _alerts.SetActive(true);
        _alertText.text = "If you go back to main menu, your current score will not be saved !!";
        _confirmText.text = "continue";
        _cancelText.text = "cancel";
        _confirmBtn.onClick.AddListener(() => _GM.GoToMainMenu());
    }

    public void showAlertQuit(){
		_confirmBtn.onClick.RemoveAllListeners ();
		ResetAlertBtn ();
		_alerts.SetActive (true);
		_alertText.text = "Are you sure want to quit the game ?";
		_confirmText.text = "yes";
		_cancelText.text = "no";
		_confirmBtn.onClick.AddListener (() => _GM.QuitGame());
	}

    public void showAlertResetLandmark()
    {
        _confirmBtn.onClick.RemoveAllListeners();
        ResetAlertBtn();
        _alerts.SetActive(true);
        _alertText.text = "Are you sure want to reset landmarks progress ?";
        _confirmText.text = "yes";
        _cancelText.text = "no";
        _confirmBtn.onClick.AddListener(() => ResetLandmark());
    }




    public void showPuAlerts(string caller){
		_confirmBtn.onClick.RemoveAllListeners ();
		ResetAlertBtn ();
		_alerts.SetActive (true);

		if (_scoreManager.getCollectedCoin () < _upgradeManager.getHarga(caller) || _scoreManager.getCollectedCoin () <= 0) {
			_alertText.text = "Insufficient funds ! \n You currently only have " + _scoreManager.getCollectedCoin ().ToString () + " coin.\n you need " + _upgradeManager.getHarga(caller).ToString () + " for upgrade this item.";
			_cancelBtn.gameObject.SetActive (false);
			_confirmBtn.gameObject.SetActive (false);
			_singleBtn.gameObject.SetActive (true);
		} else {
			switch(caller){
			case "slowmo":
				_alertText.text = "Do you want to upgrade slow motion timer by 1 second for " + _hargaSlowMo.text + " ?";
				_confirmText.text = "confirm";
				_cancelText.text = "cancel";
				_confirmBtn.onClick.AddListener (() => {
					_upgradeManager.SlowMoUpgrade ();
					SuccessUpgradeMessage ("slowmo");
				});
				break;
			case "bounce":
				_alertText.text = "Do you want to upgrade bouncing timer by 1 second for " + _hargaBounce.text + " ?";
				_confirmText.text = "confirm";
				_cancelText.text = "cancel";
				_confirmBtn.onClick.AddListener (() => {
					_upgradeManager.BouncenessUpgrade(); 
					SuccessUpgradeMessage("bounce");
				});
				break;
			case "aero":
				_alertText.text = "Do you want to upgrade aerodynamic timer by 1 second for " + _hargaAero.text + " ?";
				_confirmText.text = "confirm";
				_cancelText.text = "cancel";
				_confirmBtn.onClick.AddListener (() => {
					_upgradeManager.AeroUpgrade(); 
					SuccessUpgradeMessage("aero");
				});
				break;
			case "magnet":
				_alertText.text = "Do you want to upgrade magnetic effect timer by 1 second for " + _hargaMagnet.text + " ?";
				_confirmText.text = "confirm";
				_cancelText.text = "cancel";
				_confirmBtn.onClick.AddListener (() => {
					_upgradeManager.MagnetUpgrade(); 
					SuccessUpgradeMessage("magnet");
				});
				break;
			case "steel":
				_alertText.text = "Do you want to upgrade steel power timer by 1 second for " + _hargaSteel.text + " ?";
				_confirmText.text = "confirm";
				_cancelText.text = "cancel";
				_confirmBtn.onClick.AddListener (() => {
					_upgradeManager.SteelUpgrade(); 
					SuccessUpgradeMessage("steel");
				});
				break;
			default:
				_confirmBtn.onClick.AddListener (() => hideAlerts ());
				break;
			}
		}
	}
		
	public void SuccessUpgradeMessage(string caller){
		ResetAlertBtn ();
		_alerts.SetActive (true);
		switch(caller){
		case "slowmo":
			_alertText.text = "upgrade success ! \n Your Slow Motion timer currently on level " + _upgradeManager.getLevel(caller).ToString () + "  of 5.\n thank you !";
			_cancelBtn.gameObject.SetActive (false);
			_confirmBtn.gameObject.SetActive (false);
			_singleBtn.gameObject.SetActive (true);
			break;
		case "bounce":
			_alertText.text = "upgrade success ! \n Your bouncing timer currently on level " + _upgradeManager.getLevel(caller).ToString () + "  of 10.\n thank you !";
			_cancelBtn.gameObject.SetActive (false);
			_confirmBtn.gameObject.SetActive (false);
			_singleBtn.gameObject.SetActive (true);
			break;
		case "aero":
			_alertText.text = "upgrade success ! \n Your aerodynamic timer currently on level " + _upgradeManager.getLevel(caller).ToString () + "  of 10.\n thank you !";
			_cancelBtn.gameObject.SetActive (false);
			_confirmBtn.gameObject.SetActive (false);
			_singleBtn.gameObject.SetActive (true);
			break;
		case "magnet":
			_alertText.text = "upgrade success ! \n Your magnetic effect timer currently on level " + _upgradeManager.getLevel(caller).ToString () + "  of 10.\n thank you !";
			_cancelBtn.gameObject.SetActive (false);
			_confirmBtn.gameObject.SetActive (false);
			_singleBtn.gameObject.SetActive (true);
			break;
		case "steel":
			_alertText.text = "upgrade success ! \n Your Steel power timer currently on level " + _upgradeManager.getLevel(caller).ToString () + "  of 10.\n thank you !";
			_cancelBtn.gameObject.SetActive (false);
			_confirmBtn.gameObject.SetActive (false);
			_singleBtn.gameObject.SetActive (true);
			break;
		default:
			_alerts.SetActive (false);
			break;
		}
	}
		
	public void hideAlerts(){
		_alerts.SetActive (false);
	}

	void ResetAlertBtn(){
		_cancelBtn.gameObject.SetActive (true);
		_confirmBtn.gameObject.SetActive (true);
		_singleBtn.gameObject.SetActive (false);
	}
    #endregion

    #region Landmarks
    public void showLandmarkMenu()
    {
        _mainMenu.SetActive(false);
        _landmarkMenu.SetActive(true);
    }

    public void hideLandmarkMenu()
    {   
        _landmarkMenu.SetActive(false);
        _mainMenu.SetActive(true);
        for (int i = 0; i < Interactable.InfoPanel.Length; i++)
        {
            Interactable.InfoPanel[i].GetComponent<Image>().enabled = false;

            Interactable.InfoPanel[i].transform.GetChild(0).GetComponent<Text>().enabled = false;
        }
    }
    #endregion

    #region Landmarks Info
    public void HasLandmark(string landmark, int urutan)
    {//1000053055
        if (PlayerPrefs.GetInt(landmark) == 0)
        {
            landmarkGrids[urutan].sprite = lockedLandmark;
            landmarkBtns[urutan].onClick.RemoveAllListeners();
        }
        else
        {
            landmarkGrids[urutan].sprite = unlockedLandmarks[urutan];
            landmarkBtns[urutan].onClick.AddListener(() => {
                Interactable.InfoPanel[urutan].enabled = true;
                Interactable.InfoPanel[urutan].transform.GetChild(0).GetComponent<Text>().enabled = true; //Tulisanya juga true
                Interactable.setInfoPanelKeluar(Interactable.InfoPanel[urutan]); // Jika salah satu on atau di pencet maka keluar dan jika keluar
            });
        }
    }

    public void ResetLandmark()
    {
        landmarks.resetProgress = true;
    }
    #endregion





}