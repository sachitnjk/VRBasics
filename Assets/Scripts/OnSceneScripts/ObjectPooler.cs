using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler instance;

	[System.Serializable]
	public class PoolInfo
	{
		public GameObject prefab;
		public int countToPool = 20;
		public Transform parentTransform;
	}

	[SerializeField] public List<PoolInfo> pools;

	private Dictionary<GameObject, List<GameObject>> pooledObjects;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		pooledObjects = new Dictionary<GameObject, List<GameObject>>();

		// Create pools for each specified prefab
		foreach (PoolInfo poolInfo in pools)
		{
			List<GameObject> objectPool = new List<GameObject>();
			for (int i = 0; i < poolInfo.countToPool; i++)
			{
				Transform parent = transform;
				if(poolInfo.parentTransform != null)
				{
					parent= poolInfo.parentTransform;
				}
				GameObject instantiatedObject = Instantiate(poolInfo.prefab, parent);
				instantiatedObject.SetActive(false);
				objectPool.Add(instantiatedObject);
			}
			pooledObjects.Add(poolInfo.prefab, objectPool);
		}
	}

	public int GetActiveObjectsCount(GameObject prefab)
	{
		int count = 0;
		if(pooledObjects.ContainsKey(prefab))
		{
			List<GameObject> objectPool = pooledObjects[prefab];
			foreach(var obj in objectPool) 
			{
				if (obj.activeSelf)
				{
					count++;
				}
			}
		}
		return count;
	}

	public GameObject GetPooledObject(GameObject prefab)
	{
		if (pooledObjects.ContainsKey(prefab))
		{
			List<GameObject> objectPool = pooledObjects[prefab];
			for (int i = 0; i < objectPool.Count; i++)
			{
				if (!objectPool[i].activeInHierarchy)
				{
					//if (i > 0 && objectPool[i - 1].activeInHierarchy)
					//{
					//	continue; // Skip this one, it was just disabled
					//}
					return objectPool[i];
				}
			}
		}

		return null;
	}
}
