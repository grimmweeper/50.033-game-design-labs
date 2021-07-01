using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpriteRenderer coinSprite;
    private AudioSource coinAudio;
    private GameObject spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        coinSprite = GetComponent<SpriteRenderer>();
        coinAudio = GetComponent<AudioSource>();
        spawnManager = GameObject.Find("EmptySpawnManager");

        InvokeRepeating("FlipXpos", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FlipXpos() {
        if (coinSprite.flipX) {
            coinSprite.flipX = false;
        } else {
            coinSprite.flipX = true;
        }
    }

    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Mario")){
            PlayCoinPickupSound();
            spawnManager.GetComponent<SpawnManager>().spawnNewEnemy();
            CentralManager.centralManagerInstance.increaseScore();
            StartCoroutine(consumeSequence());

        }
    }

    IEnumerator consumeSequence(){
		Debug.Log("consume starts");

		float scaleUp = 0.8f;
        float scaleDown = -0.5f;

        this.transform.localScale = new Vector3(this.transform.localScale.x + scaleUp, this.transform.localScale.y + scaleUp, this.transform.localScale.z);
        this.transform.localScale = new Vector3(this.transform.localScale.x + scaleDown, this.transform.localScale.y + scaleDown, this.transform.localScale.z);
        yield return null;
        this.transform.localScale = new Vector3(0, 0, 0);
        
		Debug.Log("consume ends");
		this.gameObject.SetActive(false);

		yield  break;
	}

    void PlayCoinPickupSound() {
        coinAudio.PlayOneShot(coinAudio.clip);
    }

}
