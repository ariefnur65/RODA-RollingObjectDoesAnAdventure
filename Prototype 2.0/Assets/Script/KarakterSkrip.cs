using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KarakterSkrip : MonoBehaviour
{
    //Bagian Variable
    // Tutorial Manager
    public int stateSwitch;
    public bool jalanNormal;
    public static bool jumpSwitch;
    public static bool speedSwitch;
    public bool nep = false;
    private SoundManager theSoundMan;
    public CameraControl myCamController;

	private GameManager _gameManager;
    private UIManager _uiManager;

    public GameObject StartBarrier;
    public float lompatGaya;
    public GameObject start;
	private Rigidbody2D myRigid;
    public float gayaDorong;
    public float gayaDorongStore; //Gayadorong sebelum kena oli atau lumpur
    public float gayaDorongBase; // GayaDorong Base
    public bool udahKena;
    public float gayaBawah;
    public float kecepatanGelinding;
    public bool tanahKah;
    public bool duriKah;
    public bool barrierkah;
    private Collider2D myColider;
    public LayerMask apaItuTanah;
    public LayerMask apaItuDuri;
    public LayerMask apaItuBarrier;
	private float jarak;
    public static float jarakSpeedUp;
    public float jarakSpeedUpBase;
    public float SpeedMulti;
    public float speedMaks;
    //Menu Var Mau dipindah di Game Manager kalo udah selesai
    private PlatformDestroyer[] daftarGunung;
    private PlatformGeneration platformGenerator;

    //Manipulasi Gravitasi
    public float gravityX;
    public float gravityY;
    private int cek;

    //PowerUp
    public PhysicsMaterial2D[] bounceMaterial;
	public PowerManager thePowerManager;
	public float slowMoTime;
	public float magnetTime;
	public float steelTime;
	public float bouncingTime;
	public float aeroTime;

    public bool isPlaying;
    // Use this for initialization

    //Partikel Standard
    public GameObject DirtPartikel;
    public GameObject SmokePartikel;
    public GameObject OilPartikel;
    public GameObject MudPartikel;

    //Partikel PowerUp
    public GameObject PUMagnetPartikel;
    public GameObject PUAeroPartikel;
    public GameObject PUAero2Partikel;
    public GameObject PUBouncePartikel;
    public GameObject PUSteelPartikel;
    //public GameObject PUSlowPartikel;

    public int StatusPartikel;
    /*0 = Semua Hilang
      1 = Menyentuh Tanah
      2 = Menyentuh Oli
      3 = Menyentuh Lumpur
      */
    public int CekPartikel;
    /*1 = Oli true
      2 = Lumpur true*/

    private float WaktuPartikelCounter;
    public float WaktuBlackCounter;
    public float WaktuHapusPartikel;
    public int hilangkanPUPartikel;

    //InfoPanel Appear
    private InteractableOff Interactable;
    private bool adaLandMark;
    private LandMarkGenerator theLandMarkGenerator;


    //Camera Control stop
    public bool isCamFollowing;
    public Vector3 deathPosition;

    //Tutorial
    private TutorialSwitch theTutorialSwitch;

    //Death Spawn
    private bool matiOlehJurangKah; //Paku or Jurang
    public GameObject blackScreen;
    public bool respawnkahjurang;
    public bool respawnkahpaku;

    //tambah partikel
    private float WaktuPartikelObsCounter;

    private int tutorialCam;

    public bool steelkah;


    //Tambah Oli dan Lumpur
    private bool kenaOli;
    private bool kenaLumpur;
    private bool timerLumpur;
    private bool timerOli;
    private float delayLumpur;
    private float delayOli;

    //Tambhn kena Barrier
    public bool kenaBarrier;

    public GameObject noAds;

    //Death Spawn Tambahan
    private GameObject pakuToOff;
    /*
     
         */

    void Start()
    {
        tutorialCam = 0;
        //Partikel Status
        StatusPartikel = 0;
        hilangkanPUPartikel = 1;
        steelkah = false;
        kenaBarrier = false;
        matiOlehJurangKah = false;
        WaktuPartikelCounter = WaktuHapusPartikel;
        WaktuBlackCounter = 4;
        myRigid = GameObject.FindObjectOfType<Rigidbody2D> ();
		_gameManager = FindObjectOfType<GameManager> ();
        _uiManager = FindObjectOfType<UIManager>();
        theSoundMan = FindObjectOfType<SoundManager>();
        theLandMarkGenerator = FindObjectOfType<LandMarkGenerator>();

        //thePowerManager = FindObjectOfType<PowerManager>();
        thePowerManager.ResetPowerUp ();
        cek = 1;
        
        gayaDorongStore = gayaDorongBase; //Menginisialisasi Gaya dorong Awal
        gayaDorong = gayaDorongStore; // Menginisialisasi Gaya dorong Awal

        //jarakSpeedUp = jarakSpeedUpBase;
        jarakSpeedUp = theLandMarkGenerator.getJarakNyataLandMark();

        myColider = GetComponent<Collider2D>();
        platformGenerator = FindObjectOfType<PlatformGeneration>(); //Mendapatkan Obejct
        isPlaying = false;

        Interactable = FindObjectOfType<InteractableOff>();

        deathPosition = myCamController.transform.position;

        // Void Start
        //Tutorial TextBox
        jalanNormal = true;
        speedSwitch = true;
        jumpSwitch = false;
        stateSwitch = 0;

        theTutorialSwitch = FindObjectOfType<TutorialSwitch>();


    }


    private void FixedUpdate()
    {
        //Void FixedUpdate
        //Normal Switch

        if (theTutorialSwitch.tutorialSwitch == true)
        {
            jalanNormal = false;
        }
        else
        {
            jalanNormal = true;
        }
        /*
        if (respawnkahjurang)
        {
            //Debug.Log(Time.deltaTime);
            WaktuBlackCounter -= 0.02f;

            if (WaktuBlackCounter < 0.1)
            {
                AdManager.Instance.ShowVideo();
                _gameManager.DeathRespawn();
                blackScreen.SetActive(false);
                Debug.Log("BlackScreenHilang");
                respawnkahjurang = false;
            }
        }*/

        /*
        if (respawnkahpaku)
        {
            Debug.Log("BlackScreenHilang101" + WaktuBlackCounter);

            WaktuBlackCounter -= Time.deltaTime;
            Debug.Log("BlackScreenHilang102" + WaktuBlackCounter);

            if (WaktuBlackCounter < 0.1)
            {
                Debug.Log("BlackScreenHilang102");
                AdManager.Instance.ShowVideo();
                _gameManager.DeathRespawn();
                blackScreen.SetActive(false);
                Debug.Log("BlackScreenHilang2");
                respawnkahpaku = false;
            }
        }*/

        if (isPlaying)
        {
            JalanTerusCuyy();
        }
        else
        {
            StopCuy();
        }
       
        switch (hilangkanPUPartikel)
        {
            case 1: PUMagnetPartikel.SetActive(false);
                PUAeroPartikel.SetActive(false);
                PUAero2Partikel.SetActive(false);
                //PUSlowPartikel.SetActive(false);
                PUSteelPartikel.SetActive(false);
                PUBouncePartikel.SetActive(false);
                break;
        }

        switch (StatusPartikel)
        {
            case 0: //Semua False
                DirtPartikel.SetActive(false);
                SmokePartikel.SetActive(false);
                OilPartikel.SetActive(false);
                MudPartikel.SetActive(false);
                break;

            case 1: //Menyentuh Tanah
                DirtPartikel.SetActive(true);
                SmokePartikel.SetActive(true);
                OilPartikel.SetActive(false);
                MudPartikel.SetActive(false);
                break;

            case 2: //Menyentuh Oli
                DirtPartikel.SetActive(false);
                SmokePartikel.SetActive(false);
                OilPartikel.SetActive(true);
                MudPartikel.SetActive(false);
                break;

            case 3: //Menyentuh Lumpur
                DirtPartikel.SetActive(false);
                SmokePartikel.SetActive(false);
                OilPartikel.SetActive(false);
                MudPartikel.SetActive(true);
                break;

        }


        if (jalanNormal)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (tanahKah)
            {
                    //gravityX = gayaDorong;
                    //theSoundMan.jumpSound.Play();
                myRigid.velocity = new Vector2(lompatGaya, lompatGaya);
                //Physics2D.gravity = new Vector2(gravityX, gravityY); //Menset gravityX dan gravityY
                cek = 0;
                StatusPartikel = 0;
            }

        }
        jarak = gameObject.transform.position.x - start.transform.position.x; //Jarak = jarak dari game object ke start
        if(isPlaying) myRigid.velocity = new Vector2(gayaDorong, myRigid.velocity.y); //Rigidbody Player di beri speed ketika isPlaying true
        kecepatanGelinding = myRigid.velocity.x; //Speed dicatat
        tanahKah = Physics2D.IsTouchingLayers(myColider, apaItuTanah); //Jika touching tanah maka dia bisa loncat
                                                                       //Jika touching tanah maka akan di tambah physics2D nya

        if (tanahKah)
        {
            WaktuPartikelCounter = WaktuHapusPartikel;
                if (CekPartikel == 1)//Oli True
                {
                    StatusPartikel = 2;
                    WaktuPartikelObsCounter -= Time.deltaTime;
                    if (WaktuPartikelObsCounter < 0.1)
                    {
                        CekPartikel = 0;
                    }

                }
                else if (CekPartikel == 2)//Lumpur True
                {
                    StatusPartikel = 3;
                    WaktuPartikelObsCounter -= Time.deltaTime;
                    if (WaktuPartikelObsCounter < 0.1)
                    {
                        CekPartikel = 0;
                    }
                }
                else
            {
                StatusPartikel = 1;
            }
            cek = 1;
        }

        if (!tanahKah)
        {
            WaktuPartikelCounter -= Time.deltaTime;
            if (WaktuPartikelCounter < 0.1)
            {
                StatusPartikel = 0;
            }
        }

        
        if (cek == 1)
        {
            Physics2D.gravity = new Vector2(gravityX, gravityY);
            myCamController.iszoom = true;
        }
        if (cek == 0)
        {
			if (thePowerManager.getAeroStatus() == true)
            {
                Physics2D.gravity = new Vector2(0f, -4.0f);
            }
            else
            {
                Physics2D.gravity = new Vector2(0f, -9.81f);
            }
            myCamController.iszoom = false;
        }
        }
        else
        {
            if (speedSwitch && jumpSwitch)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                {
                    if (tanahKah)
                    {
                        //theSoundMan.jumpSound.Play();

                        //gravityX = gayaDorong;
                        myRigid.velocity = new Vector2(lompatGaya, lompatGaya);
                        //Physics2D.gravity = new Vector2(gravityX, gravityY); //Menset gravityX dan gravityY
                        cek = 0;
                    }
                }


                jarak = gameObject.transform.position.x - start.transform.position.x; //Jarak = jarak dari game object ke start
                myRigid.velocity = new Vector2(gayaDorong, myRigid.velocity.y); //Rigidbody Player di beri speed
                kecepatanGelinding = myRigid.velocity.x; //Speed dicatat
                tanahKah = Physics2D.IsTouchingLayers(myColider, apaItuTanah); //Jika touching tanah maka dia bisa loncat
                                                                               //Jika touching tanah maka akan di tambah physics2D nya
                if (tanahKah)
                {
                    cek = 1;
                }

                if (cek == 1)
                {
                    Physics2D.gravity = new Vector2(gravityX, gravityY);
                    myCamController.iszoom = true;
                }
                if (cek == 0)
                {
                    if (thePowerManager.getAeroStatus() == true)
                    {
                        Physics2D.gravity = new Vector2(0f, -4.0f);
                    }
                    else
                    {
                        Physics2D.gravity = new Vector2(0f, -9.81f);
                    }
                    myCamController.iszoom = false;
                }
            }
            else if (speedSwitch)
            {
                jarak = gameObject.transform.position.x - start.transform.position.x; //Jarak = jarak dari game object ke start
                myRigid.velocity = new Vector2(gayaDorong, myRigid.velocity.y); //Rigidbody Player di beri speed
                kecepatanGelinding = myRigid.velocity.x; //Speed dicatat
                tanahKah = Physics2D.IsTouchingLayers(myColider, apaItuTanah); //Jika touching tanah maka dia bisa loncat
                myCamController.iszoom = true;
                //Jika touching tanah maka akan di tambah physics2D nya
                Physics2D.gravity = new Vector2(gravityX, gravityY);
            }
            else
            {
                myRigid.velocity = new Vector2(0f, 0f);
                Physics2D.gravity = new Vector2(0f, 0f);
                myCamController.iszoom = true;
                myRigid.rotation = 0;
            }


            // State Counter
            if (stateSwitch == 1)
            {
              //  Debug.Log("Text CoinGoo2");

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                {

                    theTutorialSwitch.textCoin_1 = false;
                //    Debug.Log("Text CoinGoo3");
                    theTutorialSwitch.textCoin_2 = true;
                    nep = true;
                }
                else if (nep)
                {
                    stateSwitch = 2;
                }
            }

            if (stateSwitch == 2)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                {
                    nep = false;
                    theTutorialSwitch.textCoin_2 = false;
                    speedSwitch = true;
                }
            }

            if (stateSwitch == 3)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                {

                    theTutorialSwitch.textPaku_1 = false;
                    speedSwitch = true;
                    jumpSwitch = true;
                }
            }

            if (stateSwitch == 4)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                {

                    theTutorialSwitch.textPenandaJurang_1 = false;
                    speedSwitch = true;
                    jumpSwitch = false;
                    nep = true;
                }
                else if (nep)
                {
                    stateSwitch = 5;
                }

            }
            if (stateSwitch == 5)
            {
                speedSwitch = true;
                jumpSwitch = true;
            }
        }

        if (udahKena)

        {
            if (kenaOli)
            {

                if (gayaDorong >= gayaDorongStore * 2.5)
                {
                    if (timerOli)
                    {
                        delayOli -= Time.deltaTime;
                  //      Debug.Log("Delay Oli:" + delayOli);
                        if (delayOli <= 1f)
                        {
                    //        Debug.Log("Ore");

                            kenaOli = false;
                            timerOli = false;

                            if (!kenaOli)
                            {
                                gayaDorong = gayaDorongStore;
                      //          Debug.Log("Harusnya selesei" + gayaDorong);
                                OilPartikel.SetActive(false);

                                if (gayaDorong <= gayaDorongStore)
                                {
                                    udahKena = false;
                                }
                            }
                        }
                    }


                }
                else
                {
                    gayaDorong = Mathf.MoveTowards(gayaDorong, gayaDorong * 2.5f, Time.deltaTime * 150);

                    timerOli = true;
                    delayOli = 2f;
                }


            }

            if (kenaLumpur)
            {

                if (gayaDorong <= 2f)
                {
                    if (timerLumpur)
                    {
                        delayLumpur -= Time.deltaTime;
                        if (delayLumpur <= 1f)
                        {

                            kenaLumpur = false;
                            timerLumpur = false;
                            if (!kenaLumpur)
                            {
                                gayaDorong = gayaDorongStore;
                                Debug.Log("Harusnya selesei" + gayaDorong);
                                MudPartikel.SetActive(false);

                                if (gayaDorong <= gayaDorongStore)
                                {
                                    udahKena = false;

                                }
                            }
                        }
                    }


                }
                else
                {
                    gayaDorong = Mathf.MoveTowards(gayaDorong, 2, Time.deltaTime * 150);
                    timerLumpur = true;
                    delayLumpur = 4f;
                }

            }
        }

    }
    // Update is called once per frame
    void Update()
    {
            //SpeedingUp gaya dorong ditambah ketika jarak melebihi jarak landmark yang sedang tampil maka naik speednya
            if (jarak > jarakSpeedUp) //theLandMarkGenerator
            {
            gayaDorong = gayaDorong * SpeedMulti;
                gayaDorongStore = gayaDorongStore * SpeedMulti;
            
                
                jarakSpeedUp = theLandMarkGenerator.getJarakNyataLandMark();
                
            }

        if (gayaDorong >= speedMaks && !udahKena)
        {
            gayaDorong = speedMaks;
        }
        if (kenaBarrier)
            {
                gayaDorong = -1;
            }


        


    }

    public void ResetKarakterScript()
    {
        gayaDorongStore = gayaDorongBase; //Menginisialisasi Gaya dorong Awal
        gayaDorong = gayaDorongStore; // Menginisialisasi Gaya dorong Awal
        jarakSpeedUp = theLandMarkGenerator.getJarakLandmark(0); 
        cek = 1;
        transform.position = start.transform.position;
        myRigid.gameObject.SetActive(true);
        platformGenerator.transform.position = platformGenerator.field.transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paku"))
        {
            switch (steelkah)
            {
                case true:
                    other.gameObject.SetActive(false);
                    theSoundMan.pakuhancur.Play();
                    break;
                default:
                    if (udahKena || kenaBarrier)
                    {
                        gayaDorong = gayaDorongStore;
                        udahKena = false;
                    }
                    thePowerManager.slowMoTime = 0;
                    thePowerManager.steelTime = 0;
                    thePowerManager.magnetTime = 0;
                    thePowerManager.aeroTime = 0;
                    thePowerManager.bouncingTime = 0;
                    DirtPartikel.SetActive(false);
                    SmokePartikel.SetActive(false);
                    OilPartikel.SetActive(false);
                    MudPartikel.SetActive(false);
                    kenaBarrier = false;
                    kenaLumpur = false;
                    kenaOli = false;
                    _gameManager.Death();
                    isCamFollowing = false;
                    myCamController.isDeath = true;
                    myRigid.gameObject.SetActive(false);
                    pakuToOff = other.gameObject;
                    deathPosition = new Vector3(transform.position.x, transform.position.y + 2, deathPosition.z); // DeathPosition diganti menjadi tempat terakhir mati
                    matiOlehJurangKah = false;
                    break;
            }
        }

       
        barrierkah = other.gameObject.CompareTag("Barrier");
        if ( barrierkah ) {
            //deathChar();
            //_gameManager.DestroyGeneratedObjects();
            if (!udahKena || !kenaBarrier) gayaDorongStore = gayaDorong;
            udahKena = true;
            kenaBarrier = true;
            //platformGenerator.getStartPosition();
            //transform.position = start.transform.position;
        }


        if (other.gameObject.CompareTag("Death")) {
            //deathChar();
            //_gameManager.DestroyGeneratedObjects();
            if (udahKena||kenaBarrier)
            {
                gayaDorong = gayaDorongStore;
                udahKena = false;
            }
            thePowerManager.slowMoTime = 0;
            thePowerManager.steelTime = 0;
            thePowerManager.magnetTime = 0;
            thePowerManager.aeroTime = 0;
            thePowerManager.bouncingTime = 0;
            DirtPartikel.SetActive(false);
            SmokePartikel.SetActive(false);
            OilPartikel.SetActive(false);
            MudPartikel.SetActive(false);
            kenaBarrier = false;
            kenaLumpur = false;
            kenaOli = false;

            _gameManager.Death();
            myCamController.isDeath = true;
            myRigid.gameObject.SetActive(false);
            matiOlehJurangKah = true;
			//platformGenerator.getStartPosition();
			//transform.position = start.transform.position;
        }

        if (other.gameObject.CompareTag("JurangDeathTutorial"))
        {
            transform.localPosition = new Vector3(68, -43, -1);
        }

        if (other.gameObject.CompareTag("BarrierTutorial"))
        {
            transform.localPosition = new Vector3(68, -43, -1);
        }
        if (other.gameObject.CompareTag("pakuTutorial"))
        {
            transform.localPosition = new Vector3(38, -30, -1);
        }

    }

    //Death Respawn
    public void deathRespawn()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            noAds.SetActive(true);
        }
        else
        {
            AdManager.Instance.ShowVideo();
            blackScreen.SetActive(true);
            WaktuBlackCounter = 4;
            respawnkahjurang = true;
            isPlaying = false;
            _uiManager._death = false;
            if (matiOlehJurangKah)
            {
                transform.position = new Vector3(deathPosition.x + 5, deathPosition.y + 2, deathPosition.z);
                myRigid.gameObject.SetActive(true);
            }
            else
            {
                //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                pakuToOff.SetActive(false);
                myRigid.gameObject.SetActive(true);
            }

        }


        /*
        if (matiOlehJurangKah = false)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("Error. Check internet connection!");
                noAds.SetActive(true);
            }
            else
            {
                transform.localPosition = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
                myRigid.gameObject.SetActive(true);

                AdManager.Instance.ShowVideo();
                blackScreen.SetActive(true);
                WaktuBlackCounter = 1;
                respawnkahpaku = true;
            }
        }
        else if (matiOlehJurangKah = true)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("Error. Check internet connection!");
                noAds.SetActive(true);
            }
            else
            {
                transform.localPosition = new Vector3(transform.position.x + 2, transform.position.y + 9, transform.position.z);
                myRigid.gameObject.SetActive(true);

                AdManager.Instance.ShowVideo();
                blackScreen.SetActive(true);
                WaktuBlackCounter = 1;
                respawnkahjurang = true;
            }
        }*/
    }

    public void hideNoAds()
    {
        noAds.SetActive(false);
    }

    public float getDistance(){
		return Mathf.Round (jarak);
	}

	// Script Untuk Oli Dan Lumpur
	void OnTriggerEnter2D(Collider2D other) // Saad masuk ke dalam Collider bentuk trigger
    {
        #region Tutorial Trigger

        if (other.gameObject.CompareTag("TandaCoin_2"))
        {
            //Berhenti 
            speedSwitch = false;
            theTutorialSwitch.textCoin_1 = true;

            stateSwitch = 1;
        }

        if (other.gameObject.CompareTag("TandaPaku_1"))
        {
            speedSwitch = false;
            theTutorialSwitch.textPaku_1 = true;
            stateSwitch = 3;
        }

        if (other.gameObject.CompareTag("TandaPenandaJurang_1"))
        {
            speedSwitch = false;
            theTutorialSwitch.textPenandaJurang_1 = true;
            stateSwitch = 4;
        }

        if (other.gameObject.CompareTag("TandaJurang_2"))
        {
            theTutorialSwitch.tutorialSwitch = false;
            PlayerPrefs.SetInt("Tutorial", 1);

        }

        #endregion

        if (other.gameObject.CompareTag("Landmark"))
        {
            string namaLandmark = other.gameObject.name;
            int nilaiLandmark = PlayerPrefs.GetInt(namaLandmark);
            int urutanLandmark = theLandMarkGenerator.urutanLandmarkSedangMuncul;

            if (PlayerPrefs.GetInt(namaLandmark) == 0) //Belum Pernah atau telah direset
            {
                Interactable.InfoPanelKeluar(namaLandmark, urutanLandmark);
                _uiManager.HasLandmark(namaLandmark, urutanLandmark);
            }
        }

        if (other.gameObject.CompareTag("JurangStop"))
        {
            if (tutorialCam == 0)
            {
                isCamFollowing = false;
                deathPosition = other.transform.position;
            }
            else
            {
            }
        }

        if (other.gameObject.CompareTag("xForTutorCam"))
        {
            tutorialCam = 1;

        }

        if (other.gameObject.CompareTag("yForTutorCam"))
        {
            tutorialCam = 0;
        }



        #region Obstacle Trigger
        if (other.gameObject.CompareTag ("Oli")) 
		{
            WaktuPartikelObsCounter = 2;
            CekPartikel = 1;
            if (!udahKena)
            {
                gayaDorongStore = gayaDorong; udahKena = true;
                kenaOli = true;
            }//Jika belum kena maka gaya dorong store diisi dengan gaya dorong saat itu, jika tidak maka gayaDorongStore tidak diganti
           
            //gayaDorong = 15;
            //gayaDorong = Mathf.Lerp(gayaDorong, gayaDorong * 2.7f, Time.deltaTime * 20);
		 }
		if (other.gameObject.CompareTag ("Lumpur")) 
		{
            WaktuPartikelObsCounter = 2;
            CekPartikel = 2;
            if (!udahKena)
            {

                gayaDorongStore = gayaDorong; udahKena = true;
                kenaLumpur = true;
            }
            //gayaDorong = 3;
            //gayaDorong = Mathf.Lerp(gayaDorong, 1f, Time.deltaTime * 20);
           // StartCoroutine(SpeedDown ()); // Masuk ke menu timer IENumerator
        }
        #endregion

        #region Power UP Trigger
        if (isPlaying) {

            if (other.gameObject.CompareTag("PUSteel"))
            {
                theSoundMan.powerUpSound.Play();
                steelkah = true;
                other.gameObject.SetActive(false);
                thePowerManager.steelTime = steelTime;
                thePowerManager.setSteelStatus(true);
            }
            if (other.gameObject.CompareTag("Bounce"))
            {
                theSoundMan.powerUpSound.Play();

                other.gameObject.SetActive(false);
                thePowerManager.bouncingTime = bouncingTime;
                thePowerManager.setBounceStatus(true);
            }
            if (other.gameObject.CompareTag("Aero"))
            {
                theSoundMan.powerUpSound.Play();

                other.gameObject.SetActive(false);
                thePowerManager.aeroTime = aeroTime;
                thePowerManager.setAeroStatus(true);
            }
            if (other.gameObject.CompareTag("Magnet"))
            {
                theSoundMan.powerUpSound.Play();

                other.gameObject.SetActive(false);
                thePowerManager.magnetTime = magnetTime;
                thePowerManager.setMagnetStatus(true);
            }
            if (other.gameObject.CompareTag("SlowMotion"))
            {
                theSoundMan.powerUpSound.Play();

                other.gameObject.SetActive(false);
                thePowerManager.slowMoTime = slowMoTime;
                thePowerManager.setSlowMotionStatus(true);
            }
        }
        
        #endregion

    }


    #region Speed Up dan Down
    //Buat Timer Menggunakan fitur IEnumerator
    IEnumerator SpeedUp () 
	{        
      //  udahKena = true;
       // kenaOli = true;
        yield return new WaitForSeconds (3); // Menunggu delai 1 detik
       // kenaOli = false;
    //   Debug.Log("Kena Oli cuyy");
  //      udahKena = false;
	}
	IEnumerator SpeedDown ()
    {
      //  udahKena = true;
      //  gayaDorong = Mathf.MoveTowards(gayaDorong, 2, Time.deltaTime * 30);
        yield return new WaitForSeconds(2); // Menunggu delai 1 detik
      //  gayaDorong = Mathf.MoveTowards(gayaDorong, gayaDorongStore, Time.deltaTime * 200);
      //  udahKena = false;
    }
    IEnumerator PartikelTimer()
    {
        yield return new WaitForSeconds(2);
        CekPartikel = 0;
    }

    IEnumerator Timer(float waktu)
    {
        yield return new WaitForSeconds(waktu);
    }


    //Tambah Untuk Death Respawn
    /*IEnumerator DeathTimer()
    {
        AdManager.Instance.ShowVideo();
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        AdManager.Instance.ShowVideo();
        _gameManager.DeathRespawn();
        blackScreen.SetActive(false);
        Debug.Log("BlackScreenHilang");

    }*/
    #endregion

    private void StopCuy()
    {
        //Membuat rigid body ora jalan
        myRigid.constraints = RigidbodyConstraints2D.FreezePosition;
        StartBarrier.SetActive(true);
        
    }

    private void JalanTerusCuyy()
    {
        StartBarrier.SetActive(false);
        myRigid.constraints = RigidbodyConstraints2D.None;

    }

    public float getMyVelocity()
    {
        return myRigid.velocity.x;
    }

    public float getGayaDorong()
    {
        return gayaDorong;
    }

    public float getGayaDorongStore()
    {
        return gayaDorongStore;
    }

}






