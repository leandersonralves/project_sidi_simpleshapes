using UnityEngine;
using System.Collections;

public enum TurnSide
{
	Center,
	Left,
	Right
}

[RequireComponent(typeof(Rigidbody))]
public abstract class Car : MonoBehaviour
{
	[SerializeField]
	private float maxSpeed = 20f;
	[SerializeField]
	private float maxTorque = 20f;
	[SerializeField]
	private float brakeTorque = 150f;
	[SerializeField]
	private WheelCollider[] turnableWheels;
	[SerializeField]
	private float maxAngleWheel = 35f;
	[SerializeField]
	private WheelCollider[] wheelsWithTraction;

	private Rigidbody m_rigidbody;

	void Awake ()
	{
		m_rigidbody = GetComponent<Rigidbody> ();
	}

	protected void Acellerate (float valueNormalized)
	{
		float torque = Mathf.Clamp01 (valueNormalized) * maxTorque;
		for (int i = 0; i < wheelsWithTraction.Length; i++) {
			wheelsWithTraction [i].motorTorque = torque;
		}

		if(m_rigidbody.velocity.magnitude > maxSpeed)
		{
			m_rigidbody.velocity = m_rigidbody.velocity.normalized * maxSpeed;
		}
	}

	protected void Brake ()
	{
		for (int i = 0; i < turnableWheels.Length; i++) {
			turnableWheels [i].brakeTorque = brakeTorque;
		}
		for (int i = 0; i < wheelsWithTraction.Length; i++) {
			wheelsWithTraction [i].brakeTorque = brakeTorque;
		}
	}

	protected void Turn (TurnSide turnSide, float valueNormalized)
	{
		valueNormalized = Mathf.Clamp01(Mathf.Abs (valueNormalized));

		float angle = 0f;
		switch(turnSide)
		{
		case TurnSide.Left:
			angle = maxAngleWheel * valueNormalized * -1f;
			break;

		case TurnSide.Right:
			angle = maxAngleWheel * valueNormalized;
			break;
		}

		valueNormalized = Mathf.Clamp01 (valueNormalized);
		for (int i = 0; i < turnableWheels.Length; i++) {
			turnableWheels [i].steerAngle = angle;
		}
	}
}
