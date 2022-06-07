using System;
using TMPro;
using UnityEngine;

namespace Stations
{
  public class PantryShop : Interactable
  {
    [Header("information")]
    public GameObject foodPrefab;
    public int numberLeft;

    [Header("references")]
    [SerializeField] private GameObject textDisplayed;

    [SerializeField] private AudioClip freeSFX;
    [SerializeField] private AudioClip moneySFX;
    [SerializeField] private AudioClip notEnoughMoneySfx;
    // [SerializeField] private Transform instantiateLocation;

    private Money _money;
    private float _price;
    private IngredientScript _ingredientScript;
    private AudioSource _audioSource;

    private void Start()
    {
      _money = FindObjectOfType<Money>(); //can be optimized later
      _ingredientScript = foodPrefab.GetComponent<Ingredient>().ingredientScript;
      _price = _ingredientScript.price;
      _audioSource = GetComponent<AudioSource>();

      if (_audioSource == null)
      {
        print("pantry needs an audio source");
      }
      Initialize();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //   if (other.GetComponent<PlayerInteraction>())
    //   {
    //     print("detected player");
    //   }
    // }

    public override void Interact()
    {
      if (numberLeft > 0)
      {
        InstantiateIngredient();

        numberLeft -= 1;
        UpdateText();
        _audioSource.PlayOneShot(freeSFX);
      }
      else //try purchase
      {
        if (_money.GetCurrentMoneyLeft() >= _price)
        {
          InstantiateIngredient();
          _money.SubtractMoney(_price);
          _audioSource.PlayOneShot(moneySFX);

        }
        else
        {
          _audioSource.PlayOneShot(notEnoughMoneySfx);
          print("not enough money to buy");
        }
      }
    }

    private void InstantiateIngredient()
    {
      //just dropping things onto the floor for now
      var newItem = Instantiate(foodPrefab);
      //foodPrefab.SetIngredientScript(_ingredientScript);
      // newItem.SetIngredientScript(_ingredientScript);


      //newItem.transform.position = instantiateLocation.position;
      player.PickUpOrDrop(newItem.gameObject);

    }

    private void UpdateText()
    {
      if (numberLeft > 0)
      {
        textDisplayed.GetComponent<TextMeshPro>().text = numberLeft.ToString();
      }
      else
      {
        textDisplayed.GetComponent<TextMeshPro>().text = "$" + _price;
      }
    }
  }
}
