using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Classe para controle do carro através de Inputs do jogador.
/// </summary>
public class Player : Car
{
    //Quantidade de aceleração. [0-1]
	[SerializeField]
    private Slider accelerate;

    //Nível de esterçamento. [0-1]
	[SerializeField]
	private Slider turn;

	void Awake ()
	{
		base.Awake ();

		if (!accelerate || !turn) {
			Debug.LogError ("Not found Sliders to control Player.");
			Destroy (this);
		}
	}

    void Update () {
		if (Mathf.Approximately (accelerate.value, 0f))
            Brake();
        else
			Acellerate (accelerate.value);

		if (Mathf.Approximately (turn.value, 0))
			Turn (0);
		else
			Turn (turn.value);

        if(heath <= 0)
        {
            Singletons.GetGameManager().LoadScene("tier_1");
        }
	}
}
