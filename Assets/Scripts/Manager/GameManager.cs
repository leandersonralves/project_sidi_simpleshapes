using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Classe que gerencia o estado do jogo e possui funções para controle do mesmo.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Atraso para sair do jogo.
    /// </summary>
    private const float delayQuit = 2;

    public delegate void OnChangeState(GameState state);

    /// <summary>
    /// Evento chamado quando há mudança de estado do jogo.
    /// </summary>
    public static OnChangeState onChangeState;

    /// <summary>
    /// Estado atual do jogo.
    /// </summary>
    private GameState state = GameState.InitialMenu;
    public enum GameState
    {
        InitialMenu,
        Loading,
        InGame,
        Quiting
    }

    /// <summary>
    /// Referência para o Background do jogo, afim de controlar Fade.
    /// </summary>
    [SerializeField]
    Background background;

    void Awake()
    {
        if (!background)
        {
            Debug.LogError("Not found Background component in " + name + " in Scene " + SceneManager.GetActiveScene().name);
            Destroy(this);
        }

        onChangeState(state);
    }

    /// <summary>
    /// Define estado do som.
    /// </summary>
    /// <param name="isEnable">Se true, será habilitado, e vice-versa.</param>
    public void SetSoundState(bool isEnable)
    {
        AudioListener.pause = !isEnable;
    }

    /// <summary>
    /// Carrega uma cena do build.
    /// </summary>
    /// <param name="sceneName">Nome da cena à ser carregada.</param>
    public void LoadScene(string sceneName)
	{
        //Define o estado para Loading e chama o evento.
        state = GameState.Loading;
        onChangeState(state);

        background.Fade();

        background.onCompleteFadeIn = delegate () {
            SceneManager.LoadScene(sceneName);
            state = GameState.InGame;
            onChangeState(state);
        };
    }

    /// <summary>
    /// Sai do jogo, com delay.
    /// </summary>
    public void Quit ()
    {
        state = GameState.Quiting;
        onChangeState(state);
        StartCoroutine(DelayedQuit());
    }

    /// <summary>
    /// IEnumerator para sair do jogo, com delay.
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayedQuit ()
    {
        //Tempo de delay.
        yield return new WaitForSeconds(delayQuit);
        Application.Quit();
    }
}
