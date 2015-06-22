using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	public GameObject wavePrefab;
	public int totalSegments;
	public float velocity;
	public bool startActivated = false;
	public float maxRadius;
	protected float segmentWidth;
	protected GameObject[] waves;
	protected float radius;
	protected Vector3[] waveVelocities;
	protected bool active = false;
	// Use this for initialization
	void Start () {
		segmentWidth = wavePrefab.GetComponent<Renderer>().bounds.size.x;
		if (startActivated) {
			ConstructWaveFront();
			active = true;
		}
	}

	void ConstructWaveFront() {
		radius = 0;
		Debug.Log ("constructing wavefront");
		waves = new GameObject[totalSegments];
		//waveVelocities = new Vector3[totalSegments];
		for (int i = 0; i < this.totalSegments; i++) {
			float angle = 2f*Mathf.PI/totalSegments*i;
			//Vector3 vel = new Vector3(Mathf.Cos(angle)*velocity, Mathf.Sin(angle)*velocity, 0f);
			GameObject instance = Instantiate (wavePrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
			instance.transform.Rotate(0f,0f,57.29f*(angle)+90f);
			instance.GetComponent<WaveSegment>().wave = this;
			waves[i] = instance;
			//waveVelocities[i] = vel;
		}
	}

	void DestroyWaveFront() {
		Debug.Log ("destroying wavefront");
		for (int i = 0; i < waves.Length; i++) {
			GameObject obj = waves[i];
			if (obj == null)
				continue;
			Object.Destroy(obj);
		}
		this.active = false;
		waves = new GameObject[0];
	}

	public void DestroyWaveSegment(GameObject waveSegment) {
		for (int i = 0; i < waves.Length; i++) {
			GameObject obj = waves[i];
			if (obj != null && waveSegment.GetInstanceID() == obj.GetInstanceID()) {
				waves[i] = null;
				Object.Destroy(obj);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "SoundWave" && !this.active) {
			ConstructWaveFront();
			active = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			float angle = 2f * Mathf.PI / totalSegments;
			this.radius += this.velocity;
			//Vector3 difference = waves [0].transform.position - this.gameObject.transform.position;
			//difference.z = 0f;
			//float radius = difference.magnitude;
			if (radius > maxRadius) {
				DestroyWaveFront();
				return;
			}
			float desiredWidth = radius * Mathf.Sin (angle) * 1.1f;
			float newXScale = desiredWidth / segmentWidth;
			for (int i = 0; i < this.totalSegments; i++) {
				GameObject wave = waves [i];
				if (wave == null)
					continue;
				//Vector3 vel = waveVelocities [i];
				Vector3 displacement = new Vector3(Mathf.Cos(angle*i)*this.radius, Mathf.Sin(angle*i)*this.radius, 0f);
				wave.transform.position = this.gameObject.transform.position + displacement;
				Vector3 scale = wave.transform.localScale;
				scale.x = newXScale;
				wave.transform.localScale = scale;
			}
		}
	}
}
