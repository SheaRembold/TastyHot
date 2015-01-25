using UnityEngine;
using System.Collections;

public class WhaleController : MonoBehaviour
{
    enum SprayState { Between, Warn, Up, Pause, Down };

    public Vector2 speed = new Vector2(1f, 1f);
    float dirX = 1;
    float sinceChange;
    float tillNext;
    public float timeMin = 2;
    public float timeMax = 5;
    public float scaleFactor = 0.75f;
    float offset;
    public float hoverSpeed = 1f;
    public GameObject spray;
    public GameObject sprayTop;
    public float betweenSpray = 5f;
    public float sprayWarnTime = 1f;
    public float sprayMoveTime = 1f;
    public float sprayPauseTime = 1f;
    float sprayStateTime = 0;
    public float sprayHeight = 20f;
    SprayState sprayState = SprayState.Between;

    void Start()
    {
        if (Random.value > 0.5f)
            dirX *= -1;
        tillNext = Random.Range(timeMin, timeMax);
        offset = Random.Range(0, 2 * Mathf.PI);
    }

    void Update()
    {
        sinceChange += Time.deltaTime;
        if (sinceChange >= tillNext)
        {
            dirX *= -1;
            sinceChange = 0;
            tillNext = Random.Range(timeMin, timeMax);
        }
        transform.position += new Vector3(dirX * speed.x, Mathf.Sin(Time.time * hoverSpeed + offset) * speed.y, 0) * Time.deltaTime;

        if (sprayState == SprayState.Between)
        {
            sprayStateTime += Time.deltaTime;
            if (sprayStateTime >= betweenSpray)
            {
                sprayState = SprayState.Warn;
                sprayStateTime = 0;
                sprayTop.SetActive(true);
            }
        }
        else if (sprayState == SprayState.Warn)
        {
            sprayStateTime += Time.deltaTime;
            if (sprayStateTime >= sprayWarnTime)
            {
                sprayState = SprayState.Up;
                sprayStateTime = 0;
            }
        }
        else if (sprayState == SprayState.Up)
        {
            sprayStateTime += Time.deltaTime;
            spray.transform.localScale = new Vector3(spray.transform.localScale.x, Mathf.Lerp(0, sprayHeight, sprayStateTime / sprayMoveTime), 1f);
            sprayTop.transform.localPosition = new Vector3(sprayTop.transform.localPosition.x, spray.transform.localPosition.y + spray.renderer.bounds.size.y, 0f);
            if (sprayStateTime >= sprayMoveTime)
            {
                sprayState = SprayState.Pause;
                sprayStateTime = 0;
            }
        }
        else if (sprayState == SprayState.Pause)
        {
            sprayStateTime += Time.deltaTime;
            if (sprayStateTime >= sprayPauseTime)
            {
                sprayState = SprayState.Down;
                sprayStateTime = 0;
            }
        }
        else if (sprayState == SprayState.Down)
        {
            sprayStateTime += Time.deltaTime;
            spray.transform.localScale = new Vector3(spray.transform.localScale.x, Mathf.Lerp(sprayHeight, 0, sprayStateTime / sprayMoveTime), 1f);
            sprayTop.transform.localPosition = new Vector3(sprayTop.transform.localPosition.x, spray.transform.localPosition.y + spray.renderer.bounds.size.y, 0f);
            if (sprayStateTime >= sprayMoveTime)
            {
                sprayState = SprayState.Between;
                sprayStateTime = 0;
                sprayTop.SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
    }
}
