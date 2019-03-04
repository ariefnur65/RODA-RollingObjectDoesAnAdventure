using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

	public AudioSource coinSound;
    public AudioSource deathSFXs;
	public AudioSource BGM;
    public AudioSource jumpSound;
    public AudioSource pakuhancur;
    public AudioSource powerUpSound;
	private AudioSource[] soundFXs;
	public AudioSource[] BGMs; //Ole ganti

    public Button[] Buttons;
    //public AudioSource mainBGM;

    public float smoothness;
    public bool selesai;
    public bool deathSwitch;
    public bool ganti;
    public int step;
    private int stepDeath;


    //Memlbuat audio Source baru untuk menyimpan BGM yang di putar sebelum death dan jika revive maka akan di putar kembali

    public AudioSource beforeDeathBGM;
    private AudioSource buttonClick;
    public AudioClip btnClickClip;
    
    /*
     0-7 lagu BGM Setiap Landmark
     8 Lagu BGM utama (Main hanya ketika mulai dari main menu atau restart)
     9 Lagu BGM Main Menu

        SFX:
        Coin
        deathSfxs
         */
    //public bool soundFXmuteStat;
    //public bool bgmMuteStat;
    // Use this for initialization
    void Start () {
		soundFXs = new AudioSource[] {coinSound,deathSFXs,jumpSound,powerUpSound,pakuhancur};
        buttonClick = GetComponent<AudioSource>();
        buttonClick.clip = btnClickClip;
        step = 0;
        mainMenuPlay();
        deathSwitch = true;
        beforeDeathBGM = BGMs[8];
        //soundFXmuteStat = false;
        //bgmMuteStat = false;

        foreach (Button btn in Buttons)
        {
            btn.onClick.AddListener(() => {
                buttonClick.PlayOneShot(btnClickClip, 1f);
            });
        }
    }
	
	// Update is called once per frame
	void Update () {
     
	}

    public void changeBGM(int urutanLandMark) //Tambah Saya
    {
        //if (deathSwitch) { //Jika karakter mati ketika masih blm selesai maka
            if (step == 0 && !selesai)
            {


                BGM.volume = Mathf.MoveTowards(BGM.volume, 0.0f, smoothness);
                if (BGM.volume == 0.0f) step = 1;
            }
            else if (step == 1 && !selesai)
            {
            beforeDeathBGM = BGMs[urutanLandMark];
                BGM = BGMs[urutanLandMark];
                            BGM.Play();

                step = 2;
            }
            else if (step == 2 && !selesai)
            {

                BGM.loop = true;
                BGM.volume = Mathf.MoveTowards(BGM.volume, 1.0f, smoothness);

                if (BGM.volume >= 1.0f)
                {

                    step = 0;
                    selesai = true;

                }
            }
        //}
        //else
        // {
        /*   BGM.Stop();
           BGM = BGMs[urutanLandMark];
           selesai = true;
           step = 0;*/


        // }


    }
    /*
    public void changeBGM(int urutanLandMark, float smothnessTransition) //Tambah Saya
    {

        if (step == 0 && !selesai)
        {
            BGM.volume = Mathf.MoveTowards(BGM.volume, 0.0f, smothnessTransition);
            if (BGM.volume == 0.0f) step = 1;
        }
        else if (step == 1 && !selesai)
        {
            BGM = BGMs[urutanLandMark];
            BGM.Play();

            step = 2;
        }
        else if (step == 2 && !selesai)
        {
            BGM.loop = true;
            BGM.volume = Mathf.MoveTowards(BGM.volume, 1.0f, smothnessTransition);

            if (BGM.volume >= 1.0f)
            {
                Debug.Log(BGM.volume.ToString());
                step = 0;
                selesai = true;

            }
        }


    }*/

    public void MuteSoundFX(){
		foreach (AudioSource _soundFX in soundFXs){
			_soundFX.mute = true;
		}
		//soundFXmuteStat = true;
	}

	public void UnmuteSoundFX(){
		foreach (AudioSource _soundFX in soundFXs){
			_soundFX.mute = false;
		}
	}

	public void MuteBGM(){
		foreach (AudioSource _bgm in BGMs){
			_bgm.mute = true;
		}
        BGM.mute = true;

        //bgmMuteStat = true;
    }

    public void UnmuteBGM(){
		foreach (AudioSource _bgm in BGMs){
			_bgm.mute = false;
            
		}

        if (!BGM.isPlaying && BGM.name != "dead")
        {
            BGM.volume = 1.0f;
            BGM.Play();
        }
    }
    public void mainMenuPlay()
    {
        BGM.Stop();
        BGM = BGMs[9];
        BGM.volume = 1.0f;
        BGM.Play();
    }

    public void BGMMain()
    {
        BGM.Stop();
        BGM = BGMs[8];
        BGM.volume = 1.0f;

        BGM.Play();

    }


    //===========Death Sound====
    /*
     * Ketika Mati maka BGM utama akan Fade Out
     * BGM Mati akan Fade in dan play satu kali
     * 
     
     */

    public void BGMDeath()
    {   /*BGM.volume = 1.0f;
        BGM.Stop();
        BGMs[10].loop = false;
        BGMs[10].Play();*/
       if (stepDeath == 0 && !deathSwitch)
        {
            BGM.volume = Mathf.MoveTowards(BGM.volume, 0.0f, 0.5f);
            if (BGM.volume == 0.0f) stepDeath = 1; //BGM.Stop(); BGM.loop = false;
            if (selesai) beforeDeathBGM = BGM;
            

        }
        else if (stepDeath == 1 && !deathSwitch)
        {
            BGM = deathSFXs;
            BGM.Play();
            stepDeath = 2;
        }
        else if (stepDeath == 2 && !deathSwitch)
        {
            deathSFXs.loop = false;
            BGM.volume = Mathf.MoveTowards(BGM.volume, 1.0f, 0.5f);

            if (BGM.volume >= 1.0f)
            {

                stepDeath = 0;
                deathSwitch = true;

            }
        }

    }

}
