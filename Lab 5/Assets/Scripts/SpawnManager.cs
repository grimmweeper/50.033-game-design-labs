using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    float groundDistance = -3.8f;
    void Start()
    {
        for (int j = 0; j < 5; j++)
            spawnFromPooler(ObjectType.blueEnemy);

    }


    void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);

        if (item != null)
        {
            //set position
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), groundDistance + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnNewEnemy()
    {

        ObjectType i = Random.Range(0, 2) == 0 ? ObjectType.gombaEnemy : ObjectType.blueEnemy;
        spawnFromPooler(i);

    }

}
