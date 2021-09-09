using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : MonoBehaviour {
	public static readonly float G = 0.25f;

	public Vector3 initVelocity = Vector3.zero;

	[HideInInspector]
	public Rigidbody rig;

	CelestialBody[] bodies;

	private void Awake() {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
		bodies = new CelestialBody[objs.Length - 1];
		int cnt = 0;
		foreach (GameObject obj in objs) {
			if(obj == gameObject) {
				continue;
			}
			bodies[cnt] = obj.GetComponent<CelestialBody>();
			cnt += 1;
		}

		rig = GetComponent<Rigidbody>();
	}

	private void Start() {
		rig.AddForce(initVelocity, ForceMode.VelocityChange);
	}

	private void FixedUpdate() {
		Vector3 force = Vector3.zero;
		foreach(CelestialBody cb in bodies) {
			if(!cb) {
				continue;
			}
			Vector3 direct = cb.transform.position - transform.position;
			force += G * rig.mass * cb.rig.mass / direct.sqrMagnitude * direct.normalized;
		}
		rig.AddForce(force);
	}

	private void OnCollisionEnter(Collision collision) {
		if(collision.rigidbody.CompareTag("Player") && rig.mass > collision.rigidbody.mass) {
			rig.velocity = (rig.velocity * rig.mass + collision.rigidbody.velocity * collision.rigidbody.mass) / (rig.mass + collision.rigidbody.mass);
			rig.mass += collision.rigidbody.mass;
			transform.localScale = new Vector3(Mathf.Pow(rig.mass, 1.0f / 3.0f), Mathf.Pow(rig.mass, 1.0f / 3.0f), Mathf.Pow(rig.mass, 1.0f / 3.0f));
			Destroy(collision.gameObject);
		}
	}
}
