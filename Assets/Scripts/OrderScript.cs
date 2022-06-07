using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order Data", menuName = "Order Data", order = 52)]
public class OrderScript : ScriptableObject
{
    public string request;
    public GenericDictionary<EFood, int> satisfyingFoods;
    public float timeLimit;
}
