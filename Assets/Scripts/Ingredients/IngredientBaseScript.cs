using System;
using System.Collections;
using System.Collections.Generic;
using Ingredients;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public enum EFood
{
  // FriedRice, MacAndCheese, PastaTomato, Salad, Sandwich, Soup
  Broth,
  BurgerAndFries,
  Cabbage,
  CabbageChopped,
  CabbageChoppedBoiled,
  Carrot,
  CarrotChopped,
  CarrotChoppedBoiled,
  CheeseSliced,
  ChineseTakeout,
  EggRaw,
  EggPanFried,
  FriedRice,
  Loaf,
  MacAndCheese,
  PastaTomato,
  PastaRaw,
  PastaBoiled,
  Pizza,
  Poo,
  Potato,
  PotatoBoiled,
  PotatoMashed,
  RiceRaw,
  RiceBoiled,
  Salad,
  Sandwich,
  SausageRaw,
  SausageCooked, // boiled sausage??
  Soup,
  Toast,
  Tomato,
  TomatoChopped,
  TomatoChoppedCooked,
  Charcoal
}

public enum EPrepMethod
{
  Assemble,
  Boil,
  Chop,
  Mash,
  Microwave,
  PanFry
}

// In case general Ingredient Scripts want to be instantiated,
// this base script is needed (since abstract classes can't
// be instantiated).
public abstract class IngredientBaseScript : ScriptableObject
{
  public EFood foodName;
  public string displayName;
  public GameObject foodModel;
  public List<IngredientScript> requiredIngredients;
  public List<IngredientScript> optionalIngredients;

  public float scoreModifier = 1;

  //[SerializeField]
  //public Vector3 foodPosition = new Vector3(0,-0.15f,0);
  //public Vector3 foodScale = new Vector3(2,2,2);
  [HideInInspector] public List<EPrepMethod> SequenceOfPreparations;
  public float price;

  // States of ingredients
  [HideInInspector] public bool raw = true;
  [HideInInspector] public bool boiled = false;
  [HideInInspector] public bool mashed = false;
  [HideInInspector] public bool destroyed = false;

  // Preparation methods for ingredients

  // public string GetDisplayName()
  // {
  //   string originalName = foodName.ToString();
  //   for (int i = 0; i < originalName.Length; i++)
  //   {
  //
  //     var a = originalName[i];
  //     if (Char.IsUpper(originalName[i]))
  //     {
  //       // print("yes upper case");
  //
  //       // originalName = originalName.Substring(i) + originalName.Substring(0, i);
  //       originalName = originalName.Substring(i);
  //       return originalName;
  //     }
  //   }
  //
  //   return originalName;
  // }

  public abstract void Boil();

  public abstract void Mash();

  public abstract void Prepare(EPrepMethod currentPreparation);
}
