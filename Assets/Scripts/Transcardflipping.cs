using UnityEngine;
using System.Collections;

public class Transcardflipping : MonoBehaviour {

	public Texture[] tcards;
	public int randomtextlength = 0;
	public int chosentext = 0;
	int transformspeed = 0;
	public int transformspeedMin = 0;
	public int transformspeedMax = 0;


	// Use this for initialization
	void Start () {

		randomtextlength = tcards.Length;
		chosentext = Random.Range(0,randomtextlength);
		renderer.material.mainTexture = tcards[chosentext];
		StartCoroutine(Tcardsflip());
        transformspeed = Random.Range(transformspeedMin, transformspeedMax);
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(transformspeed * Time.deltaTime * -1,0,0);
        if (transform.position.x > Camera.main.transform.position.x && !renderer.isVisible)
            Destroy(gameObject);
	
	}

	IEnumerator Tcardsflip()
	{
		yield return new WaitForSeconds(.5f);
		chosentext = Random.Range(0,randomtextlength);
		renderer.material.mainTexture = tcards[chosentext];
		quickjump();
	}

	void quickjump()
	{
		StartCoroutine(Tcardsflip());
	}
}
