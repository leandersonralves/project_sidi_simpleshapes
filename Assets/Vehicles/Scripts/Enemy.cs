using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Car
{
	private float DistanceToTarget
	{
		get {
			return Vector3.Distance (
				m_transform.position, 
				Singletons.GetPlayer ().transform.position
			);
		}
	}

	[SerializeField]
	private State state;
	private enum State
	{
		Waiting,
		Threatening,
		Chasing,
		Broked
	}

	void Start () {
		StartCoroutine (StateMachine());
	}

	IEnumerator StateMachine () {
		switch (state) {
		case State.Chasing:
			Chasing ();
			CheckState ();
			break;
		case State.Threatening:
			yield return StartCoroutine (Threatening ());
			CheckState ();
			break;
		case State.Waiting:
			Waiting ();
			CheckState ();
			break;
		case State.Broked:
			Broke ();
			break;
		}
	}

	void Chasing ()
	{
		Vector3 targetPosition = Singletons.GetPlayer().transform.position;
		Vector3 actualPosition = m_transform.position;

		Vector3 targetDirection = (targetPosition - actualPosition).normalized;

		if (Vector3.Dot (m_transform.forward, targetDirection) < 0.9) {
			TurnSide sideTurn = TurnSide.Left;
			if (Vector3.Dot (m_transform.right, targetDirection) > 0)
				sideTurn = TurnSide.Right;

			Turn (sideTurn, 1f);
		} else {
			Turn (TurnSide.Center, 0f);
		}

		Acellerate (1f);
	}

	IEnumerator Threatening ()
	{
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

	float timeTurning = 0f;
	TurnSide sideToTurn = TurnSide.Left;
	void Waiting ()
	{
		float timeTurning = 0f;
		TurnSide sideToTurn = TurnSide.Left;
		Acellerate (0.2f);

		if (timeTurning >= 3f) {
			timeTurning = 0f;
			sideToTurn = sideToTurn == TurnSide.Left ? TurnSide.Right : TurnSide.Left;
		}

		Turn (sideToTurn, 0.7f);

		timeTurning += Time.deltaTime;
	}

	private void Broke ()
	{

	}

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
		default:
			break;
		}
	}
}
