using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunungDestruction : MonoBehaviour {
    private GameObject platformDestructionPoint;

    // Use this for initialization
    void Start()
    {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
        //Untuk pertama object akan minyimpan posisi child yang ada yang ada 
        /*
        for(int x =0; x < gameObject.transform.childCount; x++)
        {
           posisiAwalChild[x]= gameObject.transform.GetChild(x).transform.position;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //Platform di hancurkan ketika di belakang titik penghancur
        if (transform.position.x < platformDestructionPoint.transform.position.x)
        {
            gameObject.SetActive(false);
            //Ketika game object di hancurkan maka child akan diposisikan ke posisi smeula
            /*for (int x = 0; x < gameObject.transform.childCount; x++)
            {
               gameObject.transform.GetChild(x).transform.position =  posisiAwalChild[x];
            }*/

        }
    }
}
