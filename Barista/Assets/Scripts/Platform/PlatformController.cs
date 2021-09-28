using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    GameObject currentPlatform;

    GameManager gameManager;

    void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        currentPlatform = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if(!IsVisibleFrom(currentPlatform.GetComponent<Renderer>(),Camera.main) && gameManager.readyforMove)
        {
            currentPlatform.GetComponent<Platform>().moved();
            changeCurrentPlatform();
        }
    }

    void changeCurrentPlatform()
    {
        currentPlatform = transform.GetChild(0).gameObject;
    }

    bool IsVisibleFrom(Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}
