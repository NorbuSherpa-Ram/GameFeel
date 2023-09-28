using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    private void Awake()
    {
        instance = this;
    }


    private List<GameObject> pools;
    private Dictionary<GameObject, List<GameObject>> poolDictionary;

    [SerializeField] private Transform defaultParent;



    private void Start()
    {
        poolDictionary = new Dictionary<GameObject, List<GameObject>>();
    }
    public void CreatePool(Transform _parent , GameObject _objectToCreate , int _createAmount = 10)
    {

        defaultParent = _parent != null ? _parent : defaultParent;

        if (poolDictionary.ContainsKey(_objectToCreate) || _objectToCreate == null) return;

        pools = new List<GameObject>();
        for (int i = 0; i < _createAmount; i++)
        {
            GameObject newObject = Instantiate(_objectToCreate, transform.position, Quaternion.identity);
            newObject.SetActive(false);
            newObject.transform.SetParent(defaultParent);
            pools.Add(newObject);
        }
        poolDictionary.Add(_objectToCreate, pools);

    }

    public GameObject GetObjectFormPool(GameObject _poolObject,Vector2 _startPos )
    {
  
        if (poolDictionary.TryGetValue(_poolObject, out List<GameObject> value) )
        {
            foreach (GameObject item in value)
            {
                if (!item.activeInHierarchy)
                {
                    item.transform.position = _startPos;
                    item.SetActive(true);
                    return item;
                }
            }
        }
        Debug.Log(_poolObject + "Not Found ");
        return null;
    }

    public void ResetPool(GameObject _poolObject)
    {
        _poolObject.SetActive(false);
    }
}
