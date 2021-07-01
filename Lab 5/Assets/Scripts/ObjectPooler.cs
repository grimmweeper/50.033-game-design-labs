using System.Collections.Generic;
using UnityEngine;


public enum ObjectType{
    gombaEnemy = 0,
    blueEnemy = 1
}

[System.Serializable]
public class ObjectPoolItem
{
   public int amount;
   public GameObject prefab;
   public bool expandPool;
   public ObjectType type;
}

public class ExistingPoolItem
{
    public GameObject gameObject;
    public ObjectType type;

    public ExistingPoolItem(GameObject gameObject, ObjectType type){
        // reference input 
        this.gameObject = gameObject;
        this.type = type;
    }
}


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool; // types of different object to pool 
    public List<ExistingPoolItem> pooledObjects; // a list of all objects in the pool, of all types
    void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<ExistingPoolItem>();
        Debug.Log("ObjectPooler Awake");

        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                // this 'pickup' a local variable, but Unity will not remove it since it exists in the scene
                GameObject pickup = (GameObject)Instantiate(item.prefab);
                pickup.SetActive(false);
                pickup.transform.parent = this.transform;
                ExistingPoolItem e = new ExistingPoolItem(pickup, item.type);
                pooledObjects.Add(e);
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this method can be called by other scripts to get pooled object by its type defined as enum earlier, or simly as tag as you like
    // there's no "return" object to pool method. Simply set it as unavailable
    public GameObject GetPooledObject(ObjectType type)
    {
        // return inactive pooled object if it matches the type 
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].type == type)
            {
                return pooledObjects[i].gameObject;
            }
        }
        // this will be called no more active object is present, item to expand pool if required 
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.type == type)
            {
                if (item.expandPool)
                {
                    GameObject pickup = (GameObject)Instantiate(item.prefab);
                    pickup.SetActive(false);
                    pickup.transform.parent = this.transform;
                    pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
                    return pickup;
                }
            }
        }

        // we will return null IF and only IF the type doesn't match with what is defined in the itemsToPool. 
        return null;
    }

}
