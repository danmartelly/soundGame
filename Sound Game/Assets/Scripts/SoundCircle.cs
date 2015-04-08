using UnityEngine;
using System.Collections;

public class SoundCircle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.localScale;
		if (temp.x > .5) {
			gameObject.SetActive (false);
		} else {
			transform.localScale = new Vector3 (temp.x + .01F, temp.y + .01F, temp.z);
		}
	}
}
