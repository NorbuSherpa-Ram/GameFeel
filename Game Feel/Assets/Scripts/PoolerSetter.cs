using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PoolObject
{
    public GameObject objectToPool;
    public int totalAmount;
    public Transform parent;

}
public class PoolerSetter : MonoBehaviour
{
    public List<PoolObject> poolObejct;
    [SerializeField ]private ObjectPooler pooler;

    private void Start()
    {
        foreach (PoolObject item in poolObejct)
        {
            pooler.CreatePool(item.parent, item.objectToPool, item.totalAmount);
        }
    }
}
