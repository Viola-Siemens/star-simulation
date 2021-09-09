using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomRuler : MonoBehaviour {
	public GameObject objectPrefab;
	public int N = 80;
	CelestialBody[] bodies;

	public Vector3 RandomVector {
		get {
			return (Random.value - 0.5f) * Vector3.right + (Random.value - 0.5f) * Vector3.up + (Random.value - 0.5f) * Vector3.forward;
		}
	}

	private void Awake() {
		bodies = new CelestialBody[N];
		for (int i = 0; i < N; ++i) {
			bodies[i] = Instantiate(objectPrefab, RandomVector * 50.0f, Quaternion.identity).GetComponent<CelestialBody>();
			MeshRenderer meshRenderer = bodies[i].GetComponent<MeshRenderer>();
			meshRenderer.material.color = new Color(
				1.0f - Mathf.Pow(Random.value, 2.0f) * 0.5f,
				1.0f - Mathf.Pow(Random.value, 2.0f) * 0.5f,
				1.0f - Mathf.Pow(Random.value, 2.0f) * 0.5f
			);
			meshRenderer.material.SetColor("_EmissionColor", meshRenderer.material.color);
		}
	}

	private void Start() {
		for (int i = 0; i < N; ++i) {
			bodies[i].rig.mass = Random.value * 20.0f + Random.value * 20.0f + 10.0f;
			bodies[i].transform.localScale = new Vector3(
				Mathf.Pow(bodies[i].rig.mass, 1.0f / 3.0f),
				Mathf.Pow(bodies[i].rig.mass, 1.0f / 3.0f),
				Mathf.Pow(bodies[i].rig.mass, 1.0f / 3.0f)
			);
			bodies[i].rig.AddForce(bodies[i].transform.position.normalized / bodies[i].transform.position.magnitude * 40000.0f + RandomVector * 10.0f);
		}
	}
}
