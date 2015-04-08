using UnityEngine;
using System.Collections;

public class Drum : MonoBehaviour {

	public SoundCircle soundCircle;
	public AudioClip drumSound;

	// Use this for initialization
	void Awake () {
		soundCircle.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("collided");
		if (other.gameObject.tag == "SoundWave" && !soundCircle.gameObject.activeSelf) {
			SoundManager.instance.PlaySingle(drumSound);
			soundCircle.gameObject.SetActive(true);
			soundCircle.transform.position = transform.position;
			soundCircle.transform.localScale = new Vector3(.2F,.2F,.2F);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
