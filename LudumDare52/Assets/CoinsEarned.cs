using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsEarned : MonoBehaviour
{
    public TextMesh textMesh;

    public int Amount;

    public float Livetime;

    public Color32 StartColor = new Color32(255, 207, 0, 255);

    private void Start()
    {
        if (Amount != 0)
        {
            textMesh.text = "+" + Amount;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Amount != 0)
        {
            transform.position += Vector3.up * Time.fixedDeltaTime;
            StartCoroutine(FadeColor());
            StartCoroutine(Destroy());
            //Play Coin Sound
        }
        else
        {
            //Play Poop Sound
        }
    }

    private IEnumerator FadeColor()
    {
        Color32 endColor = new Color32(StartColor.r, StartColor.g, StartColor.b, 0);
        float time = 0;
        while (time < Livetime)
        {
            time += Time.deltaTime;
            textMesh.color = Color.Lerp(StartColor, endColor, time / Livetime);
            yield return null;
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(Livetime);
        Destroy(this.gameObject);
    }
    
}
