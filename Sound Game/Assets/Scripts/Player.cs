using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//various numbers that control player movement 
	public float speed = 8; 
	public float acceleration = 30; 
	public float gravity = 20; 
	public float jumpHeight = 12;

	//player moving vars 
	private float currentSpeed; 
	private float maxSpeed; 
	private Vector2 amountToMove;

	//ray casting vars 
	private BoxCollider collider; 
	private Vector3 colliderSize; 
	private Vector3 colliderCenter; 
	Ray ray; 
	RaycastHit hit; 
	public LayerMask collisionLayer; 
	private float epsilon = 0.005f; 
	public bool grounded; 
	public SoundCircle soundCircle;

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider> (); 
		colliderSize = collider.size;
		colliderCenter = collider.center; 
	}
	

	// Use this for initialization
	void Awake () {
		//soundCircle.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		maxSpeed = Input.GetAxisRaw ("Horizontal") * speed; 
		currentSpeed = AccelerateTowards (currentSpeed, maxSpeed, acceleration); 

		if (grounded) {
			amountToMove.y = 0; 

			if (Input.GetButtonDown ("Jump")) {
				amountToMove.y = jumpHeight; 
			}
		}

		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime; 
		IncrementMovement (amountToMove * Time.deltaTime);


		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D> ();
		rb2d.AddForce(new Vector2(horizontal*10, vertical*10));
		if (Input.GetKeyUp ("e")) {
			// ring appears
			soundCircle.gameObject.SetActive (true);
			soundCircle.transform.position = transform.position;
			soundCircle.transform.localScale = new Vector3 (.2F, .2F, .2F);
			Debug.Log ("player space");
		}
	}

	private float AccelerateTowards(float currentSpeed, float targetSpeed, float acceleration) 
	{
		if (currentSpeed == targetSpeed) {
			return currentSpeed; 
		} else {
			float accelDirection = Mathf.Sign(targetSpeed - currentSpeed);
			currentSpeed += acceleration * Time.deltaTime * accelDirection; 
			float updatedAccelDirection = Mathf.Sign (targetSpeed - currentSpeed); 
			return (accelDirection == updatedAccelDirection) ? currentSpeed : targetSpeed; 
		}
	}

	private void IncrementMovement(Vector2 moveAmount) {
		float dy = moveAmount.y; 
		Vector2 pos = transform.position; 

		//creating the raycast vectors extending from top and bottom 
		grounded = false; 
		for (int i = 0; i < 3; i ++) {
			float yDir = Mathf.Sign (dy); 
			float rayX = (pos.x + colliderCenter.x - colliderSize.x/2.0f) + colliderSize.x/2.0f * i; //left most side of the box, moving in increments of half colliders 
			float rayY = pos.y + colliderCenter.y + colliderSize.y/2.0f * yDir; //bottom of the box 

			ray = new Ray(new Vector2(rayX, rayY), new Vector2(0, -1) );
			Debug.DrawRay (ray.origin, ray.direction); 

			if (Physics.Raycast (ray, out hit, Mathf.Abs(dy), collisionLayer)) {
				float dist = Vector3.Distance(ray.origin, hit.point);

				if (dist > epsilon) {
					dy = -dist + epsilon;
				} else {
					dy = 0; 
				}
				grounded = true;
				break;
			}
		}


		Vector2 newMoveAmount = new Vector2(moveAmount.x, dy); 
		transform.Translate (newMoveAmount);

	}
}
