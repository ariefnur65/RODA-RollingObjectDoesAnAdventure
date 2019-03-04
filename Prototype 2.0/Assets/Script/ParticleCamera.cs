using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleCamera : MonoBehaviour
{

    public GameObject player;
    public Vector3 Offset;

    
    private Vector3 finaloffset; // Selisih Posisi dari Kamera dan Bola seharusnya (7.9,-2.21,-1.34) 
                                 // Use this for initialization
    void Start()
    {
        Offset = transform.position - player.transform.position;
    }

    //Kamera semakin besar ketika speed semakin besar

    // Update is called once per frame after update method is processed
    void LateUpdate()
    {
        transform.position = Offset + player.transform.position;
    }
}
