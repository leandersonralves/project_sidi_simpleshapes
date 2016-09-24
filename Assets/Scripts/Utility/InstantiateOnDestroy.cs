using UnityEngine;

//Instancia a array de Prefabs quando o que possui este componente é destruído.
public class InstantiateOnDestroy : MonoBehaviour {

    /// <summary>
    /// Prefabs a serem instanciados.
    /// </summary>
	[SerializeField]
	protected GameObject[] prefabs;
    
    /// <summary>
    /// Cache do próprio Transform, fins de otimização.
    /// </summary>
    protected Transform m_transform;

	void Awake () {
		if (prefabs == null || prefabs.Length < 1) {
			Debug.LogWarning ("Not found particle in GameObject " + name + " to be instantiated.");
			Destroy (this);
		}

		m_transform = transform;
	}
	
	void OnDestroy () {
        //Instanciando os prefabs na posição deste transform.
		for (int i = 0; i < prefabs.Length; i++) {
			Instantiate(prefabs[i], m_transform.position, Quaternion.identity);
		}
	}
}
