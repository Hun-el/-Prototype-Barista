using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeProgress : MonoBehaviour
{
    RecipeSystem recipeSystem;
    Image image;

    int ingredientCount;

    void Awake() 
    {
        recipeSystem = FindObjectOfType<RecipeSystem>();
        image = GetComponent<Image>();
    }

    void Start() 
    {
        ingredientCount = recipeSystem.currentRecipe.Count;
    }

    void Update() 
    {
        float a = ingredientCount - recipeSystem.currentRecipe.Count;
        float b = ingredientCount;
        image.fillAmount = a / b;
    }

}
