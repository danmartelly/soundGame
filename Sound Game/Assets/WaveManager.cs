using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

	public GameObject wavePrefab;
	public int totalSegments;
	public float velocity;
	protected float segmentWidth;
	protected GameObject[] waves;
	protected Vector3[] waveVelocities;
	// Use this for initialization
	void Start () {
		segmentWidth = wavePrefab.GetComponent<Renderer>().bounds.size.x;
		waves = new GameObject[totalSegments];
		waveVelocities = new Vector3[totalSegments];
		Vector3 defaultAxis = new Vector3 (1f, 0f, 0f);
		for (int i = 0; i < this.totalSegments; i++) {
			float angle = 2f*Mathf.PI/totalSegments*i;
			Vector3 vel = new Vector3(Mathf.Cos(angle)*velocity, Mathf.Sin(angle)*velocity, 0f);
			GameObject instance = Instantiate (wavePrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
			instance.transform.Rotate(0f,0f,57.29f*(angle)+90f);
			//instance.transform.Rotate(0f,0f,90);
			waves[i] = instance;
			waveVelocities[i] = vel;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float angle = 2f*Mathf.PI/totalSegments;
		Vector3 difference = waves[0].transform.position - this.gameObject.transform.position;
		difference.z = 0f;
		float radius = difference.magnitude;
		float desiredWidth = radius*Mathf.Sin (angle)*1.1f;
		float newXScale = desiredWidth/segmentWidth;
		for (int i = 0; i < this.totalSegments; i++) {
			GameObject wave = waves[i];
			Vector3 vel = waveVelocities[i];
			wave.transform.position = wave.transform.position + vel;
			Vector3 scale = wave.transform.localScale;// = wave.transform.localScale + new Vector3(0.1f,0f,0f);
			scale.x = newXScale;
			wave.transform.localScale = scale;
		}
		//wave2.transform.position = wave2.transform.position + new Vector3 (-.1f, .1f, 0f);
	}
}
