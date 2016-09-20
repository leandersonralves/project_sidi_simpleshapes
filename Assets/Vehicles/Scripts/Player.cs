using UnityEngine;
using System.Collections;

public class Player : Car {
	
	void Update () {
		float accelerate = Input.GetAxis("Vertical");
		if (!Mathf.Approximately (accelerate, 0f))
			Acellerate (accelerate);

		if (Input.GetKey (KeyCode.Space))
			Brake ();

		float turn = Input.GetAxis("Horizontal");

		if (Mathf.Approximately (turn, 0))
			Turn (TurnSide.Center, 0);
		else {
			if(turn > 0)
				Turn (TurnSide.Right, turn);
			else
				Turn (TurnSide.Left, turn);
		}
	}
}
