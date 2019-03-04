using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundGenerator : MonoBehaviour {
    public ObjectPooler theGunung;
    public Transform gunungGeneratorPoint;
    public float distanceGunung;
    public GameObject gunungStart;
    public GameObject BackGround;
    
	// Use this for initialization
	void Start () {
        transform.position = gunungStart.transform.position; //Melakukan default start
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (transform.position.x < gunungGeneratorPoint.position.x)
        {
            //point generator pindah ke posisi baru
            transform.Translate(distanceGunung-0.15f, 0, 0);
            //Instantiate(theGunung, transform.position,transform.rotation,BackGround.transform);
                //gunung1
                GameObject GunungObject = theGunung.GetPooledObject();
                GunungObject.transform.position = transform.position;
                GunungObject.transform.rotation = transform.rotation;
                GunungObject.transform.SetParent(BackGround.transform);
           /* if (GunungObject.transform.childCount > 0)
            {
                GunungObject.transform.GetChild(0).gameObject.transform.position = new Vector3(transform.position.x+0.151f, GunungObject.transform.GetChild(0).gameObject.transform.position.y, GunungObject.transform.GetChild(0).gameObject.transform.position.z);
            }*/
                GunungObject.SetActive(true);
            


        }
    }
}
