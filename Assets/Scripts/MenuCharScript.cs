using UnityEngine;
using System.Collections;

public class MenuCharScript : MonoBehaviour {

    public Rotate ball;
    public bool moving;

	// Use this for initialization
	void Start () {
	
	}

	bool falling = true;
	//Vector3 grav = Vector3.up*-3;
	//Vector3 speed = Vector3.right*3;

	float grav;
	float speed;
	float acc = 2.4f;
	
	float noHit;
	int vertMov;
	int horzMov;

	public GameObject bullet;

	RaycastHit2D[] hitTest = new RaycastHit2D[0];
	
	// Update is called once per frame
	void FixedUpdate () {
		falling = true;
		hitTest = Physics2D.CircleCastAll (transform.position, 0.6f, -Vector3.up);
		foreach (RaycastHit2D hit in hitTest) {
			hitInfo (hit);
		}

        if (falling)
            transform.parent.parent = null;


		//Normal Movement
		//falling = true;
		if (moving) {
			speed += acc;
		} else {
			if (speed > 0) {
				speed -= acc;
			} else if (speed < 0) {
				speed += acc;
			}
		}

		if (Mathf.Abs (speed) < 0.2f) {
			speed = 0;
		} else if (speed > 16) {
			speed = 16;
		} else if (speed < -16) {
			speed = -16;
		}

        ball.Spin(speed);

		if (falling) {
			grav -= acc;
			if (grav < -16f) {
				grav = -16f;
			}
		}

		transform.position += new Vector3(speed, grav, 0)*Time.deltaTime;
	}

	void hitInfo(RaycastHit2D hit){
        if (hit.collider != null && hit.distance <= 0.1f)
        {
            if (hit.collider.tag == "Platform")
            {
                transform.parent.parent = hit.collider.transform;
				falling = false;
				grav = 0;
			}
            else
            {
                Application.LoadLevel(hit.collider.tag);
            }
		}
	}
}
