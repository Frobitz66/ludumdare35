using UnityEngine;
using System.Collections;

public class droplet : MonoBehaviour {
    public float maxSpeed = 10f;
    public bool grounded = true;
    public LayerMask whatIsGround;
    public float JumpForce = 50;
    private float groundRadius = 1.5f;

    private CircleCollider2D groundCheck;

    // Use this for initialization
    void Start () {
        groundCheck = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.OverlapCircle(transform.position, groundRadius, whatIsGround);
        float move = Input.GetAxis("Horizontal");
        // anim.SetFloat("Speed", Mathf.Abs(move));

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (grounded && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
        }
        //if (Inpu)
    }
}
