using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PepeScript : MonoBehaviour
{
    #region Member Variables
    /// <summary>
    /// Player movement speed
    /// </summary>
    private float movementSpeed = 70.0f;

    /// <summary>
    /// Animation state machine local reference
    /// </summary>
    private Animator animator;
    private SpriteRenderer spriteRd;

    /// <summary>
    /// The last position of the player in previous frame
    /// </summary>
    private Vector3 lastPosition;

    /// Les images d'état du vieux
    public Image Face;
    public Sprite[] sprites;

    /// <summary>
    /// The last checkpoint position that we have saved
    /// </summary>
    private Vector3 CheckPointPosition;

    /// <summary>
    /// Is the player dead?
    /// </summary>
    private bool isDead = false;
    #endregion

    public float speedy = 100.0f;

    // Use this for initialization
    void Start()
    {
        // get the local reference
        animator = GetComponent<Animator>();
        spriteRd = GetComponent<SpriteRenderer>();
        Show("");

        // set initial position
        lastPosition = transform.position;
        CheckPointPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // check for player exiting the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // get the input this frame
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        animator.SetBool("running", Mathf.Abs(horizontal) > 0.01);
       
        // reset the velocity each frame
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        // horizontal movement, left or right, set animation type and speed 
        if (horizontal > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * speedy, 0));
            //animator.SetInteger("Direction", 1);
        }
        else if (horizontal < 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * -speedy, 0));
            //animator.SetInteger("Direction", 3);
        }

        // vertical movement, up or down, set animation type and speed 
        if (vertical > 0)
        {
            //transform.Translate(0, movementSpeed * 0.9f * Time.deltaTime, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, movementSpeed * Time.deltaTime);
            //animator.SetInteger("Direction", 0);
        }
        else if (vertical < 0)
        {
            //transform.Translate(0, -movementSpeed *  0.9f * Time.deltaTime, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -movementSpeed * Time.deltaTime);
            //animator.SetInteger("Direction", 2);
        }

        //compare this position to the last known one, are we moving?
        //if (this.transform.position == lastPosition)
        //{
            // we aren't moving so make sure we dont animate
            //animator.speed = 0.0f;
        //}

        // get the last known position
        lastPosition = transform.position;

        // if we are dead do not move anymore
        if (isDead == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            //animator.speed = 0.0f;
        }
        if (horizontal < 0.0f && !spriteRd.flipX || horizontal > 0.0f && spriteRd.flipX)
        {
            spriteRd.flipX = !spriteRd.flipX;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ramassable"))
        {
            Show("curiosity");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ramassable"))
        {
            Show("");
        }
    }

    private void Show(string etat)
    {
        if(etat == "curiosity")
        {
            Face.sprite = sprites[1];
        }
        if (etat == "choc")
        {
            Face.sprite = sprites[2];
        }
        if (etat == "dos")
        {
            Face.sprite = sprites[3];
        }
        if (etat == "pensif")
        {
            Face.sprite = sprites[4];
        }
        if (etat == "neutre")
        {
            Face.sprite = sprites[5];
        }
        if (etat == "profil")
        {
            Face.sprite = sprites[6];
        }
        if (etat == "enerve")
        {
            Face.sprite = sprites[7];
        }
        if (etat == "contrarie")
        {
            Face.sprite = sprites[8];
        }
        if (etat == "")
        {
            Face.enabled = false;
        }
        else Face.enabled = true;
    }

}
