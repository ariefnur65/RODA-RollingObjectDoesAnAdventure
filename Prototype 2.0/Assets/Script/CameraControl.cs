using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour {

    public GameObject player;
	public Camera cam;
	public float orthographSizeDef;
    public float zoomOutSize;
	private float myOrthographSize;
    public float smoothness;
    public float fastness;
    public bool iszoom;
	public bool isDeath;
	public Vector3 startOffset;
    public Transform StartCam;
	private Vector3 finaloffset; // Selisih Posisi dari Kamera dan Bola seharusnya (7.9,-2.21,-1.34) 
    public float mainMenuZoom;
    private GameObject theMainMenu;
    public GameObject theLandmarkMenu;
    public GameObject theUpgradeMenu;
    public GameObject theCreditMenu;
        



	// Use this for initialization
	void Start () {
		startOffset = transform.position - player.transform.position;
		//Camera.main.orthographicSize = orthographSizeDef;
		myOrthographSize = orthographSizeDef;
        theMainMenu = GameObject.Find("MainMenu");
     
		isDeath = false;
	}

    //Kamera semakin besar ketika speed semakin besar
	
	// Update is called once per frame after update method is processed
	void LateUpdate () {
		if(player.GetComponent<KarakterSkrip>().isCamFollowing)
        {
			//melakukan lerp utnuk x dari awal (startPos) --> (endPos) endpos = Offset 
			startOffset.x = Mathf.MoveTowards(startOffset.x, 7.9f, Time.deltaTime * smoothness);
			//melakukan lerp utnuk y dari awal (startPos) --> (endPos) endpos = Offset 
			startOffset.y = Mathf.MoveTowards(startOffset.y, -0.5f , Time.deltaTime * smoothness);
            //Mempertahankan posisi biar kayak child tapi gk ikut muter
            transform.position = startOffset + player.transform.position;

        
        }

        else if (!player.GetComponent<KarakterSkrip>().isCamFollowing)
        {
            //move the camera to the center of the field Mathf.MoveTowards(transform.position.x, player.GetComponent<KarakterSkrip>().deathPosition.x, fastness)
            gameObject.transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, player.GetComponent<KarakterSkrip>().deathPosition.y, fastness), transform.position.z);
        }

        /*
        if (isDeath)
        {
            transform.position = transform.position;
            iszoom = false;
        }*/


    }


    void Update()
    {


        #region Main menu zoom
        if (theMainMenu.activeSelf|| theLandmarkMenu.activeSelf || theCreditMenu.activeSelf || theUpgradeMenu.activeSelf )
        {
            //Jika aktif maka zoomnya akan ganti menjadi zoom main menu
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, mainMenuZoom, 0.5f); ;
            
        }
        #endregion

        #region zoom in and out
        else if (iszoom)
        {
            // sise orthographic saat  zoomIn
            //nilai orthographic Size = nilai awal + nilai dibelakang koma dari kecepatan / 5 
            //nilai bisa diatur ulang, bisa dengan mengubah nilai pembagi dan pengali yg digunakan
            //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, orthographSizeDef, Time.deltaTime * smoothness);
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, orthographSizeDef, Time.deltaTime * smoothness);
           
        }
        else {
               // / sise orthographic saat  zoomOut
            //nilai orthographic Size = nilai awal + nilai dibelakang koma dari kecepatan / 5 + perubahan waktu * 2 
            //nilai bisa diatur ulang, bisa dengan mengubah nilai pembagi dan pengali yg digunakan
            //cam.orthographicSize = myOrthographSize + ((mySpeed - Mathf.Floor (mySpeed)) / 5) + (Time.deltaTime * 2);
            //Mengubah size kamera dari size awal kemudian ke size yang lebih besar
            //Lerp akan mengubah size orthographic dengan besaran delta time dikali kelembutan setiap framenya
            //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, Time.deltaTime * smoothness);
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, zoomOutSize, Time.deltaTime * smoothness);

        }
        #endregion


    }

    public void ResetCamera()
    {
        transform.position = StartCam.position;
        player.GetComponent<KarakterSkrip>().deathPosition = StartCam.position; //Tambahan ole
        startOffset = transform.position - player.transform.position;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, Time.deltaTime * smoothness);

    }

    public void samplingOffset()
    {
        startOffset = transform.position - player.transform.position;

    }

}
