using UnityEngine;
using System.Collections;

public class CardPicker : MonoBehaviour
{
    public GameObject[] cards;
    public float selectScale = 1.1f;
    int selected = 0;
    public MenuCharScript player;

    void Start()
    {
        cards[selected].transform.localScale = Vector3.one * selectScale;
        cards[selected].transform.localPosition += new Vector3(0, 0.6f, 0);
        cards[selected].transform.GetChild(0).gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.moving = true;
            gameObject.SetActive(false);
        }
        else if (Input.GetButtonDown("Left"))
        {
            cards[selected].transform.localScale = Vector3.one;
            cards[selected].transform.localPosition -= new Vector3(0, 0.6f, 0);
            cards[selected].transform.GetChild(0).gameObject.SetActive(false);
            selected--;
            if (selected < 0)
                selected = cards.Length - 1;
            cards[selected].transform.localScale = Vector3.one * selectScale;
            cards[selected].transform.localPosition += new Vector3(0, 0.6f, 0);
            cards[selected].transform.GetChild(0).gameObject.SetActive(true);

        }
        else if (Input.GetButtonDown("Right"))
        {
            cards[selected].transform.localScale = Vector3.one;
            cards[selected].transform.localPosition -= new Vector3(0, 0.6f, 0);
            cards[selected].transform.GetChild(0).gameObject.SetActive(false);
            selected++;
            if (selected > cards.Length - 1)
                selected = 0;
            cards[selected].transform.localScale = Vector3.one * selectScale;
            cards[selected].transform.localPosition += new Vector3(0, 0.6f, 0);
            cards[selected].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
