using UnityEngine;
using System.Collections;

public class BullScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	Vector3 moveLoc;
	// Update is called once per frame
	void Update () {
		transform.position += transform.right*20*Time.deltaTime;
	}
}
