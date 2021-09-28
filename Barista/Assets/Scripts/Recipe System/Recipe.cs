using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe" , menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public Sprite recipeIcon;
    public Sprite[] ingredientsIcons;
    public string[] Ingredients;
}
