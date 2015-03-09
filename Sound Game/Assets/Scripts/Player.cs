using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D> ();
		rb2d.AddForce(new Vector2(horizontal*10, vertical*10));
	}
}
