using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;

    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    private GameObject pool;
    public static ObjectPool Instance 
    {
        get
        {
          if(instance == null) 
            {
               instance = new ObjectPool();
            }
          return instance;
        }
    }

    private ObjectPool()
    {
        pool = new GameObject("objectPoll");
        GameObject.DontDestroyOnLoad(pool);
    }

    //public GameObject GetObject(GameObject prefab) 
    // {
    //     GameObject _object;
    //     if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
    //    {
    //        _object = GameObject.Instantiate(prefab);
    //        PushObject(_object);
    //         if (pool == null)
    //             pool = new GameObject("ObjectPool");
    //             GameObject child = GameObject.Find(prefab.name);
    //             if (!child) 
    //             {
    //                 child = new GameObject(prefab.name );
    //                 child.transform.SetParent(pool.transform); 

    //             }
    //             _object.transform.SetParent(child.transform);
    //         }
    //         _object = objectPool[prefab.name].Dequeue();
    //         _object.SetActive(true);
    //         return _object;
    // }

    public GameObject GetObject(GameObject prefab)
    {
        GameObject _object;
        string prefabName = prefab.name.Replace("(Clone)", string.Empty);

        // 检查对象池中是否有可用的对象
        if (objectPool.ContainsKey(prefabName) && objectPool[prefabName].Count > 0)
        {
            _object = objectPool[prefabName].Dequeue();
            // 检查对象是否已经被销毁
            if (_object == null)
            {
                // 如果对象已被销毁，递归调用获取新对象
                return GetObject(prefab);
            }
            _object.SetActive(true);
            return _object;
        }

        // 对象池中没有可用对象，创建新对象
        _object = GameObject.Instantiate(prefab);

        // 设置父对象
        if (pool == null)
        {
            pool = new GameObject("ObjectPool");
            // 确保对象池在场景切换时不销毁
            GameObject.DontDestroyOnLoad(pool);
        }

        // 查找或创建类别父对象
        string categoryName = prefabName + "Pool";
        GameObject child = GameObject.Find(categoryName);
        if (child == null)
        {
            child = new GameObject(categoryName);
            child.transform.SetParent(pool.transform);
        }

        _object.transform.SetParent(child.transform);
        _object.name = prefabName; // 移除(Clone)后缀

        return _object;
    }

    //public void PushObject(GameObject prefab) 
    //{
    //  string _name = prefab.name.Replace("(Clone)",string.Empty);
    //    if (!objectPool.ContainsKey(_name)) 
    //        objectPool.Add(_name,new Queue<GameObject>());
    //    objectPool[_name].Enqueue(prefab);
    //    prefab.SetActive(false);


    //}
    public void PushObject(GameObject obj)
    {
        if (obj == null) return;

        string _name = obj.name.Replace("(Clone)", string.Empty);

        if (!objectPool.ContainsKey(_name))
        {
            objectPool.Add(_name, new Queue<GameObject>());
        }

        obj.SetActive(false);
        objectPool[_name].Enqueue(obj);
    }
}
