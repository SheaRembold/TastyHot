using UnityEngine;
using System.Collections;

public class DropPlayer : MonoBehaviour
{
    public GameObject picker;
    public GameObject player;
    bool dropped;
    float time;
    GameObject inst;

    void Start()
    {
        transform.localScale = Vector3.zero;
        audio.volume = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time < 0.5f)
        {
            transform.localScale = Vector3.one * (time / 0.5f);
            audio.volume = time / 0.5f;
        }
        else if (time < 1f)
        {
            if (!dropped)
            {
                transform.localScale = Vector3.one;
                audio.volume = 1f;
                inst = GameObject.Instantiate(player, transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
                dropped = true;
            }
        }
        else if (time < 1.5f)
        {
            float interp = 1f - (time - 1f) / 0.5f;
            transform.localScale = Vector3.one * interp;
            audio.volume = interp;
        }
        else
        {
            picker.GetComponent<CardPicker>().player = inst.GetComponentInChildren<MenuCharScript>();
            picker.SetActive(true); 
            GameObject.Destroy(gameObject);
        }
    }
}
