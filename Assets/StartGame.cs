using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Application.LoadLevel("selectLevel");
        }
    }
}
