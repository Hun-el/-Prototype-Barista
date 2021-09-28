using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    List<GameObject> orderList = new List<GameObject>();
    [SerializeField] GameObject orderPrefab;
    [SerializeField] Transform spawnLoc;

    int order;

    GameManager gameManager;

    void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();
        order = gameManager.order;
    }

    void Start() 
    {
        for(int i = 0; i < order; i++)
        {
            GameObject g = Instantiate(orderPrefab,spawnLoc);
            orderList.Add(g);
        }
    }

    public void orderCompleted()
    {
        order--;
        orderList[0].transform.GetChild(0).GetComponent<Image>().enabled = true;
        orderList.Remove(orderList[0]);
        
        if(order <= 0)
        {
            gameManager.levelCompleted();
        }
    }
}
