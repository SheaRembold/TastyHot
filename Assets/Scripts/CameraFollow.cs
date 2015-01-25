using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public GameObject player;
	
	// Update is called once per frame
	void Update () {
		Vector3 charPos = camera.WorldToViewportPoint (player.transform.position);

		if (charPos.x > 0.7f) {
			transform.position += Vector3.right*8*Time.deltaTime;
		}

		if (charPos.x < 0.3f) {
			transform.position -= Vector3.right*8*Time.deltaTime;
		}

		if (charPos.y > 0.5) {
			transform.position += Vector3.up*8*Time.deltaTime;
		}

		if (charPos.y < 0.3) {
			transform.position -= Vector3.up*8*Time.deltaTime;
		}
	}
}
