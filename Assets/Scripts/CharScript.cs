using UnityEngine;
using System.Collections;

public class CharScript : MonoBehaviour {

    public Rotate ball;
    public Transform tail;
    public ParticleSystem groundParticle;
    public ParticleSystem ssParticle;

    public AudioClip jumpSound;
    public AudioClip[] hitSounds;

	// Use this for initialization
	void Start () {
	
	}

	bool falling = true;
	//Vector3 grav = Vector3.up*-3;
	//Vector3 speed = Vector3.right*3;

	float grav;
	float speed;
	float acc = 0.8f;
	float charge = 0f;
	int health = 3;
	float noHit;
	bool invinc = false;
	bool gradius = false;
	int vertMov;
	int horzMov;
	bool canRight = true;
	bool canLeft = true;

	public GameObject bullet;

	RaycastHit2D[] hitTest = new RaycastHit2D[0];
	
	// Update is called once per frame
	void FixedUpdate () {
		if(health <= 0){
			Application.LoadLevel ("selectLevel");
		}

		falling = true;
		canRight = true;
		canLeft = true;

		hitTest = Physics2D.CircleCastAll (transform.position, 0.6f, -Vector3.up);
		foreach (RaycastHit2D hit in hitTest) {
			hitInfo (hit);
		}

        if (falling)
            transform.parent.parent = null;

		//Invincible Frames
		if (invinc) {
			noHit -= 0.1f;

			if(noHit <= 0){
				invinc = false;
			}
		}

		//Attacking
		if (Input.GetButton ("Fire")) {
			charge += 0.1f;

			if(gradius){
				if (charge >= 1) {
					Instantiate (bullet, transform.position, Quaternion.identity);
					charge = 0;
				}
			}else{
				if (charge >= 5) {
					Instantiate (bullet, transform.position, transform.rotation);
					charge = 0;
				}
			}
		}
        else
        {
			charge = 0;
		}

        if (gradius)
            ssParticle.emissionRate = charge * 500;
        else
            ssParticle.emissionRate = charge * 100;

		//Movement
		if (gradius) {
			//Gradius Movement
			falling = false;
			vertMov = 0;
			horzMov = 0;

			if (Input.GetButton ("Right")) {
				horzMov = 1;
			}
			if (Input.GetButton ("Left")) {
				horzMov = -1;
			}
			if (Input.GetButton ("Up")) {
				vertMov = 1;
			}
			if (Input.GetButton ("Down")) {
				vertMov = -1;
			}

			transform.position += new Vector3(horzMov, vertMov, 0)*8*Time.deltaTime;
		} else {
			//Normal Movement
			//falling = true;
			if (Input.GetButton ("Right") && canRight) {
				speed += acc;
                tail.parent = transform;
				transform.LookAt (transform.position + Vector3.back);
                tail.parent = transform.parent;
			} else if (Input.GetButton ("Left") && canLeft) {
				speed -= acc;
                tail.parent = transform;
				transform.LookAt (transform.position + Vector3.forward);
                tail.parent = transform.parent;
			} else {
				if (speed > 0) {
					speed -= acc;
				} else if (speed < 0) {
					speed += acc;
				}
			}

			if (Mathf.Abs (speed) < 0.2f) {
				speed = 0;
			} else if (speed > 6) {
				speed = 6;
			} else if (speed < -6) {
				speed = -6;
			}

            ball.Spin(speed);
            if (falling)
                groundParticle.emissionRate = 0;
            else
                groundParticle.emissionRate = Mathf.Abs(speed) * 10;

			//Jumping
			if (Input.GetButton ("Jump") && falling == false) {
				grav = 16f;
				falling = true;
                //audio.PlayOneShot(jumpSound);
			}
			
			if (falling) {
				grav -= acc;
				if (grav < -16f) {
					grav = -16f;
				}
			}

			transform.position += new Vector3(speed, grav, 0)*Time.deltaTime;
		}

        if (transform.position.y < (gradius ? -10 : -5))
        {
            Application.LoadLevel("selectLevel");
        }
	}

	void hitInfo(RaycastHit2D hit){
		if (hit.collider != null && hit.distance <= 0.1f) {
            if (hit.collider.tag == "Platform")
            {
				if(hit.point.y < transform.position.y){
					falling = false;
					//transform.parent = hit.collider.transform;
					grav = 0;
				}else if(hit.point.y > transform.position.y){
					grav = 0;
				}else if(hit.point.x > transform.position.x){
					speed = -1;
					canRight = false;
					
				}else if(hit.point.x < transform.position.x){
					speed = 1;
					canLeft = false;
				}
               /*transform.parent.parent = hit.collider.transform;
				falling = false;
				grav = 0;*/
			}

			if((hit.collider.tag == "Enemy" || hit.collider.tag == "Brain") && !invinc){
				health--;
				noHit = 6;
				invinc = true;
                Debug.Log("I'm hit!  " + health);
                audio.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
			}

			if(hit.collider.tag == "Lava"){
                health -= 3;
                audio.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
			}

			if(hit.collider.tag == "Coin"){
				Destroy (hit.collider.gameObject);
				Debug.Log ("Coin Get!");
			}

            if (hit.collider.tag == "Portal")
            {
                hit.collider.gameObject.GetComponent<CompleteLevel>().StartClosing(transform.parent.gameObject);
                Destroy(this);
            }
            if (hit.collider.tag == "WhaleRide")
            {
                hit.collider.transform.parent = transform;
                hit.collider.tag = "Untagged";
                gradius = true;
            }
		}
	}
}
