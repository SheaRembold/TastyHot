using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Project : MonoBehaviour
{
    public float speed = 1f;
    Vector3 dir = new Vector3(1, 0, 0);

    void Start()
    {
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
