using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject go = new GameObject(typeof(T).Name);
				instance = go.AddComponent<T>();
				DontDestroyOnLoad(go);
			}

			return instance;
		}
	}

	public virtual void Awake()
	{
		this.transform.parent = null;

		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}

		instance = this as T;
		DontDestroyOnLoad(gameObject);
	}
}
