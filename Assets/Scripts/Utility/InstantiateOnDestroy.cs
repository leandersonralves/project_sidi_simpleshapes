using UnityEngine;
using System.Collections;

public class InstantiateOnDestroy : MonoBehaviour {

	[SerializeField]
	protected GameObject[] prefabs;

	protected Transform m_transform;

	void Awake () {
		if (prefabs == null || prefabs.Length < 1) {
			Debug.LogWarning ("Not found particle in GameObject " + name + " to be instantiated.");
			Destroy (this);
		}

		m_transform = transform;
	}
	
	void OnDestroy () {
		for (int i = 0; i < prefabs.Length; i++) {
			Instantiate(prefabs[i], m_transform.position, Quaternion.identity);
		}
	}
}
