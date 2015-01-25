using UnityEngine;
using System.Collections;

public class PlaneManager : MonoBehaviour
{
    public GameObject plane;
    float fireRate = 2f;
    float sinceFire = 0;

    void Update()
    {
        sinceFire += Time.deltaTime;
        if (sinceFire >= fireRate)
        {
            GameObject obj = GameObject.Instantiate(plane) as GameObject;
            obj.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0) + new Vector3(-10.5f, Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize), 0);
            fireRate -= 0.1f;
            if (fireRate < 0.5f)
                fireRate = 0.5f;
            sinceFire = 0;
        }
    }
}
