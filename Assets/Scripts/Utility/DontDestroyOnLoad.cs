using UnityEngine;
using System.Collections;

/// <summary>
/// Componente que define o gameObject para não ser destruído, quando outra cena é carregada.
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour {
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
