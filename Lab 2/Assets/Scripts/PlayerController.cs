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


    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGroundState)
        {

        }

    }

  // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate() {
    
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // stop
            marioBody.velocity = Vector2.zero;
        }

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed * 2);
        } else {
            // stop horizontal movement
            marioBody.velocity = new Vector2(0, marioBody.velocity.y);
        }

        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) {
            onGroundState = true; // back on ground
        }
    }


}
