using UnityEngine;

/// <summary>
/// Classe com função de contralar um Tranform, fazendo-o seguir outro.
/// </summary>
public class CameraFollower : MonoBehaviour {

    /// <summary>
    /// Se True, o transform seguirá o alvo transladando.
    /// </summary>
    [SerializeField]
    private bool followPosition;

    /// <summary>
    /// Se true, o tranform apontará a direção forward (Z) para o alvo.
    /// </summary>
    [SerializeField]
    private bool followDirection;

    /// <summary>
    /// Referência para o tranform do alvo.
    /// Se nulo de início, este componente será removido.
    /// </summary>
    [SerializeField]
    private Transform target;

    /// <summary>
    /// O Quanto de deslocamento da direção, vetor na coordenada local do alvo.
    /// </summary>
    [SerializeField]
    private Vector3 offSetDirection;

    /// <summary>
    /// O quanto deslocado o gameObject ficará em relação ao alvo, vetor na coordenada local do alvo.
    /// </summary>
    [SerializeField]
    private Vector3 offSetPosition;

    /// <summary>
    /// Cache do próprio Transform, fins de otimização.
    /// </summary>
    private Transform m_transform;

    /// <summary>
    /// Qual a altura que o Tranform ficará em relação ao alvo.
    /// </summary>
    [SerializeField]
    private float height = 5f;

	void Awake () {
	    if(!target)
        {
            Debug.Log("Not found target in scrip CameraFollower in GameObject " + name);
            Destroy(this);
        }

        m_transform = transform;
    }
	
	void Update () {
        if(followPosition)
        {
            Vector3 targetPosition = target.TransformPoint(offSetPosition);
            targetPosition.y = height;
            m_transform.position = targetPosition;
        }

        if (followDirection)
        {
            m_transform.LookAt (target);
        }
    }
}
