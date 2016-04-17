using UnityEngine;
using System.Collections;

public class droplet : MonoBehaviour {
    public float maxSpeed = 10f;
    public bool grounded = true;
    public LayerMask whatIsGround;
    public float JumpForce = 50;
    private float groundRadius = 1.5f;
    private Animator animator;
	private float temperature = 21.0f;
	public int Lives = 3;
	private bool isAlive = true;
	private float respawnTime; //The time at which we should respawn
	private const float timeItTakesToRespawn = 3.0f; //The time from death that it takes us to respawn.
	private Vector3 startingPosition;

	public enum DropletState {
		Ice = -1,
		Water = 0,
		Gas = 1
	};

	private DropletState state;

    private bool facingRight = true;

    private CircleCollider2D groundCheck;

	public delegate void LivesChanged(int lives);
	public event LivesChanged OnLivesChanged;

    // Use this for initialization
    void Start () {
        groundCheck = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
		isAlive = true;
		startingPosition = this.transform.position;
        Camera.main.GetComponent<SmoothCamera>().target = gameObject;
    }
	
	// Update is called once per frame
	void Update () {

		if (!isAlive) {
			if (Time.time >= respawnTime)
				Respawn ();
			return;
		}

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

		// Can only move when in gas state or not jumping
		if (state == DropletState.Gas || grounded) 
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

		//Can't jump while in gas state
		if (state != DropletState.Gas && grounded && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
        }
    }
		
	public float GetTemperature(){
		return temperature;
	}

	public float GetMaxTemperature(){
		return 175.0f;
	}

	public float GetMinTemperature(){
		return -75.0f;
	}

	public void IncrementTemperature(float delta){
		if (!isAlive)
			return;

		temperature += delta;

		if (temperature >= GetMaxTemperature () || temperature <= GetMinTemperature ()) {
			KillTheDroplet ();
			return;
		}
		//TODO: Do state switching check here.
	}

    void Flip()
    {
        //Debug.Log("switching...");
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	public void KillTheDroplet(){
		if (!isAlive)
			return;
		
		Lives--;
		if (OnLivesChanged != null)
			OnLivesChanged (Lives);

		isAlive = false;
		animator.SetBool("isAlive", false);

		//TODO: 
		//-Play death animation for current state.
		respawnTime = Time.time + timeItTakesToRespawn;
	}

	public void SetDropletState(DropletState newState){
		state = newState;
		animator.SetInteger("dropletState", (int)state);
		//TODO: setup state-specfic stuff here
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		switch (state) {
		case DropletState.Gas:
			rigidBody.gravityScale = -1;
			break;
		case DropletState.Water:
			rigidBody.gravityScale = 1;
            break;
		};

	}

	private void Respawn(){
		isAlive = true;
		animator.SetBool("isAlive", true);
		temperature = 21.0f;
		this.transform.position = startingPosition;
	}
}
