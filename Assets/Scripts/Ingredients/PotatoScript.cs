using System.Collections;
using System.Collections.Generic;
using Ingredients;
using UnityEngine;

// Specifications for a potato. Currently, it can just be boiled once. 
// States and methods are from the IngredientBaseScript
public class PotatoScript : IngredientScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Boil the potato, boiling more than once will destroy it.
    public override void Boil(){
        if (raw){
            raw = false;
            boiled = true;
        }
        else{
            destroyed = true;
        }
    }

    public override void Mash(){
        if (boiled){
            mashed = true;
        }
        else{
            destroyed = true;
        }
    }
}
