using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

// Specifications for a prep station. Currently does nothing.
// Some code from BoilStation should probably be moved here, it'll
// get more clear when there are more than one prep station.
namespace Stations
{
  public class PrepStation : Interactable
  {
    //public GameObject foodPrefab;
    [SerializeField] public List<IngredientScript> ingredients = new List<IngredientScript>();
    [SerializeField] EPrepMethod prepMethod;
    [SerializeField] bool canHaveMultipleIngredients;
    [SerializeField] RecipeBookScript recipeBook;

    [Header("Time")] [SerializeField] private int
      timeBeforeBurnt; //timeBeforeBurnt to travel half circle. include both undercooked and okay timeBeforeBurnt, undercooked is 1/3 of this timeBeforeBurnt

    [SerializeField] private Transform arrow;
    [SerializeField] private GameObject charcol;


    [Header("Misc")] [SerializeField] private AudioClip SFX;
    [SerializeField] private AudioClip putIngredientInSFX;
    private bool _inAction;
    private float timeInatoAction;
    private GameObject circleTimerUi;
    private AudioSource _audioSource;
    private GameManager _gameManager;

    // [SerializeField] Transform customPivot;


    void Start()
    {
      //From Interactable class, sets 1. player object and 2. collider to trigger
      Initialize();
      circleTimerUi = transform.Find("time circle").gameObject; //todo :( name reference
      circleTimerUi.SetActive(false);
      _audioSource = GetComponent<AudioSource>();
      _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
      // StartCoroutine(StartTimer(timeBeforeBurnt));
    }

    IngredientScript PrepIngredient(EFood inFoodName, EPrepMethod inPrepMethod)
    {
      IngredientScript preppedFood = recipeBook.GetRecipe(inFoodName, inPrepMethod);
      return preppedFood;
    }


    // Specifies what happens when an ingredient enters the pot
    // There will be more functionality here in the future. Currently a pot just boils an ingredients,
    // but there will be other cases, such as when multiple ingredients boil together to create a new
    // ingredient.
    void OnCollisionEnter(Collision other)
    {
      //station interacting with ingredient
      if (other.gameObject.GetComponent<Ingredient>())
      {
        _audioSource.PlayOneShot(putIngredientInSFX);

        // Boil the ingredient
        IngredientScript addedIngredient = other.gameObject.GetComponent<Ingredient>().ingredientScript;
        // Saves the ingredient's data. This is necessary because the ingredient gets destroyed,
        // and the player needs to pick it up later.
        IngredientScript preppedIngredient;
        if (prepMethod == EPrepMethod.Assemble)
        {
          preppedIngredient = addedIngredient;
        }
        else
        {
          preppedIngredient = PrepIngredient(addedIngredient.foodName, prepMethod);
        }

        ingredients.Add(preppedIngredient);
        // Destroy the gameobject.
        Destroy(other.gameObject);

        if (canHaveMultipleIngredients == false) //start process immediately
        {
          PickUpPreppedFood();
        }
        else
        {
          //todo: prompt player to precess space again to interact
        }
      }
    }


    public override void Interact()
    {
       if (timeBeforeBurnt == 0) //if is an instantaneous station, such as chopping
       {
         PickUpPreppedFood();
       }
      else if (_inAction == false && ingredients.Count > 0)
      {
        StartCoroutine(StartTimer());
      }
      else if (_inAction)
      {
        TryPickUpPreppedFood();
      }
    }

    private void TryPickUpPreppedFood()
    {
      if (timeInatoAction <= timeBeforeBurnt / 3f)
      {
        // print("undercooked");
        //print("time1: " + timeInatoAction);
        player.PickUpOrDrop(Instantiate(charcol));
        StartCoroutine(_gameManager.DisplayUndercookedText());
      }
      else if (timeInatoAction <= timeBeforeBurnt)
      {
        // print("success");
        //print("time2: " + timeInatoAction);
        PickUpPreppedFood(); //success
      }
      else if (timeInatoAction > timeBeforeBurnt)
      {
        // print("overcooked");
        //print("time3: " + timeInatoAction);
        player.PickUpOrDrop(Instantiate(charcol));
        StartCoroutine(_gameManager.DisplayOvercookedText());
      }


      circleTimerUi.SetActive(false);
      arrow.localEulerAngles = Vector3.zero; //restore original state

      timeInatoAction = 0;
      ingredients.Clear();
      _inAction = false;


      _audioSource.Stop();
    }


    private void PickUpPreppedFood()
    {
      // When there is a prepared ingredient in the pot
      if (ingredients.Count > 0)
      {
        IngredientScript preppedFood;
        if (canHaveMultipleIngredients && ingredients.Count > 1)
        {

          // Debug.Log("THESE ARE THE INGREDIENTS: ");
          // foreach (var ingredient in ingredients)
          // {
          //   Debug.Log(ingredient.name);
          // }

          preppedFood = recipeBook.GetCompoundRecipe(ingredients);
        }
        else
        {

          preppedFood = ingredients[0];
        }

        GameObject preppedFoodObject = Instantiate(preppedFood.foodModel);
        //preppedFoodObject.GetComponent<Ingredient>().SetIngredientScript(preppedFood);
        ingredients.Clear();

        // print(preppedFood.name);
        if (preppedFood.name == "poo")
        {
          StartCoroutine(_gameManager.DisplayPooText());
        }

        player.PickUpOrDrop(preppedFoodObject);
      }
    }


    private IEnumerator StartTimer()
    {
      circleTimerUi.SetActive(true);

      _audioSource.clip = SFX;
      _audioSource.Play();


      // print("start counting down");
      _inAction = true;
      // float timeLeft = timeBeforeBurnt;
      while (_inAction && timeInatoAction < timeBeforeBurnt * 2)
      {
        // fillBar.fillAmount = 1 - timeLeft / timeBeforeBurnt;
        yield return new WaitForSeconds(Time.deltaTime);
        timeInatoAction += Time.deltaTime;
        // timeLeft -= Time.deltaTime;
        // print("timeBeforeBurnt: "+(timeBeforeBurnt-timeLeft));

        var speed = 180 / timeBeforeBurnt; // angle traveled per second
        Vector3 rotateAngle = new Vector3(0, 0, -speed * Time.deltaTime);
        rotateAngle = transform.TransformDirection(rotateAngle);

        arrow.localEulerAngles += rotateAngle;
        // print(arrow.localEulerAngles);
      }

      // print("timeBeforeBurnt up!");
      // PickUpPreppedFood();

      // sfx.PlaySuccessSFX();
      // inMiddleOfOneActivity = false;
    }
  }
}
