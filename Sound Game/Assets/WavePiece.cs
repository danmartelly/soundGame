using UnityEngine;
using System.Collections;

public class WavePiece : MonoBehaviour {

	public Vector2 startPosition;
	public Vector2 speed;
	public float maxTravelDistance;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		speed = new Vector2 (.05f, .05f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 prev = transform.position;
		transform.position = new Vector3(prev.x+speed.x, prev.y+speed.y, prev.z);
	}
}
