using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject levelCompletedPrefab;
    public GameObject[] foodPrefabs;

    [Header("Platform Settings")]
    [Range(0,5)] public int minFood;
    [Range(1,5)] public int maxFood;

    [Header("Barista Settings")]
    [Range(5,15)] public int baristaSpeed;

    [Header("Order Settings")]
    [Range(1,5)] public int order;

    [HideInInspector] public bool readyforMove;
    [HideInInspector] public bool stopMove;

    public void Ready()
    {
        readyforMove = true;
    }

    public void levelCompleted()
    {
        Instantiate(levelCompletedPrefab);
        stopMove = true;

        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.5f);
        LoadingSystem loadingSystem = GetComponent<LoadingSystem>();
        loadingSystem.LoadLevel("Restart");
    }
}
