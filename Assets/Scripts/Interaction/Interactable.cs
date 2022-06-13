using System;
using UnityEngine;


//https://www.youtube.com/watch?v=GaVADPZlO0o
[RequireComponent(typeof(BoxCollider))]
// Specifications for anything that is interactable. Currently, ingredients
// and prep stations are the only interactable objects.
public abstract class Interactable : MonoBehaviour
{
  [HideInInspector] public PlayerInteraction player;


  // All interactable objects need a reference to the player and
  // need trigger box colliders (which specify interaction distance)
  public void Initialize()
  {
    player = FindObjectOfType<PlayerInteraction>();
    //disabled this line to not run into stations
    // GetComponent<BoxCollider>().isTrigger = true;
  }

  // Specify what happens when the players interacts with the object
  public abstract void Interact();

  // Triggers currently unused, but would be helpful for UI signaling
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.GetComponent<PlayerInteraction>())
    {
      OpenUIText();
      // other.gameObject.GetComponent<PlayerInteraction>().OpenInteractableIcon();
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.GetComponent<PlayerInteraction>())
    {
      CloseUIText();
      // other.gameObject.GetComponent<PlayerInteraction>().CloseInteractableIcon();
    }
  }

  protected void OpenUIText()
  {
    Transform name = transform.Find("name"); //text object must be name "name"
    if (name)
    {
      name.gameObject.SetActive(true);
      // print(name);
    }
  }

  protected void CloseUIText()
  {
    Transform name = transform.Find("name");
    if (name)
    {
      name.gameObject.SetActive(false);
      // print(name);
    }
  }
}
