using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "SoundWave") {
			other.gameObject.GetComponent<WaveSegment>().wave.DestroyWaveSegment(other.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
