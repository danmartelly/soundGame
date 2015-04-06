using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public SoundCircle soundCircle;

	// Use this for initialization
	void Awake () {
		soundCircle.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D> ();
		rb2d.AddForce(new Vector2(horizontal*10, vertical*10));
		if (Input.GetKeyUp("e")) {
			// ring appears
			soundCircle.gameObject.SetActive(true);
			soundCircle.transform.position = transform.position;
			soundCircle.transform.localScale = new Vector3(.2F,.2F,.2F);
			Debug.Log("player space");
		}
	}
}
