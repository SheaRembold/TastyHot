using UnityEngine;
using System.Collections;

public class RotateHole : MonoBehaviour
{
    public float speed = 180;

    public void Update()
    {
        Vector3 euler = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(euler.x, euler.y, euler.z - speed * Time.deltaTime);
    }
}
