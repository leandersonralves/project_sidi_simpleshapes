using UnityEngine;

/// <summary>
/// Direção do Carro.
/// </summary>
public enum TurnSide
{
	Center,
	Left,
	Right
}

/// <summary>
/// Classe base para Objetos que se comportem como um Carro.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class Car : MonoBehaviour
{
    /// <summary>
    /// Referencia para o Particle Sytem de fumação, executado quando é quebrado.
    /// </summary>
    [SerializeField]
    private ParticleSystem smoke;

    /// <summary>
    /// Velocidade máxima.
    /// </summary>
	[SerializeField]
	private float maxSpeed = 20f;

    /// <summary>
    /// Torque máximo.
    /// </summary>
	[SerializeField]
	private float maxTorque = 20f;

    /// <summary>
    /// Torque para o freio.
    /// </summary>
	[SerializeField]
	private float brakeTorque = 150f;

    /// <summary>
    /// Referência para os WheelsCollider que viram.
    /// </summary>
	[SerializeField]
	private WheelCollider[] turnableWheels;

    /// <summary>
    /// Máximo ângulo que as rodas viram.
    /// </summary>
	[SerializeField]
	private float maxAngleWheel = 35f;

    /// <summary>
    /// Referência para os WheelColliders tracionam.
    /// </summary>
	[SerializeField]
	private WheelCollider[] wheelsWithTraction;

    /// <summary>
    /// Centro de massa do Rigidbody, quando mais baixo, mais estável e vice-versa.
    /// </summary>
    [SerializeField]
    private Vector3 centerOfMass = new Vector3(0f, -1f, 0f);

    /// <summary>
    /// Referência para o rigidbody do veículo, fins de otimização.
    /// </summary>
    private Rigidbody m_rigidbody;

    /// <summary>
    /// Cache do próprio Transform, fins de otimização.
    /// </summary>
    protected Transform m_transform;

    /// <summary>
    /// "Saúde" do veículo.
    /// </summary>
    protected float heath = 100f;

    /// <summary>
    /// O quão fraco é o carro para batidas.
    /// Quanto maior o valor, mais fraco e mais rápido quebra.
    /// </summary>
    [SerializeField]
    private float weakness = 1f;


    void Awake ()
	{
		m_rigidbody = GetComponent<Rigidbody> ();
        m_rigidbody.centerOfMass = centerOfMass;
        
        m_transform = transform;

        //Checando se o carro possui rodas que viram.
		bool wheelsOk = true;
		if (turnableWheels == null || turnableWheels.Length < 1) {
			Debug.LogError ("Not found turnables Wheels in Car (" + transform.root.name + ")");
			wheelsOk = false;
		}

        //Checando se o carro possui rodas com tração.
		if (wheelsWithTraction == null || wheelsWithTraction.Length < 1) {
			Debug.LogError ("Not found Wheels with Traction in Car (" + transform.root.name + ")");
			wheelsOk = false;
		}

        //Caso não um dos tipos, será autodestruído.
		if (!wheelsOk)
			Destroy (this);
	}

    /// <summary>
    /// Função para acelerar o veículo.
    /// </summary>
    /// <param name="valueNormalized">Quantidade de aceleração. Valor entre de [0-1], 0 = nula e 1 = máxima.</param>
	public void Acellerate (float valueNormalized)
	{
		float torque = Mathf.Clamp (valueNormalized,-1f, 1f) * maxTorque;
		for (int i = 0; i < wheelsWithTraction.Length; i++) {
			wheelsWithTraction [i].motorTorque = torque;

			if (wheelsWithTraction [i].brakeTorque != 0)
				wheelsWithTraction [i].brakeTorque = 0;
		}

		if(m_rigidbody.velocity.magnitude > maxSpeed)
		{
			m_rigidbody.velocity = m_rigidbody.velocity.normalized * maxSpeed;
		}
	}

    /// <summary>
    /// Freia o veículo.
    /// </summary>
	public void Brake ()
	{
		for (int i = 0; i < wheelsWithTraction.Length; i++) {
			wheelsWithTraction [i].motorTorque = 0f;
			wheelsWithTraction [i].brakeTorque = brakeTorque;
		}
	}

    /// <summary>
    /// Esterce o veículo para a direita ou esquerda.
    /// </summary>
    /// <param name="turnSide">Lado para estercer.</param>
    /// <param name="valueNormalized">O quanto será estercido as rodas, Valor entre de [-1~1], 0 = nula e 1 = máxima.</param>
	public void Turn (float valueNormalized)
	{
        //Clamp do valor para que o veículo não vire além do limite.
		valueNormalized = Mathf.Clamp(Mathf.Abs (valueNormalized), -1f, 1f);

        float angle = maxAngleWheel * valueNormalized;

		valueNormalized = Mathf.Clamp01 (valueNormalized);
		for (int i = 0; i < turnableWheels.Length; i++) {
			turnableWheels [i].steerAngle = angle;
		}
	}

    /// <summary>
    /// Evento para colisão do carro, se colidir demais quebrará.
    /// </summary>
    /// <param name="colliderHitted"></param>
    public void OnCollisionEnter (Collision colliderHitted)
    {
        if(colliderHitted.collider.tag == "Wall" || colliderHitted.collider.tag == "Player" || colliderHitted.collider.tag == "Enemy")
        {
            heath -= weakness;
            if(weakness <= 0f)
            {
                Broke();
            }
        }
    }

    //Quebra o veículo.
    public void Broke ()
    {
        Acellerate(0f);
        Brake();
        smoke.Play();
    }
}
