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
        }
    }

    void OnBecameInvisible(){
        Debug.Log("MUSHIE DESTROYED!");
        Destroy(gameObject);	
    }

    void MoveMushroom() {
        velocityVector = new Vector2(velocity * xDirection, mushroomBody.velocity.y);
        mushroomBody.velocity = velocityVector;
    }

}
