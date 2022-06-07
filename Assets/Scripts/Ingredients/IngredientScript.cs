using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// General case ingredient script
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
