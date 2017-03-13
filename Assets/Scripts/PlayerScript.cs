using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private Rigidbody2D m_RB = null;
    private int m_speed = 10;
    private Animator m_Anim = null;
    private SpriteRenderer spriteRd;
    private int m_Grounded = 0;
    private int m_InContact = 0;
    public int jump_speed = 200;
    private bool SawFirstTeddy = false;
    public bool firstTeddy = false;
    public bool secondTeddy = false;
    public Text TeddyText;
    public Text WarningText;
    public int delay = 2;
    private bool displayingtext = false;

    // Use this for initialization
    void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        spriteRd = GetComponent<SpriteRenderer>();
        TeddyText.text = "";
        WarningText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(firstTeddy)
        {
            if (!displayingtext)
                WarningText.text = "Press Up!";
            if (Input.GetButtonDown("Vertical"))
            {
                SawFirstTeddy = true;
                if(!displayingtext)
                    StartCoroutine(ShowMessage(1, delay));
            }
        }
        if(secondTeddy && SawFirstTeddy)
        {
            if (!displayingtext)
                WarningText.text = "Press Jump!";
            if (Input.GetButtonDown("Jump"))
            {
                if (!displayingtext)
                    StartCoroutine(ShowMessage(2, delay));
            }
        }

    }

    void FixedUpdate()
    {

        float h = 0;
        if (m_InContact == 0 || m_Grounded > 0)
        {
            h = Input.GetAxis("Horizontal");
            m_RB.velocity = new Vector2(h * m_speed, m_RB.velocity.y);
            m_Anim.SetFloat("vSpeed", -0.1f * m_RB.velocity.y);
            m_Anim.SetBool("running", Mathf.Abs(h) > 0.01);
        }
        if (h < 0.0f && !spriteRd.flipX || h > 0.0f && spriteRd.flipX)
        {
            spriteRd.flipX = !spriteRd.flipX;
        }

        if (m_Grounded > 0)
        {
            Debug.DrawRay(m_RB.position, new Vector3(0, -10, 0), Color.blue, 1);
            if (Input.GetButtonDown("Jump"))
            {
                m_RB.AddForce(new Vector2(0, jump_speed));

            }

        }
        m_Anim.SetBool("OnGround", m_Grounded > 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_InContact++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        m_InContact--;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_Grounded++;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Peluche_0"))
        {
            firstTeddy = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Peluche_1"))
        {
            secondTeddy = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_Grounded--;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Peluche_0"))
        {
            firstTeddy = false;
            WarningText.text = "";
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Peluche_1"))
        {
            secondTeddy = false;
            WarningText.text = "";
        }
    }
    IEnumerator ShowMessage (int Teddy, int delay)
    {
        WarningText.text = "";
        displayingtext = true;
        if (Teddy == 1)
        {
            TeddyText.text = "What are you doing here, little boy?";
            yield return new WaitForSeconds(delay);
            TeddyText.text = "Hm? What with the long face? ";
            yield return new WaitForSeconds(delay);
            TeddyText.text = "You lost your friend?";
            yield return new WaitForSeconds(delay);
            TeddyText.text = "";
        }
        if(Teddy == 2)
        {
            TeddyText.text = "Ah! There you are!";
            yield return new WaitForSeconds(delay);
            TeddyText.text = "How did you get here?";
            yield return new WaitForSeconds(delay);
            TeddyText.text = "And I will I reach you now?";
            yield return new WaitForSeconds(delay);
            TeddyText.text = "...";
            yield return new WaitForSeconds(delay);
            TeddyText.text = "";
        }
        displayingtext = false;
    }
}
