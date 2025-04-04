using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _poolObject;

    [SerializeField]
    private int _poolSize = 10;

    private List<GameObject> _pool = new List<GameObject>();

    private int _index;



    // Game Loop Methods---------------------------------------------------------------------------

    private void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            CreatePoolItem();
        }
    }

    // Member Methods------------------------------------------------------------------------------

    private GameObject CreatePoolItem()
    {
        GameObject createdObject = Instantiate(_poolObject, transform);
        createdObject.SetActive(false);

        _pool.Add(createdObject);

        return createdObject;
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[_index].activeInHierarchy)
            {
                return _pool[_index];
            }

            _index++;

            if (_index >= _pool.Count)
            {
                _index = 0;
            }
        }

        return CreatePoolItem();
    }
}
