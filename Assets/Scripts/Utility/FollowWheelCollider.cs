using UnityEngine;
using System.Collections;

public class FollowWheelCollider : MonoBehaviour {

	[SerializeField]
	private WheelCollider wheelFollow;

	private Transform m_transform;

	private Vector3 initialPosition;

	void Awake () {
		if (!wheelFollow) {
			Debug.Log ("Not Found WheelCollider in wheel " + name + " from Car " + transform.root.name);
			Destroy (this);
		}

		m_transform = transform;
		initialPosition = m_transform.localPosition;
	}

	void Update () {
//		Vector3 m_eulerAngles = m_transform.localEulerAngles;
//		m_eulerAngles.x += 2 * Mathf.PI * wheelFollow.rpm / 60f;
//		m_eulerAngles.y = wheelFollow.steerAngle;
//
//		m_transform.localEulerAngles = m_eulerAngles;

		Vector3 pos = Vector3.zero;
		Quaternion rotation = Quaternion.identity;

		wheelFollow.GetWorldPose (out pos, out rotation);

		m_transform.rotation = rotation;
		m_transform.position = pos;
	}
}
