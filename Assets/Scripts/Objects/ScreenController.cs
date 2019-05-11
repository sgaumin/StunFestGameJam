using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour
{
    public float timerSpeed = 1F;
    float timer = 1F;
    float originalSize;
    float totalTime = 0F;

    public Image timerFill;
    public GameObject mire;


    // Start is called before the first frame update
    void Start()
    {
        originalSize = timerFill.rectTransform.rect.height;
        mire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        if(timerFill.rectTransform.rect.height > 0)
        {
            // resize the timer fill
            float newSize = originalSize * (1 - totalTime * timerSpeed);
            timerFill.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize);
        } else
        {
            Debug.Log("timer over");
            // show the mire when time is over
            mire.SetActive(true);
        }

    }
}
