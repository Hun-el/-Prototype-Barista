using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Barista : MonoBehaviour
{
    Animator animator;
    float speed;

    GameManager gameManager;
    RecipeSystem recipeSystem;

    void Awake() 
    {
        animator = GetComponent<Animator>();
        recipeSystem = FindObjectOfType<RecipeSystem>();
        gameManager = FindObjectOfType<GameManager>();

        speed = gameManager.baristaSpeed;
    }

    void FixedUpdate() 
    {
        if(gameManager.readyforMove && !gameManager.stopMove){ Moving(); animator.SetBool("Run",true); }
        else{ animator.SetBool("Run",false); }
    }

    public void Moving()
    {
        float NewX = 0;
        float touchXDelta = 0;

        #if UNITY_ANDROID && !UNITY_EDITOR
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchXDelta = 60*Input.GetTouch(0).deltaPosition.x / Screen.width;
            if(touchXDelta < 0)
            {
                Turn(-1);
            }
            else if(touchXDelta != 0)
            {
                Turn(1);
            }
            else
            {
                Turn(0);
            }
        }
        else
        {
            Turn(0);
        }
        #endif

        #if UNITY_STANDALONE || UNITY_EDITOR
        if(Input.GetMouseButton(0))
        {
            touchXDelta = 20*Input.GetAxis("Mouse X");
            if(touchXDelta < 0)
            {
                Turn(-1);
            }
            else if(touchXDelta != 0)
            {
                Turn(1);
            }
            else
            {
                Turn(0);
            }
        }
        else
        {
            Turn(0);
        }
        #endif

        NewX = transform.position.x + touchXDelta * 1 * Time.deltaTime;
        NewX = Mathf.Clamp(NewX ,-2.15f,2.15f);

        Vector3 newPos = new Vector3(NewX,transform.position.y,transform.position.z + speed * Time.deltaTime);
        transform.position = newPos;
    }

    public void Turn(int right)
    {
        transform.DOLocalRotate(new Vector3(0,45 * right,0) , 0.25f);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            other.transform.DOScale(new Vector3(0,0,0) , 0.5f).OnComplete(()=>{ Destroy(other); });
            string stringToCheck = other.tag;
            List<GameObject> currentRecipe = recipeSystem.currentRecipe;
            foreach (GameObject x in currentRecipe)
            {
                if (x.name == stringToCheck)
                {
                    animator.SetTrigger("Jump");
                    x.transform.GetChild(1).GetComponent<Image>().transform.DOScale(new Vector3(1,1,1),0.75f).SetEase(Ease.OutElastic);
                    recipeSystem.currentRecipe.Remove(x);
                    break;
                }
            }
        }
    }
}
