using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSpawner : MonoBehaviour
{
    public PropPrefab[] dropPrefabs;//储存不同的道具预制体

    public void DropItems()
    {
        foreach(var propPrefab in dropPrefabs)
        {
            if (Random.Range(0f, 100f) <= propPrefab.dropPrecentage)
            {
                Instantiate(propPrefab.prefab,transform.position,Quaternion.identity);
            }
        }
        
    }

    [System.Serializable]
   public class PropPrefab
    {
        public GameObject prefab;
        [Range(0f, 100f)] public float dropPrecentage;
    }
}
