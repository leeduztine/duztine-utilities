using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    private Dictionary<string, RecyclableObject> prefabPool = new Dictionary<string, RecyclableObject>();

    private Dictionary<string, Queue<RecyclableObject>> instantiatedObjects =
        new Dictionary<string, Queue<RecyclableObject>>();

    private bool HasPrefabInPool(string prefabName)
    {
        return prefabPool.ContainsKey(prefabName);
    }

    private void AddToPool(RecyclableObject newObj)
    {
        prefabPool.Add(newObj.name, newObj);
    }

    public RecyclableObject SpawnObject(RecyclableObject prefab, Transform parent = null)
    {
        if (!HasPrefabInPool(prefab.name))
        {
            AddToPool(prefab);
        }

        var obj = GetNewObject(prefab.name);

        if (obj)
        {
            if (parent) obj.transform.SetParent(parent);
            obj.gameObject.SetActive(true);
            obj.transform.localPosition = Vector3.zero;
            obj.OnSpawn();
        }

        return obj;
    }

    public RecyclableObject SpawnObject(RecyclableObject prefab, Vector3 position)
    {
        if (!HasPrefabInPool(prefab.name))
        {
            AddToPool(prefab);
        }

        var obj = GetNewObject(prefab.name);

        if (obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            obj.OnSpawn();
        }

        return obj;
    }

    public T SpawnObject<T>(RecyclableObject prefab, Transform parent = null)
    {
        if (!HasPrefabInPool(prefab.name))
        {
            AddToPool(prefab);
        }

        var obj = GetNewObject(prefab.name);

        if (obj)
        {
            if (parent) obj.transform.SetParent(parent);
            obj.gameObject.SetActive(true);
            obj.transform.localPosition = Vector3.zero;
            obj.OnSpawn();
        }

        return obj.GetComponent<T>();
    }

    public T SpawnObject<T>(RecyclableObject prefab, Vector3 position)
    {
        if (!HasPrefabInPool(prefab.name))
        {
            AddToPool(prefab);
        }

        var obj = GetNewObject(prefab.name);

        if (obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            obj.OnSpawn();
        }

        return obj.GetComponent<T>();
    }

    private RecyclableObject GetNewObject(string prefabName)
    {
        if (instantiatedObjects.ContainsKey(prefabName))
        {
            if (instantiatedObjects[prefabName].Count > 0)
            {
                var obj = instantiatedObjects[prefabName].Dequeue();
                return obj;
            }
        }

        var prefab = GetPrefab(prefabName);

        if (prefab)
        {
            var obj = Instantiate(prefab);
            obj.originalName = prefabName;
            return obj;
        }

        return null;
    }

    public void DestroyObject(RecyclableObject obj)
    {
        var objName = obj.originalName;
        if (!instantiatedObjects.ContainsKey(objName))
        {
            instantiatedObjects[objName] = new Queue<RecyclableObject>();
        }
        
        instantiatedObjects[objName].Enqueue(obj);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
    }

    private RecyclableObject GetPrefab(string prefabName)
    {
        return prefabPool.ContainsKey(prefabName) ? prefabPool[prefabName] : null;
    }
}
