using UnityEngine;
using System.Collections;

public class DropperPlatform : MonoBehaviour
{
    public GameObject icycle;

    void Start()
    {

    }

    void Update()
    {
        if (transform.childCount < 1)
        {
            GameObject obj = GameObject.Instantiate(icycle) as GameObject;
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), -0.5f, 0f);
        }
    }
}
