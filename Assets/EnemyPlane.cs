using UnityEngine;
using System.Collections;

public class EnemyPlane : MonoBehaviour
{
    public GameObject bullet;
    public Transform fireFrom;
    float fireRate = 0.3f;
    float sinceFire = 0;
    Vector3 dir = new Vector3(1, 1, 0);

    void Update()
    {
        transform.position += dir * 1f * Time.deltaTime;
        Vector3 inView = Camera.main.WorldToViewportPoint(transform.position);
        if (inView.y >= 1)
            dir.y = -1;
        else if (inView.y <= 0)
            dir.y = 1;
        if (inView.x >= 0)
        {
            sinceFire += Time.deltaTime;
            if (sinceFire >= fireRate)
            {
                GameObject.Instantiate(bullet, fireFrom.position, Quaternion.Euler(0, 180, 0));
                sinceFire = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Project")
        {
            GameObject.Destroy(col.gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}
