using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsManager : MonoBehaviour
{
    public AudioClip[] musics;
    public static List<string> completed = new List<string>();

    public static void FinishLevel(string level)
    {
        if (!completed.Contains(level))
            completed.Add(level);
    }

    void Start()
    {
        audio.clip = musics[completed.Count];
        audio.Play();
    }
}
