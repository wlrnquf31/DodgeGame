using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    private Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        ObjectInit(25);
    }

    private void ObjectInit(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private GameObject CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab);

        newObj.gameObject.SetActive(false);

        newObj.transform.SetParent(transform);

        return newObj;
    }

    public GameObject GetObject()
    {
        GameObject obj;
        while (true)
        {
            if (instance.poolingObjectQueue.Count > 0)
            {
                obj = instance.poolingObjectQueue.Dequeue();
                if (obj.activeInHierarchy || obj == null)
                {
                    continue;
                }
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = instance.CreateNewObject();
                obj.gameObject.SetActive(true);
            }

            break;
        }
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        instance.poolingObjectQueue.Enqueue(obj);
    }
}
