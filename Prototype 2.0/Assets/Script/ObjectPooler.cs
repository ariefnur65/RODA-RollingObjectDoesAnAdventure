using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public GameObject pooledObject; // Object yang akan disimpan Banyak Object yanga akan di pooled dan di tentukan oleh random ketika start
    public int pooledAmount; //Jumlah Object yang akan disimpan
    //public int randomSelector;
    List<GameObject> listPooledObject; //Tempat untuk object yang disimpan
    //Menginstansiasi semua object terlebih dahulu lalu menggunakan list pooled untuk di gunakan
    //menyimpan di game object yang telah di instantiate dan digunakan 
    void Start () {
        //Referensi List
        listPooledObject = new List<GameObject>();
        //Membuat seset Platform yang kemudian disimpan di list sejumlah yang diset dalam pooledamount
        for (int i = 0; i < pooledAmount; i++)
        {
            //randomSelector = Random.Range(0, 4);//Randomisasi untuk memilih objectPooled
            GameObject obj = (GameObject)Instantiate(pooledObject); //Cast (GameObject) memastikan object yang diinstansiasi bertime gameobject
            obj.SetActive(false); // Menonaktifkan object
            listPooledObject.Add(obj);  // Memasukan ke list 

        }
    }

    //Digunakan untuk mendapatkan object yang sudah tidak aktif pada list sehingga dapat digunakan pada 
    //Platform Generator dan di aktifkan dan di pindahkan ke posisi yang sesuai disana
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < listPooledObject.Count; i++)
        {
            //Jika object tidak aktif maka object akan dijadikan keluaran
            if (!listPooledObject[i].activeInHierarchy)
            {
                return listPooledObject[i];

            }
        }
            //Jika object yang di aktifkan masih kurang amaka akan dibuat object baru
            //randomSelector = Random.Range(0, 4);//Randomisasi untuk memilih objectPooled
            GameObject obj = (GameObject)Instantiate(pooledObject); //Cast (GameObject) memastikan object yang diinstansiasi bertime gameobject
            obj.SetActive(false); // Menonaktifkan object
            listPooledObject.Add(obj);  // Memasukan ke list 
            return obj;

    }
}
