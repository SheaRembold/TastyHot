using UnityEngine;
using System.Collections;

public class ReaganGrow : MonoBehaviour
{
    public float growSpeed = 0.1f;
    public AudioClip[] randomAudio;
    public float betweenMin = 2f;
    public float betweenMax = 5f;
    float tillNext;
    float sinceLast;
    bool countingDown;
    float countdown = 3;

    void Start()
    {
        tillNext = Random.Range(betweenMin, betweenMax);
    }

    void Update()
    {
        transform.localScale += transform.localScale * growSpeed * Time.deltaTime;

        //float screenWidth = Camera.main.aspect * Camera.main.orthographicSize * 2;
        sinceLast += Time.deltaTime;
        if (sinceLast >= tillNext)
        {
            int aci = Random.Range(0, randomAudio.Length);
            audio.clip = randomAudio[aci];
            audio.Play();
            sinceLast = 0;
            tillNext = Random.Range(betweenMin, betweenMax) + randomAudio[aci].length;
        }
        audio.volume = transform.localScale.x;

        if (countingDown)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
                Application.LoadLevel("selectLevel");
        }
        else
        {
            if (transform.localScale.x >= 1)
            {
                GameObject[] allObj = GameObject.FindObjectsOfType<GameObject>();
                for (int i = 0; i < allObj.Length; i++)
                    if (allObj[i] != gameObject && allObj[i] != Camera.main.gameObject)
                        GameObject.Destroy(allObj[i]);
                countingDown = true;
            }
        }
    }
}
