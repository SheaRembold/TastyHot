using UnityEngine;
using System.Collections;

public class Icycle : MonoBehaviour
{
    Vector3 targetScale;
    public float growTime = 1f;
    public float fallSpeed = 1f;
    bool growing = true;
    float timeGrowing;

    void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (growing)
        {
            timeGrowing += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, timeGrowing / growTime);
            //transform.localPosition = new Vector3(transform.localPosition.x, -transform.localScale.y / 2f - 0.5f, 0f);
            if (timeGrowing >= growTime)
            {
                growing = false;
                transform.parent = null;
            }
        }
        else
        {
            transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
            if (transform.position.y < -5f)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
