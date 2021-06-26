using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed = 10;
    public float upSpeed = 30;

    private Rigidbody2D marioBody;
    private bool onGroundState = true;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    private Animator marioAnimator;
    // private AudioSource marioAudio;
    // private AudioSource marioDieAudio;

    public AudioClip marioJumpAudioClip;
    public AudioClip marioDieAudioClip;
    

    private GameObject cameraManager;



    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();

        GameManager.OnPlayerDeath  +=  PlayerDiesSequence;

        cameraManager = GameObject.Find("Main Camera");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z,this.gameObject);
        }

        if (Input.GetKeyDown("x")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.X,this.gameObject);
        }
    }

  // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate() {

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        

        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
            // Check velocity
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
                marioAnimator.SetTrigger("onSkid");
            }

        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
            // Check velocity
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
                marioAnimator.SetTrigger("onSkid");
            }
        }


        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        } else {
            // stop horizontal movement
            marioBody.velocity = new Vector2(0, marioBody.velocity.y);
        }

        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
            PlayJumpSound();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        string[] objectTagArray = {"Ground", "MushroomSpawn", "Platform", "QuestionBox", "Pipe"};
        GroundCollision(col, objectTagArray);

    }

    void GroundCollision(Collision2D col, string[] objectTagArray) {
        for (int i = 0; i < objectTagArray.Length; i++) {
            if (col.gameObject.CompareTag(objectTagArray[i]) && Mathf.Abs(marioBody.velocity.y) < 0.01f) {
                onGroundState = true;
                marioAnimator.SetBool("onGround", onGroundState);
                Debug.Log("MARIO COLLIDES WITH: "+ objectTagArray[i]);
            }
        }
        
    }

    void PlayJumpSound(){
        GetComponent<AudioSource>().PlayOneShot(marioJumpAudioClip);
    }

    void  PlayerDiesSequence(){
        // Mario dies
        Debug.Log("Mario dies");
        // do whatever you want here, animate etc
        StartCoroutine(dieAnimation());
        this.gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;

        cameraManager.GetComponent<CameraController>().stopBackgroundSound();
        PlayDieSound();


    }

    IEnumerator dieAnimation(){
		for (int i =  0; i  < 2; i  ++){

			this.transform.position  =  new  Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
			yield  return  null;
		}
		// this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
        
		yield  break;
	}

    void PlayDieSound() {
        GetComponent<AudioSource>().PlayOneShot(marioDieAudioClip);
    }


}
