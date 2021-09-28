using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RecipeSystem : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject ingredientPrefab;
    [SerializeField] GameObject recipeIconPrefab;
    [SerializeField] GameObject stickPrefab;

    [Header("Others")]
    [SerializeField] Transform spawnLoc;
    [SerializeField] Recipe[] recipes;

    [HideInInspector] public List<GameObject> currentRecipe = new List<GameObject>();

    bool clearing;

    GameManager gameManager;
    Order order;

    void Awake() 
    {
        gameManager = GetComponent<GameManager>();
        order = GetComponent<Order>();
    }

    void Start()
    {
        selectRecipe();
    }

    void Update() 
    {
        if(gameManager.readyforMove && currentRecipe.Count == 0 && !clearing)
        {
            StartCoroutine(clearRecipe());
            clearing = true;
        }
    }

    void selectRecipe()
    {
        Recipe recipe = recipes[Random.Range(0,recipes.Length)];

        GameObject recipeIcon = Instantiate(recipeIconPrefab,spawnLoc);
        recipeIcon.GetComponent<Image>().sprite = recipe.recipeIcon;
        recipeIcon.transform.GetChild(0).GetComponent<Image>().sprite = recipe.recipeIcon;
        Instantiate(stickPrefab,spawnLoc);

        for(int i = 0; i < recipe.Ingredients.Length; i++)
        {
            GameObject ingredient = Instantiate(ingredientPrefab,spawnLoc);
            ingredient.GetComponent<Image>().sprite = recipe.ingredientsIcons[i];
            ingredient.transform.GetChild(0).GetComponent<Text>().text = recipe.Ingredients[i];
            ingredient.name = recipe.Ingredients[i];

            currentRecipe.Add(ingredient);
        }   

        clearing = false;
    }

    IEnumerator clearRecipe()
    {
        order.orderCompleted();

        yield return new WaitForSeconds(0.5f);
        List<GameObject> recipeList = new List<GameObject>();
        for(int i = 0; i < spawnLoc.transform.childCount; i++)
        {
            Destroy(spawnLoc.transform.GetChild(i).gameObject);
        }
        yield return new WaitForSeconds(0.5f);
        selectRecipe();
    }
}
