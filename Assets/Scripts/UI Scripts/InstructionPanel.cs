using System.Collections;
using System.Collections.Generic;
using Misc_Scripts;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InstructionPanel : MonoBehaviour
{
  [SerializeField] private GameManager gameManager;
  private ThirdPersonController player;
  private Image panelBG;
  private int numberClicked = 0;

  private GameObject message01;
  private GameObject instructions;

  private StarterAssetsInputs _input;

  private bool stopLoop = false;

  // Start is called before the first frame update
  void Start()
  {
    panelBG = GetComponent<Image>();
    _input = FindObjectOfType<StarterAssetsInputs>();
    player = FindObjectOfType<ThirdPersonController>();
    print(_input);


    gameManager.enabled = false;// .SetActive(false);
    player.enabled = false;
    panelBG.enabled = true;

    message01 = gameObject.transform.GetChild(0).gameObject;
    instructions = gameObject.transform.GetChild(1).gameObject;

    message01.SetActive(true);
    instructions.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (_input.interact && !stopLoop)

    {
      MoveToNextPage();
      stopLoop = true;
    }
    else if (!_input.interact)
    {
      stopLoop = false;
    }
  }

  private void MoveToNextPage()
  {
    if (numberClicked == 0)
    {
      // print("0");
      message01.SetActive(false);
      instructions.SetActive(true);
    }
    else
    {
      // print("1");
      gameManager.enabled = true; //.SetActive(true);
      player.enabled = true;


      Destroy(gameObject);
    }

    numberClicked += 1;
  }
}
