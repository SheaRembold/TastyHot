using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public float speedMult = 180;

    public void Spin(float speed)
    {
        Vector3 euler = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(euler.x, euler.y, euler.z - speed * speedMult * Time.deltaTime * Mathf.Sign(transform.parent.right.x));
    }
}
