using System.Collections;
using System.Collections.Generic;
using Ingredients;
using Misc_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSubmit : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] GameObject winBanner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
      //station interacting with ingredient
      if (other.gameObject.GetComponent<Ingredient>())
      {
        // find submitted food
        IngredientScript submission = other.gameObject.GetComponent<Ingredient>().ingredientScript;
        if (submission.foodName == EFood.PotatoMashed) {
            winBanner.SetActive(true);
        }
      }
    }

}
