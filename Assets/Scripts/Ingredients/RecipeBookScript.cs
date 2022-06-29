using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Ingredients;

[CreateAssetMenu(fileName = "New Recipe Book", menuName = "Recipe Book", order = 52)]
public class RecipeBookScript : ScriptableObject
{
  public GenericDictionary<EFood, GenericDictionary<EPrepMethod, IngredientScript>> recipes;
  //public GenericDictionary<IngredientScript, List<EFood>> compoundRecipes;

  public List<IngredientScript> compoundRecipes;

  [SerializeField] IngredientScript poo;


  // private GameManager _gameManager;


  public IngredientScript GetRecipe(EFood foodName, EPrepMethod prepMethod)
  {
    if (recipes.ContainsKey(foodName) && recipes[foodName].ContainsKey(prepMethod))
    {
      return recipes[foodName][prepMethod];
    }
    else
    {
      return poo;
    }
  }

  public IngredientScript GetCompoundRecipe(List<IngredientScript> ingredients)
  {
    foreach (var recipe in compoundRecipes)
    {
      IEnumerable<IngredientScript> intersectsOptional = ingredients.Intersect(recipe.optionalIngredients);
      IEnumerable<IngredientScript> intersects = ingredients.Intersect(recipe.requiredIngredients);

      if (intersects.Count() == recipe.requiredIngredients.Count)
      {
        recipe.scoreModifier = 1;
        for (int i = 0; i < ingredients.Count; i++)
        {
          if (!recipe.optionalIngredients.Contains(ingredients[i]) &&
              !recipe.requiredIngredients.Contains(ingredients[i]))
          {
            break;
          }

          // if went through the whole list without breaking, return ingredient script
          if (i == ingredients.Count - 1)
          {
            //we can set the score in here, but it does modify it in the editor permanently so be careful
            //recipe.score = 10;
            recipe.scoreModifier += 0.1f * intersectsOptional.Count();
            return recipe;
          }
        }
      }
    }

    // if it couldn't find the recipe, return poo
    return poo;
  }
}
