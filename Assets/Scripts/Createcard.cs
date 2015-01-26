using UnityEngine;
using System.Collections;

public class Createcard : MonoBehaviour {

	public GameObject tcard;
	int	waittime = 0;
	public int	waittimeMin = 0;
	public int	waittimeMax = 0;

	// Use this for initialization
	void Start () {
        looper();
	}
	IEnumerator Createobj()
	{

		yield return new WaitForSeconds(waittime);
		Instantiate(tcard,transform.position,transform.rotation);
		looper();
	}

	void looper()
	{
        waittime = Random.Range(waittimeMin, waittimeMax);
		StartCoroutine(Createobj());
	}
}
