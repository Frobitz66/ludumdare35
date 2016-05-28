using UnityEngine;
using System.Collections;

public class droplet : MonoBehaviour {
	public const float START_TEMP = 21.0f;
    public float maxSpeed = 10f;
    public bool grounded = true;
    public LayerMask whatIsGround;
    public float JumpForce = 50;
    private Animator animator;
	private float temperature = START_TEMP;
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

	private void ResetDropletStateToWater() {
		temperature = START_TEMP;
		SetDropletState (DropletState.Water);
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
		ResetDropletStateToWater();
    }

	public void Respawn(){
		isAlive = true;
		animator.SetBool("isAlive", true);
		SetDropletState (DropletState.Water);
		temperature = 21.0f;

		if(spawnPoint == null){
			var spawnPointScript = GameObject.FindObjectOfType<SpawnPoint>();
			if(spawnPoint == null){
				Debug.Log("Unable to find spawn point in level!  Picking arbitrary place.");
				var crystals = GameObject.FindObjectsOfType<crystal>();
				if(crystals.Length > 0){
					spawnPoint = crystals[0].gameObject;
				}
				else{
					//well, we're screwed.
				}
			}
			else{
				spawnPoint = spawnPointScript.gameObject;
			}
		}

		this.transform.position = (spawnPoint == null) ? startingPosition : spawnPoint.transform.position;
		ResetDropletStateToWater ();
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

	public void PlayAudio(AudioClip sound){
		AudioSource[] audios = gameObject.GetComponents<AudioSource>();
		if(audios.Length == 0)
			return;

		AudioSource slot = audios[0];
		if(slot.isPlaying)
			slot.Stop();
		
		audios[0].clip = sound;
		audios[0].Play();
	}

	public void StopAudio(AudioClip sound){
		AudioSource[] audios = gameObject.GetComponents<AudioSource>();
		if(audios.Length == 0)
			return;

		foreach(var slot in audios){
			if(slot.clip == sound)
				slot.Stop();
		}
	}

	public void StopAllAudio(){
		AudioSource[] audios = gameObject.GetComponents<AudioSource>();
		if(audios.Length == 0)
			return;

		foreach(var slot in audios){
			slot.Stop();
		}
	}
}
