using UnityEngine;
using System.Collections;

/// <summary>
/// Classe para controle do carro através de Inputs do jogador.
/// </summary>
public class Player : Car
{
    //Quantidade de aceleração. [0-1]
    public float accelerate = 0f;

    //Nível de esterçamento. [0-1]
    public float turn = 0f;

    void Update () {
		if (Mathf.Approximately (accelerate, 0f))
            Brake();
        else
            Acellerate (accelerate);

		if (Mathf.Approximately (turn, 0))
			Turn (0);
		else
            Turn (turn);

        if(heath <= 0)
        {
            Singletons.GetGameManager().LoadScene("tier_1");
        }
	}
}
