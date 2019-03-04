using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwanDanBalonGenerator : MonoBehaviour {
    public ObjectPooler[] theAwan;
    public ObjectPooler[] theBalon;
    private GameObject AwanAndBalonGeneratorPoint;
    public Transform BackGround;


    public Transform minJarakHeight;
    public Transform maxJarakHeight;

    public float jarakObject;

    public int randomAppear;
    public int randomSelectorBalon;
    public int randomSelectorAwan;
	// Use this for initialization
	void Start () {
        AwanAndBalonGeneratorPoint = GameObject.Find("AwanDanBalonGeneratorPoint");
        transform.position = AwanAndBalonGeneratorPoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        /*
         1.Jika Random Appear True Maka Awan atau balon atau keduanya Appear;
         */
         if(transform.position.x < AwanAndBalonGeneratorPoint.transform.position.x)
        {
            if (Random.Range(0, 100) < randomAppear)
            {
                transform.position = new Vector3(transform.position.x + jarakObject, transform.position.x, transform.position.z);
                if (Random.Range(0, 100) < randomSelectorAwan)
                { //Membuat Awan
                    GameObject Awan = theAwan[Random.Range(0,theAwan.Length)].GetPooledObject();
                    Awan.transform.position = new Vector3(transform.position.x, Random.Range(minJarakHeight.position.y,maxJarakHeight.position.y),transform.position.z);
                    Awan.transform.rotation = transform.rotation;
                    Awan.transform.SetParent(BackGround);
                    Awan.SetActive(true);

                    //memindahkan Generator
                    transform.position = new Vector3(transform.position.x + jarakObject, transform.position.x, transform.position.z);
                }
                if (Random.Range(0, 100) < randomSelectorBalon)
                { // Membuat Balon
                    GameObject Balon = theBalon[Random.Range(0, theBalon.Length)].GetPooledObject();
                    Balon.transform.position = new Vector3(transform.position.x, Random.Range(minJarakHeight.position.y, maxJarakHeight.position.y), transform.position.z);
                    Balon.transform.rotation = transform.rotation;
                    Balon.transform.SetParent(BackGround);
                    Balon.SetActive(true);

                    //memindahkan Generator
                    transform.position = new Vector3(transform.position.x + jarakObject, transform.position.x, transform.position.z);
                }
            }
        }
            
	    }
}
