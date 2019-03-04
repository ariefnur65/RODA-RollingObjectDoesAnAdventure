using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour {

    private KarakterSkrip thePlayer;
    private SlowMotionPowerUp theSlowMo;
    private MagnetPowerUp theMagnet;
	private UIManager theUIManager;
	public GameObject LayerSlowMo;
	private CameraControl theCameraController;
    private Camera theCamera;
	private Rigidbody2D thePlayerRigid;

    public float slowMoTime;
    public float magnetTime;
    public float steelTime;
    public float bouncingTime;
    public float aeroTime;

	private bool steelStat;
	private bool bounceStat;
	private bool aeroStat;
	private bool slowMoStat;
	private bool magnetStat;


    // Use this for initialization
    void Start () {
        thePlayer		 	= GameObject.FindObjectOfType<KarakterSkrip>();
		theUIManager 		= GameObject.FindObjectOfType<UIManager>();
        theSlowMo 			= GameObject.FindObjectOfType<SlowMotionPowerUp>();
        theMagnet 			= GameObject.FindObjectOfType<MagnetPowerUp>();
        theCamera 			= GameObject.FindObjectOfType<Camera>();
		theCameraController	= GameObject.FindObjectOfType<CameraControl> ();
		thePlayerRigid 		= thePlayer.GetComponent<Rigidbody2D> ();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //setiap power dicek stiap frame
        if (steelStat) 	SteelPowerUp();
        if (bounceStat) BouncingPowerUp();
        if (aeroStat) 	AeroPowerUp();
        if (slowMoStat) SlowMotionPowerUp();
        if (magnetStat)	MagnetPowerUp();
    }

    public void setSteelStatus(bool _status){
		steelStat = _status;
	}
	public void setAeroStatus(bool _status){
		aeroStat = _status;
	}
	public void setBounceStatus(bool _status){
		bounceStat = _status;
	}
	public void setMagnetStatus(bool _status){
		magnetStat = _status;
	}
	public void setSlowMotionStatus(bool _status){
		slowMoStat = _status;
	}

	public bool getSteelStatus(){
		return steelStat;
	}
	public bool getAeroStatus(){
		return aeroStat;
	}
	public bool getBounceStatus(){
		return bounceStat;
	}
	public bool getMagnetStatus(){
		return magnetStat;
	}
	public bool getSlowMotionStatus(){
		return slowMoStat;
	}

	#region Tambahan
	public void ResetPowerUp(){
		setSteelStatus (false);
		setBounceStatus (false);
		setAeroStatus (false);
		setMagnetStatus (false);
		setSlowMotionStatus (false);
	}
	#endregion

    #region CountDown PowerUP
    public void SlowMotionPowerUp()
    {
        thePlayer.hilangkanPUPartikel = 0;
        //thePlayer.PUSlowPartikel.SetActive(true);
        theSlowMo.setSlowMo(true);
        LayerSlowMo.SetActive(true);
        LayerSlowMo.transform.position = new Vector3(Mathf.Lerp(LayerSlowMo.transform.position.x ,theCamera.transform.position.x,15f * Time.deltaTime) , LayerSlowMo.transform.position.y, LayerSlowMo.transform.position.z);
        theUIManager._sloMoPowerUp.enabled = true;
        theUIManager._sloMoTime.enabled = true;
        
        //Countdown timer saat slow motion effect aktif
        if (theSlowMo.slowMotion)
        {
			theCameraController.iszoom = false;
			slowMoTime -= Time.deltaTime * 5;
            theUIManager._sloMoTime.text = Mathf.Round(slowMoTime) + "  s";
            if (thePlayer.tanahKah) {
                if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                {
					theSlowMo.setSlowMo(false);
                    slowMoTime = 0f;
                    //thePlayer.PUSlowPartikel.SetActive(false);
                }
            }
            
        }

        //Mematikan effect saat waktu habis
        if (slowMoTime <= 1)
        {
			theCameraController.iszoom = true;
            theUIManager._sloMoPowerUp.enabled = false;
            theUIManager._sloMoTime.enabled = false;
            theSlowMo.setSlowMo(false);
			setSlowMotionStatus (false);
            LayerSlowMo.SetActive(false);
            LayerSlowMo.transform.position = new Vector3(LayerSlowMo.transform.position.x + 41f, LayerSlowMo.transform.position.y, LayerSlowMo.transform.position.z);
            //thePlayer.PUSlowPartikel.SetActive(false);
        }

    }

    public void MagnetPowerUp()
    {
        thePlayer.hilangkanPUPartikel = 0;
        thePlayer.PUMagnetPartikel.SetActive(true);
        theMagnet.setMagnet(true);
        theMagnet.setMagneticForce(30.0f);//Pengaturan Magnet
        theMagnet.setMagneticRange(5.0f);

        theUIManager._magnetPowerUp.enabled = true;
		theUIManager._magnetTime.enabled = true;

        //memulai timer effect
        magnetTime -=  Time.deltaTime;
        theUIManager._magnetTime.text = Mathf.Round(magnetTime) + "  s";
        if (magnetTime <= 1)
        {
            theUIManager._magnetPowerUp.enabled = false;
            theUIManager._magnetTime.enabled = false;
            magnetTime = 0f;
			theMagnet.setMagnet(false);
            thePlayer.PUMagnetPartikel.SetActive(false);
        }

    }

    public void SteelPowerUp()
    {
        thePlayer.hilangkanPUPartikel = 0;
        thePlayer.PUSteelPartikel.SetActive(true);
        theUIManager._steelPowerUp.enabled = true;
        theUIManager._steelTime.enabled = true;
        steelTime -= Time.deltaTime;
        theUIManager._steelTime.text = Mathf.Round(steelTime) + "  s";
        
        if (steelTime <= 1)
        {
            steelTime = 0;
            theUIManager._steelPowerUp.enabled = false;
            theUIManager._steelTime.enabled = false;
			setSteelStatus (false);
            thePlayer.steelkah = false;
            thePlayer.PUSteelPartikel.SetActive(false);
        }

    }

    public void BouncingPowerUp()
    {
        thePlayer.hilangkanPUPartikel = 0;
        thePlayer.PUBouncePartikel.SetActive(true);
        theUIManager._bouncePowerUp.enabled = true;
        theUIManager._bounceTime.enabled = true;
		thePlayerRigid.sharedMaterial = thePlayer.bounceMaterial[1]; //Mengubah material menjadi memiliki bounceness
		theCameraController.iszoom = false;

        bouncingTime -= Time.deltaTime;
        theUIManager._bounceTime.text = Mathf.Round(bouncingTime) + "  s";
        if (bouncingTime <= 1) 
        {
            bouncingTime = 0f;
			thePlayerRigid.sharedMaterial = thePlayer.bounceMaterial[0]; //Mengubah material menjadi seperti semula
			theCameraController.iszoom = true;
           
            theUIManager._bouncePowerUp.enabled = false;
            theUIManager._bounceTime.enabled = false;
			setBounceStatus (false);
           thePlayer.PUBouncePartikel.SetActive(false);
            bouncingTime = thePlayer.bouncingTime;
        }
    }

    public void AeroPowerUp() {
        thePlayer.hilangkanPUPartikel = 0;
        thePlayer.PUAeroPartikel.SetActive(true);
        thePlayer.PUAero2Partikel.SetActive(true);
        theCameraController.iszoom = false;
        theUIManager._aeroPowerUp.enabled = true;
        theUIManager._aeroTime.enabled = true;
        thePlayer.lompatGaya =7 ;
        aeroTime -= Time.deltaTime;
        theUIManager._aeroTime.text = Mathf.Round(aeroTime) + "  s";
        if (aeroTime <= 1)
        {
            aeroTime = 0;
			theCameraController.iszoom = true;
            thePlayer.lompatGaya = 6;
            theUIManager._aeroPowerUp.enabled = false;
            theUIManager._aeroTime.enabled = false;
			setAeroStatus (false);
            aeroTime = thePlayer.aeroTime;
            thePlayer.PUAeroPartikel.SetActive(false);
            thePlayer.PUAero2Partikel.SetActive(false);
        }
    }
    #endregion 
}
