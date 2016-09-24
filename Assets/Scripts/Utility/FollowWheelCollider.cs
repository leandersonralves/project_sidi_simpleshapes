using UnityEngine;

//Classe utilitária para que o transform (Mesh da Rodas) tenham a mesma posição e rotação dos WheelColliders.
public class FollowWheelCollider : MonoBehaviour {
    /// <summary>
    /// Referência para o WheelCollider que deverá ser seguido.
    /// </summary>
	[SerializeField]
	private WheelCollider wheelFollow;

    /// <summary>
    /// Cache do próprio Transform, fins de otimização.
    /// </summary>
    private Transform m_transform;
    
	void Awake () {
		if (!wheelFollow) {
			Debug.Log ("Not Found WheelCollider in wheel " + name + " from Car " + transform.root.name);
			Destroy (this);
		}

		m_transform = transform;
	}

	void Update () {
		Vector3 pos = Vector3.zero;
		Quaternion rotation = Quaternion.identity;

        //Consulta a rotação e posição atual do WheelCollider e aplica neste transform.
		wheelFollow.GetWorldPose (out pos, out rotation);

		m_transform.rotation = rotation;
		m_transform.position = pos;
	}
}
