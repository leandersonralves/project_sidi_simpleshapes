using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    private static MenuManager m_menu;

    void Awake ()
    {
        if(m_menu)
        {
            Destroy(this);
        }

        m_menu = this;
        GameManager.onChangeState += ChangingState;
    }

    private void ChangingState (GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.InGame:
                break;
            case GameManager.GameState.InitialMenu:
                break;
            case GameManager.GameState.Loading:
                break;
            case GameManager.GameState.Quiting:
                break;
        }
    }
}
