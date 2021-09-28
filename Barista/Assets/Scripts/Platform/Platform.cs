using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    GameObject[] foodPrefabs;
    GameManager gameManager;

    int minFood;
    int maxFood;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        minFood = gameManager.minFood;
        maxFood = gameManager.maxFood;

        foodPrefabs = gameManager.foodPrefabs;
        if(transform.parent.GetChild(0) != this.transform){ setFood(Random.Range(minFood,maxFood + 1)); }
    }

    public void moved()
    {   
        foreach (Transform child in transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
        setFood(Random.Range(minFood,maxFood + 1));

        Transform t = this.transform;
        t.position = new Vector3(t.position.x,t.position.y,t.position.z+(transform.parent.childCount * t.GetComponent<Renderer>().bounds.size.z));
        this.transform.SetAsLastSibling();
    }

    public void setFood(int count = 1)
    {
        int limit = 10;
        for(int i = 0; i < count; i++)
        {
            bool validPosition = true;

            Collider collider = GetComponent<Collider>();
            float maxX = collider.bounds.max.x;
            float minX = collider.bounds.min.x;

            float maxZ = collider.bounds.max.z;
            float minZ = collider.bounds.min.z;

            float randomX = Random.Range(minX,maxX);
            float randomZ = Random.Range(minZ,maxZ);

            Vector3 spawnPoint = new Vector3(randomX , collider.bounds.max.y , randomZ);
            Collider[] colliders = Physics.OverlapSphere(spawnPoint, 3f);
            foreach(Collider col in colliders)
            {
                if(col.gameObject.layer == LayerMask.NameToLayer("Food"))
                {
                    validPosition = false;

                    limit--;
                    if(limit > 0){ i--;}
                }
            }

            if(validPosition)
            {
                GameObject foodClone = Instantiate(foodPrefabs[Random.Range(0,foodPrefabs.Length)],new Vector3(randomX,collider.bounds.max.y*3,randomZ),Quaternion.identity);
                foodClone.transform.SetParent(transform);
            }
        }
    }
}
