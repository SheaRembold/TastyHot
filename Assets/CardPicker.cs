using UnityEngine;
using System.Collections;

public class CardPicker : MonoBehaviour
{
    public GameObject[] cards;
    public string[] scenes;
    public float selectScale = 1.1f;
    int selected = 0;

    void Start()
    {
        cards[selected].transform.localScale = Vector3.one * selectScale;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Application.LoadLevel(scenes[selected]);
        }
        else if (Input.GetButtonDown("Left"))
        {
            cards[selected].transform.localScale = Vector3.one;
            selected--;
            if (selected < 0)
                selected = cards.Length - 1;
            cards[selected].transform.localScale = Vector3.one * selectScale;

        }
        else if (Input.GetButtonDown("Right"))
        {
            cards[selected].transform.localScale = Vector3.one;
            selected++;
            if (selected > cards.Length - 1)
                selected = 0;
            cards[selected].transform.localScale = Vector3.one * selectScale;
        }
    }
}
