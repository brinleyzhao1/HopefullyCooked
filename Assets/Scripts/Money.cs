using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
  [SerializeField] private float moneyLeft;

  [SerializeField] private TextMeshProUGUI moneyText;

  // Start is called before the first frame update
  void Start()
  {
    UpdateMoneyText();
  }

  // Update is called once per frame
  void Update()
  {
  }

  void UpdateMoneyText()
  {
    moneyText.text = "$: " + moneyLeft;
  }

  //should be called when purchasing things
  public void SubtractMoney(float amount)
  {
    moneyLeft -= amount;
    UpdateMoneyText();
  }

  public float GetCurrentMoneyLeft()
  {
    return moneyLeft;
  }
}
