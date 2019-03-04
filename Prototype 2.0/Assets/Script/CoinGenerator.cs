using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

    public ObjectPooler coinPool;
    public GameObject coin0,coin1,coin2,coin3,coin4,coin5,coin6,coin7,coin8,coin9,coin10,coin11;

    public float distanceXBetweenCoins;
    public float distanceYBetweenCoins;

    public void SpawnCoin(int pattern, Vector3 startPosition) {
        switch (pattern) {

            case 0: //  Lurus 4
                coin0 = coinPool.GetPooledObject();
                coin0.transform.position = startPosition;
                coin0.SetActive(true);

                coin1 = coinPool.GetPooledObject();
                coin1.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 1, startPosition.y, startPosition.z);
                coin1.SetActive(true);

                coin2 = coinPool.GetPooledObject();
                coin2.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 2, startPosition.y, startPosition.z);
                coin2.SetActive(true);

                coin3 = coinPool.GetPooledObject();
                coin3.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 3, startPosition.y, startPosition.z);
                coin3.SetActive(true);

                break;


            case 1: // Melengkung 6
                coin0 = coinPool.GetPooledObject();
                coin0.transform.position = startPosition;
                coin0.SetActive(true);

                coin1 = coinPool.GetPooledObject();
                coin1.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 1, startPosition.y + distanceYBetweenCoins * 1, startPosition.z);
                coin1.SetActive(true);

                coin2 = coinPool.GetPooledObject();
                coin2.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 2, startPosition.y + distanceYBetweenCoins * 2, startPosition.z);
                coin2.SetActive(true);

                coin3 = coinPool.GetPooledObject();
                coin3.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 3, startPosition.y + distanceYBetweenCoins * 2, startPosition.z);
                coin3.SetActive(true);

                coin4 = coinPool.GetPooledObject();
                coin4.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 4, startPosition.y + distanceYBetweenCoins * 1, startPosition.z);
                coin4.SetActive(true);

                coin5 = coinPool.GetPooledObject();
                coin5.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 5, startPosition.y, startPosition.z);
                coin5.SetActive(true);
                break;

            case 2: // Melingkar
                coin0 = coinPool.GetPooledObject();
                coin0.transform.position = startPosition;
                coin0.SetActive(true);

                coin1 = coinPool.GetPooledObject();
                coin1.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 1, startPosition.y + distanceYBetweenCoins * 1, startPosition.z);
                coin1.SetActive(true);

                coin2 = coinPool.GetPooledObject();
                coin2.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 2, startPosition.y + distanceYBetweenCoins * 2, startPosition.z);
                coin2.SetActive(true);

                coin3 = coinPool.GetPooledObject();
                coin3.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 3, startPosition.y + distanceYBetweenCoins * 2, startPosition.z);
                coin3.SetActive(true);

                coin4 = coinPool.GetPooledObject();
                coin4.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 4, startPosition.y + distanceYBetweenCoins * 1, startPosition.z);
                coin4.SetActive(true);

                coin5 = coinPool.GetPooledObject();
                coin5.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 5, startPosition.y, startPosition.z);
                coin5.SetActive(true);

                coin6 = coinPool.GetPooledObject();
                coin6.transform.position = new Vector3(startPosition.x, startPosition.y - 0.5f, startPosition.z);
                coin6.SetActive(true);

                coin7 = coinPool.GetPooledObject();
                coin7.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 1, startPosition.y - distanceYBetweenCoins * 2, startPosition.z);
                coin7.SetActive(true);

                coin8 = coinPool.GetPooledObject();
                coin8.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 2, startPosition.y - distanceYBetweenCoins * 3, startPosition.z);
                coin8.SetActive(true);

                coin9 = coinPool.GetPooledObject();
                coin9.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 3, startPosition.y - distanceYBetweenCoins * 3, startPosition.z);
                coin9.SetActive(true);

                coin10= coinPool.GetPooledObject();
                coin10.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 4, startPosition.y - distanceYBetweenCoins * 2, startPosition.z);
                coin10.SetActive(true);

                coin11 = coinPool.GetPooledObject();
                coin11.transform.position = new Vector3(startPosition.x + distanceXBetweenCoins * 5, startPosition.y - 0.5f, startPosition.z);
                coin11.SetActive(true);
                break;

        }


        
            
    
    } 
    }



/*
 * for (int i = 0; i < 4; i++) {
                    arrayOfCoins[i] = coinPooler.GetPooledObject();
                }
                for (int i = 0; i < arrayOfCoins.Length; i++) {
                    arrayOfCoins[i].transform.position = new Vector2(positionStart.x + (0.4f * i), positionStart.y);
                   // positionStart.position= arrayOfCoins[i].transform.position;
                }*/