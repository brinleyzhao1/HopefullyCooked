using System.Collections;
using System.Collections.Generic;
using Ingredients;
using UnityEngine;

// Used to establish ingredients. Any gameobject with an ingredient component
// becomes an ingredient. An ingredient script must be specified, as this component
// uses the script to distinguish between ingredients. For example, a PotatoScript
// would convert a general ingredient into a potato.
//
// All ingredients are interactable by the player. Currently this just means
// they get picked up.
public class Ingredient : Interactable
{
    // Allows us to save data and load data from an ingredient
    // that is being prepared (i.e. getting a boiled potato
    // after a raw potato is placed into a pot)
    public IngredientScript ingredientScript;
    private GameObject model;

    // Start is called before the first frame update
    void Start(){
        //From Interactable class, sets 1. player object and 2. collider to trigger
        Initialize();
        //if (ingredientScript){
        //    SetIngredientScript(ingredientScript);
        //}
    }

    // Used to load data into a new ingredient, usually by prepstations that
    // have just prepared some ingredients. Also spawns food model.
    public void SetIngredientScript(IngredientScript newScript){
        ingredientScript = newScript;
        if (model){
            Destroy(model);
        }
        model = Instantiate(ingredientScript.foodModel);
        //SetNewModel();

    }

    private void SetNewModel(){
        // Give the new model a mesh collider with the appropriate mesh
        MeshCollider mc = model.AddComponent<MeshCollider>() as MeshCollider;
        mc.convex = true;
        MeshFilter modelMeshFilter = ingredientScript.foodModel.GetComponent<MeshFilter>();
        if (!modelMeshFilter)
            modelMeshFilter = ingredientScript.foodModel.GetComponentInChildren<MeshFilter>();
        mc.sharedMesh = modelMeshFilter.sharedMesh;

        // Set the model's position
        model.transform.SetParent(this.gameObject.transform);
        //model.transform.localPosition = ingredientScript.foodPosition;
        //model.transform.localScale = ingredientScript.foodScale;
    }

    // Specifies interaction with the player. Just gets picked up for now.
    public override void Interact(){

        player.PickUpOrDrop(gameObject);
    }

}
