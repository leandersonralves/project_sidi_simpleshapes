using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Classe para manipular o canvas de fundo.
/// </summary>
public class Background : MonoBehaviour
{
    /// <summary>
    /// Tempo para fade. 
    /// O tempo total de Fade é o dobro do definido, pois há o Fade In e Out.
    /// </summary>
    public float timeFade;

    public delegate void OnCompleteFade();

    /// <summary>
    /// Evento chamado ao completar o FadeIn.
    /// </summary>
    public OnCompleteFade onCompleteFadeIn;

    /// <summary>
    /// Evento chamado ao completar o FadeOut.
    /// </summary>
    public OnCompleteFade onCompleteFadeOut;

    /// <summary>
    /// Cache do IEnumerator para uso em corotina, para evitar o acúmuldo de chamadas.
    /// </summary>
	IEnumerator fade;

    /// <summary>
    /// Referência para o component Image do Background.
    /// </summary>
    Image backgroundImage;

    void Awake ()
    {
        backgroundImage = GetComponentInChildren<Image>();
    }

    /// <summary>
    /// Função que executa o Fade In e em seguida Fade Out do Background.
    /// </summary>
    public void Fade()
    {
		if (fade != null) {
			StopCoroutine (fade);
		}

		fade = Fading();
        StartCoroutine(fade);
    }
    

    /// <summary>
    /// IEnumerator que executa o FadeIn e Out.
    /// </summary>
    /// <returns>Não há retorno, função para Coroutines.</returns>
    IEnumerator Fading ()
    {
        //Fade In
        Color m_color = backgroundImage.color;
        float timeLeft = 0f;
        while (timeLeft < timeFade)
        {
            timeLeft += Time.deltaTime;
            m_color.a = 1 - (timeFade - timeLeft) / timeFade;
            backgroundImage.color = m_color;
            yield return null;
        }

        if (onCompleteFadeIn != null)
            onCompleteFadeIn.Invoke();
		
        //Fade Out
        timeLeft = 0f;
        while (timeLeft < timeFade)
        {
            timeLeft += Time.deltaTime;
            m_color.a = (timeFade - timeLeft) / timeFade;
            backgroundImage.color = m_color;
            yield return null;
        }

        if (onCompleteFadeOut != null)
            onCompleteFadeOut.Invoke();
    }
}
