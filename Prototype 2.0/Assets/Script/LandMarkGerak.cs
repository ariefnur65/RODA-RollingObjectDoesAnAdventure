using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkGerak : MonoBehaviour {
    private float Speed;
    public KarakterSkrip thePlayer;
    public float speedPercent;
    private GameObject Camera;
    public bool tengahCamera;
    public float offsetBatasDepan;
    public float offsetBatasBelakang;
    public float kecepatanGerak;
    private string namaLandmark;
    public float percentageCepat;
    public float percentageLambat;
    public float smoothness;
    private float maxKecepatanGerak;

    private SoundManager theSoundMan;
    private LandMarkGenerator theLandMarkGenerator;

    private static Vector3 tempatSemulaAwanIN;
    private static Vector3 tempatSemulaAwanOUT;

    private GameObject awanIn;
    private GameObject awanOut;

    private float transisiGerak;

    /*Child[]
    0. transisiAwanIN
    1. transisiAwanOut
    2. openingPoint
    3. IN Point
    4. Out Point


        
		1. landmark jalan sampai point buka di tengah layar
		2. ketika ditengah layar maka awan bergerak
		3. salama awan bergerak maka landamark berhenti atau melambat (percentage di turunin) sampai awan bergerak sampai posisi ganti posisi
		4. ketika sampai posisi maka awan bergerak degnan normal lagi
		5. Setelah Disable maka posisinya kembali ke posisi start



         */
    // Use this for initialization
    void Start()
    {
        tempatSemulaAwanIN = transform.GetChild(0).gameObject.transform.localPosition;

        tempatSemulaAwanOUT = transform.GetChild(1).gameObject.transform.localPosition;

        awanIn = transform.GetChild(0).gameObject;
        awanOut = transform.GetChild(1).gameObject;
        namaLandmark = gameObject.name;
        thePlayer = FindObjectOfType<KarakterSkrip>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        theSoundMan = FindObjectOfType<SoundManager>();
        theLandMarkGenerator = FindObjectOfType<LandMarkGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayer.isActiveAndEnabled) {
            if (Time.timeScale != 0)
            {

                if (Camera.transform.position.x < transform.GetChild(2).position.x  || Camera.transform.position.x > transform.GetChild(1).position.x )
                {
                    speedPercent = percentageCepat;
                    maxKecepatanGerak = 0.3f;
                    theSoundMan.selesai = false;

                }
                
                else {
                    if (!theSoundMan.selesai && thePlayer.isPlaying) //Dimulai ketika blm selesai dan ketika dalam proses tidak boleh diinterupsi
                    {

                        theSoundMan.changeBGM(theLandMarkGenerator.urutanLandmarkSedangMuncul);

                    }
                    if (awanIn.transform.position.x > transform.GetChild(3).position.x)
                    {
                        //float posisiXIN = awanIn.transform.position.x;
                        //posisiXIN = Mathf.MoveTowards(posisiXIN, transform.transform.GetChild(3).transform.position.x, transisiGerak);
                        //awanIn.transform.position = new Vector3(posisiXIN,awanIn.transform.position.y, awanIn.transform.position.z);

                        //float posisiXOUT = awanOut.transform.position.x;
                        //posisiXOUT = Mathf.MoveTowards(posisiXOUT, transform.transform.GetChild(4).transform.position.x, transisiGerak);
                        //awanOut.transform.position = new Vector3(posisiXOUT, awanOut.transform.position.y, awanOut.transform.position.z);
                        awanIn.transform.Translate(-0.5f, 0, 0);


                    }
                    if (awanOut.transform.position.x < transform.GetChild(4).position.x)
                    {

                        //float posisiXIN = awanIn.transform.position.x;
                        //posisiXIN = Mathf.MoveTowards(posisiXIN, transform.transform.GetChild(3).transform.position.x, transisiGerak);
                        //awanIn.transform.position = new Vector3(posisiXIN,awanIn.transform.position.y, awanIn.transform.position.z);

                        //float posisiXOUT = awanOut.transform.position.x;
                        //posisiXOUT = Mathf.MoveTowards(posisiXOUT, transform.transform.GetChild(4).transform.position.x, transisiGerak);
                        //awanOut.transform.position = new Vector3(posisiXOUT, awanOut.transform.position.y, awanOut.transform.position.z);
                        awanOut.transform.Translate(-0.5f, 0, 0);
                    }


                    speedPercent = Mathf.MoveTowards(speedPercent, percentageLambat, Time.deltaTime * smoothness);
                    maxKecepatanGerak = 0.065f;

                }

                /*
                //Jika Awan pertama sudah ditengah layar maka akan dipercepat
                if (transform.GetChild(0).position.x >= Camera.transform.position.x + offsetBatasDepan)
                {
                    speedPercent = percentageCepat;
                    maxKecepatanGerak = 0.3f;
                    theSoundMan.selesai = false;
                }

                else if (transform.GetChild(1).position.x <= Camera.transform.position.x + offsetBatasBelakang)
                {
                    speedPercent = percentageCepat;
                    maxKecepatanGerak = 0.3f;
                }
                else
                {

                    speedPercent = Mathf.MoveTowards(speedPercent, percentageLambat, Time.deltaTime * smoothness);
                    maxKecepatanGerak = 0.065f;
                    if (!theSoundMan.selesai) //Dimulai ketika blm selesai dan ketika dalam proses tidak boleh diinterupsi
                    {
                        theSoundMan.changeBGM(theLandMarkGenerator.urutanLandmark);

                    }
                }*/
                Speed = thePlayer.kecepatanGelinding;
                
                kecepatanGerak = Speed * speedPercent * Time.deltaTime * smoothness;


                if (kecepatanGerak >= maxKecepatanGerak)
                {
                    kecepatanGerak = maxKecepatanGerak;
                }
                transform.Translate(-kecepatanGerak, 0, 0);


             
            }
        }

    }
    public void awanKembali()
    {
        //Out
        //-12.3, 1.9
        //In
        //14.6 , -1.364555

            awanIn.transform.localPosition = tempatSemulaAwanIN;
            awanOut.transform.localPosition = tempatSemulaAwanOUT;
     
    }
  
}
