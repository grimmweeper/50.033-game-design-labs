using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MushroomSpawn : MonoBehaviour
{
    private Vector2 velocityVector;
    private float velocity = 4.0f;
    private bool isMoving = true;
    private int xDirection = 1;
    private Rigidbody2D mushroomBody;
    // private bool collected = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomBody.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (isMoving) {
            MoveMushroom();
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Pipe")) {
            xDirection *= -1;
            Debug.Log("Mushroom collides with Pillar and change direction");
        }

        if (col.gameObject.CompareTag("Mario")) {
            isMoving = false;
            // collected = true;
            StartCoroutine(consumeSequence());
            Debug.Log("consume sequence ends");
            
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
		// this.gameObject.SetActive(false);

		yield  break;
	}


    void MoveMushroom() {
        velocityVector = new Vector2(velocity * xDirection, mushroomBody.velocity.y);
        mushroomBody.velocity = velocityVector;
    }

}
