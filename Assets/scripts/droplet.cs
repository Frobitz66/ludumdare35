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
	private float defaultGravityScale = 1.0f;
	private bool isTouchingWater = false;
	private bool canMove = true;
    private GameObject spawnPoint;


	public enum DropletState {
		Ice = -1,
		Water = 0,
		Gas = 1
	};

	private DropletState state;

    private bool facingRight = true;

    private CircleCollider2D groundCheck;

	public delegate void PlayerKilled(string killer);
	public event PlayerKilled OnPlayerKilled;

	public delegate void LivesChanged(int lives);
	public event LivesChanged OnLivesChanged;

	public delegate void StateChanged(DropletState newState);
	public event StateChanged OnStateChanged;

	public delegate void PlayerSpawned();
	public event PlayerSpawned OnPlayerSpawned;

	public delegate void GameOver();
	public event GameOver OnGameOver;

    void OnGUI()
    {
        //GUIStyle style = new GUIStyle();
        //style.normal.textColor = Color.black;
        //GUILayout.Label("");
        //GUILayout.Label("");
        //GUILayout.Label("");
        //GUILayout.Label("");
        //GUILayout.Label("temp: " + temperature, style);
        //GUILayout.Label("state: " + state, style);
    }

	//Keep the player betweens levels.  After a game over, we will
	//find and destroy the current player so that lives, etc, are reset.
	void Awake(){
		DontDestroyOnLoad (this.gameObject);

    }

    // Use this for initialization
    void Start () {

        groundCheck = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
		isAlive = true;
		//startingPosition = this.transform.position;
        Camera.main.GetComponent<SmoothCamera>().target = gameObject;
		var rigidBody = GetComponent<Rigidbody2D> ();
		if (rigidBody)
			defaultGravityScale = rigidBody.gravityScale;
		state = DropletState.Water;
    }
	
	// Update is called once per frame
	void Update () {

		if (!isAlive) {
			if (Time.time >= respawnTime  && respawnTime >= 0.0f)
				Respawn ();
			return;
		}

		grounded = groundCheck.IsTouchingLayers (whatIsGround);
		if (state == DropletState.Ice && !grounded) {
			grounded = isTouchingWater;
		} 
        //grounded = Physics2D.OverlapCircle(transform.position, groundRadius, whatIsGround);
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
		if (canMove && (state == DropletState.Gas || grounded))
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
		if (state != DropletState.Gas && state != DropletState.Ice && grounded && Input.GetButtonDown("Jump") && canMove)
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

	public float GetWarningTemperatureMax(){
		return 150.0f;
	}

	public float GetMinTemperature(){
		return -75.0f;
	}

	public float GetWarningTemperatureMin(){
		return -50.0f;
	}

	// Change the Temperature of Droplet (the name is misleading)
	public void IncrementTemperature(float delta, string incrementerName){
		if (!isAlive)
			return;

		temperature += delta;

		if (temperature >= GetMaxTemperature () || temperature <= GetMinTemperature ()) {
			KillTheDroplet (incrementerName);
			return;
		}

		//State switching check.
		if (temperature >= 100.0f) {
			SetDropletState (DropletState.Gas);
		} else if (temperature > 0.0f) {
			SetDropletState (DropletState.Water);
		} else {
			SetDropletState (DropletState.Ice);
		}
	}

    void Flip()
    {
        //Debug.Log("switching...");
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	public void KillTheDroplet(string killerName){
		if (!isAlive)
			return;
		
		Lives--;
		if (OnLivesChanged != null)
			OnLivesChanged (Lives);

		if (OnPlayerKilled != null)
			OnPlayerKilled (killerName);

		isAlive = false;
		animator.SetBool("isAlive", false);

		if (Lives <= 0) {
			respawnTime = -1.0f;
			if (OnGameOver != null)
				OnGameOver ();
			return;
		}

		//TODO: 
		//-Play death animation for current state.
		respawnTime = Time.time + timeItTakesToRespawn;
	}

	public void SetDropletState(DropletState newState){
		if (state == newState)
			return;
		
		state = newState;
		animator.SetInteger("dropletState", (int)state);
		//TODO: setup state-specfic stuff here

		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		switch (state) {
		case DropletState.Gas:
			rigidBody.gravityScale = -(defaultGravityScale * .1f);
			rigidBody.drag = 1;
			rigidBody.mass = 1;
			break;
		case DropletState.Water:
			rigidBody.gravityScale = defaultGravityScale;
			rigidBody.drag = 1;
			rigidBody.mass = 1;
			break;
		case DropletState.Ice:
			rigidBody.gravityScale = defaultGravityScale * 2;  // this should make his jumps a little bit weaker
			rigidBody.drag = 0;
			rigidBody.mass = 0.9f;	// this should ensure that he floats
			break;
		};

		if (OnStateChanged != null)
			OnStateChanged (state);
	}

	public DropletState GetDropletState(){
		return state;
	}

    public void SpawnAt(GameObject spawnPoint)
    {
        Camera.main.GetComponent<SmoothCamera>().target = gameObject;
        transform.position = spawnPoint.transform.position;
        this.spawnPoint = spawnPoint;
        this.startingPosition = spawnPoint.transform.position;
    }

	private void Respawn(){
		isAlive = true;
		animator.SetBool("isAlive", true);
		SetDropletState (DropletState.Water);
		temperature = 21.0f;
		this.transform.position = startingPosition;
		if (OnPlayerSpawned != null)
			OnPlayerSpawned ();

		respawnTime = -1.0f;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.layer == LayerMask.NameToLayer ("Water"))
			isTouchingWater = true;
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.layer == LayerMask.NameToLayer ("Water"))
			isTouchingWater = false;
	}

	public bool GetCanMove(){
		return canMove;
	}

	public void SetCanMove(bool canMove){
		this.canMove = canMove;
	}
}
