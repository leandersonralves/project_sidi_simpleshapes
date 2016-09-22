using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Background : MonoBehaviour
{
    public float timeFade;
    public Color backgroundColor;

    public delegate void OnCompleteFade();
    public OnCompleteFade onCompleteFadeIn;
    public OnCompleteFade onCompleteFadeOut;

    Coroutine fade;
    Image backgroundImage;

    void Awake ()
    {
        backgroundImage = GetComponentInChildren<Image>();
    }

    public void Fade()
    {
        fade = StartCoroutine(Fade(true));
    }
    
    IEnumerator Fade (bool isIn)
    {
        //Fade In
        backgroundImage.CrossFadeAlpha(1f, timeFade, true);
        float timeLeft = 0f;
        while (timeLeft < timeFade)
        {
            timeLeft += Time.deltaTime;
            yield return null;
        }

        if (onCompleteFadeIn != null)
            onCompleteFadeIn.Invoke();
        
        //Fade Out
        backgroundImage.CrossFadeAlpha(0f, timeFade, true);
        timeLeft = 0f;
        while (timeLeft < timeFade)
        {
            timeLeft += Time.deltaTime;
            yield return null;
        }

        if (onCompleteFadeOut != null)
            onCompleteFadeOut.Invoke();
    }
}
