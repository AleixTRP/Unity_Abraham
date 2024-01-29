using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ingredient", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public List<Ingredients> ingredients;
    public Ingredients result;


}
