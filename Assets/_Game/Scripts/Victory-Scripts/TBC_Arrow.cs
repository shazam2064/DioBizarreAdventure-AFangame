using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TBC_Arrow : MonoBehaviour
{
    public bool isImgOn = false;
    public Image img;
    private float timer = 45f;

    void Start()
    {
        img.enabled = false;
        isImgOn = false;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            img.enabled = true;
            isImgOn = true;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(44f);
    }
}