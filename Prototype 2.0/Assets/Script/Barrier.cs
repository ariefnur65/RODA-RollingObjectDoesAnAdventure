using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    public bool tebingKah;
    public Collider2D barrier;
    public LayerMask tanah;
    //private KarakterSkrip scriptCharacter;
	private GameManager _GameManager;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
        barrier = GetComponent<Collider2D>();
        //scriptCharacter = FindObjectOfType<KarakterSkrip>();
		_GameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update () {
        transform.position = offset + player.transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("tanah"))

        {
            //scriptCharacter.deathChar();
			_GameManager.DestroyGeneratedObjects();
        }



    }
}
