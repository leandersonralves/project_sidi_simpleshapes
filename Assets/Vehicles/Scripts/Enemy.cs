using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Classe para controle dos carros Inimigos.
/// </summary>
public class Enemy : Car
{
    /// <summary>
    /// Distância para o Alvo.
    /// </summary>
	private float DistanceToTarget
	{
		get {
			return Vector3.Distance (
				m_transform.position, 
				Singletons.GetPlayer ().transform.position
			);
		}
	}

    /// <summary>
    /// Estado atual da IA do Carro.
    /// </summary>
	[SerializeField]
	private State state;
	private enum State
	{
		Waiting,
		Threatening,
		Chasing,
		Broked
	}

	/// <summary>
	/// Referencia para o componente Text.
	/// </summary>
	public Text textHeath;

	void Start () {
        //Inicia a máquina de estados da IA, não utilizado o evento Update,
        //pois é mais prático permanecer em um estado, utilizando Coroutines.
		StartCoroutine (StateMachine());
	}

	void Update () {
		textHeath.text = heath.ToString("000");

		if (heath <= 0f)
			state = State.Broked;
			
	}

    /// <summary>
    /// IEnumerator para atualização da máquina de estados.
    /// </summary>
    /// <returns></returns>
	IEnumerator StateMachine () {

        while (state != State.Broked)
        {
            //Após sair de cada estado, é checado qual será o próximo estado.
            switch (state)
            {
                case State.Chasing:
                    Chasing();
                    CheckState();
                    break;
                case State.Threatening:
                    yield return StartCoroutine(Threatening());
                    CheckState();
                    break;
                case State.Waiting:
                    Waiting();
                    CheckState();
                    break;
            }

            yield return null;
        }

        Broke();
	}

    /// <summary>
    /// Função que o veículo segue o alvo.
    /// </summary>
	void Chasing ()
	{
		Vector3 targetPosition = Singletons.GetPlayer().transform.position;
		Vector3 actualPosition = m_transform.position;

        //Checando a direção do alvo.
		Vector3 targetDirection = (targetPosition - actualPosition).normalized;

        //Checando se o alvo está à frente. Se estiver a frente, irá seguir reto.
		if (Vector3.Dot (m_transform.forward, targetDirection) < 0.9) {

            //Checando se o alvo está mais a direita ou esquerda.
            if (Vector3.Dot(m_transform.right, targetDirection) > 0)
                Turn(1f);
            else
                Turn(-1f);

		} else {
			Turn (0f);
		}

        //Acelera o veículo.
		Acellerate (1f);
	}

    /// <summary>
    /// Função que o veículo fica "ameaçando", acelerando e freiando.
    /// </summary>
    /// <returns></returns>
	IEnumerator Threatening ()
	{
        Turn(0f);
		Brake();

		float timeLeft = 4f;
		while(timeLeft > 0f)
		{
			timeLeft -= Time.deltaTime;

			Acellerate (0.5f);
			yield return new WaitForSeconds (0.5f);
			Brake ();

			yield return null;
		}
	}

    /// <summary>
    /// Tempo que o veículo está virando para um dado lado, usado pela máquina de estados.
    /// </summary>
    private float timeTurning = 0f;

    /// <summary>
    /// Cache do última direção que o carro está virando.
    /// </summary>
    private bool isLeft = true;

    /// <summary>
    /// Função com o estado que o veículo fica andando e virando de um lado para o outro.
    /// </summary>
	void Waiting ()
	{
		Acellerate (0.2f);

        //Checa o tempo que está virando para um lado, se for maior que 3 irá virar para o outro lado.
		if (timeTurning >= 3f) {
			timeTurning = 0f;
            isLeft = !isLeft;
		}

        if (isLeft)
        {
            Turn(-0.7f);
        }
        else
        {
            Turn(0.7f);
        }

		timeTurning += Time.deltaTime;
	}
    
    /// <summary>
    /// Checa o estado atual da máquina de estados e define qual deverá ser definido.
    /// </summary>
	private void CheckState ()
	{
		switch (state) {
		case State.Chasing:
			if (DistanceToTarget < 15f && DistanceToTarget > 12f)
				state = State.Threatening;
			else if (DistanceToTarget > 25f)
				state = State.Waiting;
			break;
		case State.Threatening:
			if (DistanceToTarget > 25f)
				state = State.Waiting;
			else
				state = State.Chasing;
			break;
		case State.Waiting:
			if (DistanceToTarget <= 25f)
				state = State.Chasing;
			break;
		}
	}
}
