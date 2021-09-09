using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
	public float MouseTurn = 6.0f;

	void Update() {
		transform.Rotate(0, Input.GetAxis("Mouse X") * MouseTurn, 0); ;
	}
}
