using UnityEngine;
using System.Collections;

public class droplet : MonoBehaviour {
    public float maxSpeed = 10f;
    public bool grounded = true;
    public LayerMask whatIsGround;
    public float JumpForce = 50;
    private float groundRadius = 1.5f;
    private Animator animator;
	private int temperature = 21;
    private bool facingRight = true;

    private CircleCollider2D groundCheck;

    // Use this for initialization
    void Start () {
        groundCheck = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        Camera.main.GetComponent<SmoothCamera>().target = gameObject;
    }
	
	// Update is called once per frame
	void Update () {

         grounded = Physics2D.OverlapCircle(transform.position, groundRadius, whatIsGround);
        float move = Input.GetAxis("Horizontal");
        // anim.SetFloat("Speed", Mathf.Abs(move));
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        if (grounded) // this if is so he can't move while jumping
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }


        if (GetComponent<Rigidbody2D>().velocity.x != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }



        if (grounded && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
        }
        //if (Inpu)
    }

    void Flip()
    {
        //Debug.Log("switching...");
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void IncrementTemperature(int delta){
		temperature += delta;
		//Do state switching check here.

	}
}
