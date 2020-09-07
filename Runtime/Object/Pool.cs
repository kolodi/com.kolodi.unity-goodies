using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{

    private Transform _pool;
    private T _template;

    public Pool(T template, Transform parent = null)
    {
        _template = template;
        var go = new GameObject($"[pool of {typeof(T).FullName}]");
        var root = go.transform;
        root.SetParent(parent, false);
        go.SetActive(false);
        var templateParent = new GameObject("Templates").transform;
        templateParent.SetParent(root, false);
        template.transform.SetParent(templateParent, false);
        _pool = new GameObject("pool").transform;
        _pool.SetParent(root);
    }

    public T GetInstance(Transform newParent = null, bool activateGameObject = true)
    {
        if (_pool.childCount > 0)
        {
            var objInPool = _pool.GetChild(0);
            objInPool.SetParent(newParent);
            objInPool.gameObject.SetActive(activateGameObject);
            return objInPool.GetComponent<T>();
        }

        Transform parent = newParent != null ? newParent : _pool;

        T instance = Object.Instantiate<T>(_template, parent);
        instance.gameObject.SetActive(activateGameObject);
        return instance;
    }

    public void ReleaseInstance(T obj, bool disactivateGameObject = true)
    {
        obj.transform.SetParent(_pool);
        obj.gameObject.SetActive(!disactivateGameObject);
    }

    public void ReleaseMany(IEnumerable<T> list, bool disactivateGameObject = true)
    {
        foreach (var i in list)
        {
            ReleaseInstance(i, disactivateGameObject);
        }
    }
}
