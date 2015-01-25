using UnityEngine;
using System.Collections;

public class KillParticle : MonoBehaviour
{
    void Update()
    {
        if (!particleSystem.IsAlive(false))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
