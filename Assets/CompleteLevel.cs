using UnityEngine;
using System.Collections;

public class CompleteLevel : MonoBehaviour
{
    float time;
    bool closing = false;
    GameObject player;
    Vector3 playerStartPos;

    public void StartClosing(GameObject player)
    {
        closing = true;
        audio.Play();
        this.player = player;
        playerStartPos = player.transform.position;
    }

    void Update()
    {
        if (closing)
        {
            time += Time.deltaTime;
            if (time < 0.75f)
            {
                transform.localScale = Vector3.one * (1f -  time / 1.5f);
                player.transform.position = Vector3.Lerp(playerStartPos, transform.position, time / 0.75f);
                player.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, time / 0.75f);
            }
            else if (time < 1.5f)
            {
                transform.localScale = Vector3.one * (1f - time / 1.5f);
                audio.volume = 1f - (time - 0.75f) / 0.75f;
            }
            else
            {
                LevelsManager.FinishLevel(Application.loadedLevelName);
                Application.LoadLevel("selectLevel");
            }
        }
    }
}
