using UnityEngine;

public static class Singletons
{
	private static GameObject player;
	public static GameObject GetPlayer ()
	{
		if (!player) {
			if (GameObject.FindWithTag ("Player") == null) {
				Debug.LogError ("Not Found GameObject with Tag (Player)");
				return null;
			}

			player = GameObject.FindWithTag ("Player");
		}

		return player;
	}
}

