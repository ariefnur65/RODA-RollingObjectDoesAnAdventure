using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandMarkGenerator : MonoBehaviour {
    public GameObject[] theLandMark;
    public ScoreManager theScoreManager;
    public float[] jarakLandmark;
    public float storeJarakLandmark;
    public int urutanLandmark;
    public int urutanLandmarkSedangMuncul;
    public Transform backGround;
    private GameManager GM;
    private KarakterSkrip thePlayer;
    private UIManager UIM;
    private InteractableOff Interactable;
    public static string namaLandMark;
    private PlatformGeneration thePlatformGenerator;
    public float jarakNyatanya;
    public bool resetProgress;
    // Use this for initialization
    void Start () {
        urutanLandmark = 0;
        jarakNyatanya = jarakLandmark[0];
        thePlatformGenerator = FindObjectOfType<PlatformGeneration>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlayer = FindObjectOfType<KarakterSkrip>();
        UIM = FindObjectOfType<UIManager>();

        //jarakLandmark = storeJarakLandmark;
        GM = FindObjectOfType<GameManager>();
        //Membuat PlayerPref 
        int x = 0;
        thePlatformGenerator.setPlatformGenerator(urutanLandmark);
        for (int i = 0; i < theLandMark.Length; i++)
        { 

             namaLandMark = theLandMark[x].name; //Mengambil nama gameObject
            Debug.Log("nama:" + namaLandMark);
            // PlayerPrefs.SetInt(namaLandMark, 1); // Default nilai

            //PlayerPrefs.SetInt(namaLandMark, 1); // Default nilai

            if (!PlayerPrefs.HasKey(namaLandMark))
            {
                PlayerPrefs.SetInt(namaLandMark, 0); // Default nilai
            }

            UIM.HasLandmark(namaLandMark, x);
            x++;
                   
       }
    }
	
	// Update is called once per frame
	void Update () {
        if (theScoreManager.getCurrentDistance() > jarakNyatanya-100)
        {//Jika Melebihi Jarak Landmark maka akan di tampilkan 
        

            //Instansiasi Landmark
            GameObject landMark =  theLandMark[urutanLandmark];
            landMark.transform.position = transform.position;
            landMark.transform.rotation = transform.rotation;
            landMark.transform.SetParent(backGround);
            landMark.SetActive(true);
            
            urutanLandmarkSedangMuncul = urutanLandmark;
            urutanLandmark++;//Landmark Selanjutnya
                             //Generator ganti presentase ketika membuat landmark baru
            if (urutanLandmark >= theLandMark.Length) {
                urutanLandmark = 0;
                jarakNyatanya += 1000;
            }
            //jarakLandmark += storeJarakLandmark + (200*urutanLandmark);
            jarakNyatanya += jarakLandmark[urutanLandmark]; 

        }
        thePlatformGenerator.setPlatformGenerator(urutanLandmark);

        



        if (GM.landMarkStoreOke) //Jika restart maka jarak landmark di kembalikan seperti semula
        {
            // jarakLandmark = storeJarakLandmark;
            jarakNyatanya = jarakLandmark[0];
        }

        if (UIM.restart) // Jika Restart maka urutan landmark kembali
        {
            urutanLandmark = 0;
        }

        if (resetProgress)
        {
            int index = 0;
            for (int i = 0; i < theLandMark.Length; i++)
            {
                if (PlayerPrefs.HasKey(theLandMark[index].name))
                {
                    PlayerPrefs.SetInt(theLandMark[index].name, 0);
                }

                UIM.HasLandmark(theLandMark[index].name, index);
                index++;
            }
            resetProgress = false;
        }
    }
    
    public bool landmarkChecker(int i)
    {
        if (PlayerPrefs.GetInt(theLandMark[i].name) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public float getJarakNyataLandMark()
    {
        return jarakNyatanya;
    }

    public float getJarakLandmark(int i)
    {
        return jarakLandmark[i];
    }
}
