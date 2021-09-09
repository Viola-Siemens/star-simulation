using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class Text3DRotate : MonoBehaviour {
	void Update() {
		transform.eulerAngles = Camera.main.transform.eulerAngles.y * Vector3.up;
	}
}
