using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrainController : MonoBehaviour
{
    public float speed = 1f;
    Vector3 dir = new Vector3(1, 0, 0);
    float sinceChange;
    float tillNext;
    public float timeMin = 2;
    public float timeMax = 5;
    public float scaleFactor = 0.75f;
    [System.NonSerialized]
    public BrainController merger;
    List<BrainController> mergingBrains = new List<BrainController>();
    List<Vector3> startPos = new List<Vector3>();
    List<Vector3> startScale = new List<Vector3>();
    public float mergeTime = 1;
    float timeMerging;
    Vector3 mergePos;
    Vector3 mergedScale;
    [System.NonSerialized]
    public bool splitting = false;
    [System.NonSerialized]
    public Vector3 splitStartPos;
    [System.NonSerialized]
    public Vector3 splitTargetPos;
    [System.NonSerialized]
    public Vector3 splitStartScale;
    [System.NonSerialized]
    public Vector3 splitTargetScale;

    float gravity = -10f;

    void Start()
    {
        if (Random.value > 0.5f)
            dir.x *= -1;
        tillNext = Random.Range(timeMin, timeMax);
    }

    void Update()
    {
        if (splitting)
        {
            timeMerging += Time.deltaTime;
            transform.position = Vector3.Lerp(splitStartPos, splitTargetPos, timeMerging / mergeTime);
            transform.localScale = Vector3.Lerp(splitStartScale, splitTargetScale, timeMerging / mergeTime);
            if (timeMerging >= mergeTime)
            {
                splitting = false;
            }
        }
        else if (merger == null)
        {
            sinceChange += Time.deltaTime;
            RaycastHit2D hit;
            if (dir.x < 0)
                hit = Physics2D.Raycast(transform.position - new Vector3(0.5f, 0, 0), new Vector2(0, -1), Mathf.Infinity, 1 << LayerMask.NameToLayer("Platform"));
            else
                hit = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 0), new Vector2(0, -1), Mathf.Infinity, 1 << LayerMask.NameToLayer("Platform"));
            //if (sinceChange >= tillNext)
            if (hit.collider == null)
            {
                dir.x *= -1;
                sinceChange = 0;
                tillNext = Random.Range(timeMin, timeMax);
            }

            RaycastHit2D[] hitTest = Physics2D.CircleCastAll(transform.position, 0.5f, -Vector3.up);
            bool falling = true;
            foreach (RaycastHit2D hitCirc in hitTest)
            {
                if (hitCirc.collider != null && hitCirc.distance <= 0.1f)
                {
                    if (hitCirc.collider.tag == "Platform")
                    {
                        falling = false;
                    }
                }
            }
            if (falling)
                dir.y += gravity * Time.deltaTime;
            else
                dir.y = 0;
            transform.position += new Vector3(dir.x * speed, dir.y, 0) * Time.deltaTime;
        }
        else if (merger == this)
        {
            timeMerging += Time.deltaTime;
            for (int i = 0; i < mergingBrains.Count; i++)
            {
                mergingBrains[i].transform.position = Vector3.Lerp(startPos[i], mergePos, timeMerging / mergeTime);
                mergingBrains[i].transform.localScale = Vector3.Lerp(startScale[i], mergedScale, timeMerging / mergeTime);
            }
            if (timeMerging >= mergeTime)
            {
                for (int i = 0; i < mergingBrains.Count; i++)
                {
                    if (mergingBrains[i] != this)
                        GameObject.Destroy(mergingBrains[i].gameObject);
                }
                mergingBrains.Clear();
                startPos.Clear();
                startScale.Clear();
                merger = null;
            }
        }
        transform.right = new Vector3(-dir.x, 0, 0);
    }

    public void AddMergingBrain(BrainController brain)
    {
        mergingBrains.Add(brain);
        startPos.Add(brain.transform.position);
        startScale.Add(brain.transform.localScale);
        mergedScale += brain.transform.localScale * scaleFactor;
    }

    public void AbortMerge()
    {
        float sep = mergedScale.x / (2 * scaleFactor) + 1f;
        float begin = -sep * (mergingBrains.Count - 1) / 2f;
        for (int i = 1; i < mergingBrains.Count; i++)
        {
            mergingBrains[i].merger = null;
            mergingBrains[i].splitting = true;
            mergingBrains[i].splitStartPos = mergingBrains[i].transform.position;
            mergingBrains[i].splitTargetPos = mergePos + new Vector3(begin + sep * i, 0, 0);
            mergingBrains[i].splitStartScale = mergingBrains[i].transform.localScale;
            mergingBrains[i].splitTargetScale = startScale[i];
            mergingBrains[i].timeMerging = 0;
        }
        mergingBrains.Clear();
        startPos.Clear();
        startScale.Clear();
    }

    public void Split(float direction)
    {
        splitting = true;
        splitStartPos = transform.position;
        splitStartScale = transform.localScale;
        splitTargetScale = transform.localScale / (2 * scaleFactor);
        splitTargetPos = transform.position + new Vector3(direction * (splitTargetScale.x / 2f + 0.5f), 0, 0);
        dir.x = direction;
        timeMerging = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Brain")
        {
            BrainController other = col.gameObject.GetComponent<BrainController>();
            if (merger == null && !splitting || !other.splitting)
            {
                if (other.merger == null)
                {
                    mergePos = (transform.position + col.gameObject.transform.position) / 2f;
                    mergedScale = Vector3.zero;
                    timeMerging = 0;
                    AddMergingBrain(this);
                    merger = this;
                }
                else
                {
                    other.merger.AddMergingBrain(this);
                }
            }
        }
        else if (col.gameObject.tag == "Project")
        {
            if (merger != null)
            {
                merger.AbortMerge();
            }
            else
            {
                if (transform.localScale.x < 0.25f)
                {
                    GameObject.Destroy(gameObject);
                }
                else
                {
                    GameObject obj = GameObject.Instantiate(gameObject) as GameObject;
                    obj.GetComponent<BrainController>().Split(1);
                    Split(-1);
                }
                col.gameObject.tag = "Untagged";
                GameObject.Destroy(col.gameObject);
            }
        }
    }
}
