using UnityEngine;

// General case ingredient script
namespace Ingredients
{
  [CreateAssetMenu(fileName = "New Ingredient Data", menuName = "Ingredient Data", order = 51)]
  public class IngredientScript : IngredientBaseScript
  {
    public override void Boil(){

    }

    public override void Mash(){

    }

    public override void Prepare(EPrepMethod currentPreparation)
    {
      SequenceOfPreparations.Add(currentPreparation);
    }
  }
}
