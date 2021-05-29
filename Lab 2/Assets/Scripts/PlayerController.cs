using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float upSpeed = 30;

    private Rigidbody2D marioBody;
    private bool onGroundState = true;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    private  Animator marioAnimator;
    private AudioSource marioAudio;


    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {


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
	    marioAudio.PlayOneShot(marioAudio.clip);
    }


}
