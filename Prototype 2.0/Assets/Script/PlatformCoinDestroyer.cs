using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCoinDestroyer : MonoBehaviour {
    private GameObject platformDestructionPoint;
    private Vector3[] posisiAwalChild = new Vector3[100];
    // Use this for initialization  
    void Start()
    {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
        //Untuk pertama object akan minyimpan posisi child yang ada yang ada 
        for (int x = 0; x < gameObject.transform.childCount; x++)
        {
            //Debug.Log(gameObject.transform.GetChild(x).transform.position);
            posisiAwalChild[x] = gameObject.transform.GetChild(x).transform.position;
            //Debug.Log(posisiAwalChild[x]);

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Membuat game manger dimana ketika mati akan memanggil semua game object yang mmeiliki script ini kemudian mengembalikan posisinya, tapi posisinya di tentuin pake apa?
        //Platform di hancurkan ketika di belakang titik penghancur
        if (transform.position.x < platformDestructionPoint.transform.position.x)
        {

            gameObject.SetActive(false);

            Reset();
        }
        
    }

    public Vector3 getPosisiAwalChild(int x)
    {
        return posisiAwalChild[x];
    }
    public void samplingPosisiChild()
    {
        //Untuk pertama object akan minyimpan posisi child yang ada yang ada 
        for (int x = 0; x < gameObject.transform.childCount; x++)
        {
            //Debug.Log(gameObject.transform.GetChild(x).transform.position);
            posisiAwalChild[x] = gameObject.transform.GetChild(x).transform.position;
            //Debug.Log(posisiAwalChild[x]);

        }
    }

    public void Reset()
    {
        if (gameObject.activeSelf == false)
        {
            for (int x = 0; x < gameObject.transform.childCount; x++)
            {
                gameObject.transform.GetChild(x).transform.position = posisiAwalChild[x];
            }
        }
    }


    /*
    private void OnDisable()
    {
        //Ketika game object di hancurkan maka child akan diposisikan ke posisi smeula
        for (int x = 0; x < gameObject.transform.childCount; x++)
        {
            gameObject.transform.GetChild(x).transform.position = posisiAwalChild[x];
        }
    }*/
}
