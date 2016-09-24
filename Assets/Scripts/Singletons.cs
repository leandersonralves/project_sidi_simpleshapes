using UnityEngine;

/// <summary>
/// Pattern Singleton para garantir a instância de apenas um Player e facilitar o acesso ao mesmo.
/// </summary>
public static class Singletons
{
	private static GameObject player;

    /// <summary>
    /// Função para acessar o Player.
    /// </summary>
    /// <returns>GameObject com tag Player.</returns>
	public static GameObject GetPlayer ()
	{
        //Checando se já há um Player na cena.
		if (!player) {
			if (GameObject.FindWithTag ("Player") == null) {
				Debug.LogError ("Not Found GameObject with Tag (Player)");
				return null;
			}

			player = GameObject.FindWithTag ("Player");
		}

		return player;
	}

    private static GameManager gameManager;

    /// <summary>
    /// Função para acessar o Player.
    /// </summary>
    /// <returns>GameObject com tag Player.</returns>
	public static GameManager GetGameManager()
    {
        //Checando se já há um Player na cena.
        if (!gameManager)
        {
            if (GameObject.FindObjectOfType<GameManager>() == null)
            {
                Debug.LogError("Not Found GameObject with Component GameManager");
                return null;
            }

            gameManager = GameObject.FindObjectOfType<GameManager>();
        }

        return gameManager;
    }
}

