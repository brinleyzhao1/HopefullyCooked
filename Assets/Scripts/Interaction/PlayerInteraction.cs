using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Component for player functionality. Currently a player can pick up, drop,
// and interact with certain objects.
public class PlayerInteraction : MonoBehaviour
{
  // The object that is being picked up
  public GameObject pickedUpObject;

  // The location where a picked up object should go
  public GameObject pickUpLocation;

  // Bool if the player is holding an object
  bool holdingObject = false;

  // This should definitely be a queue, it's a bit buggy
  // with overlapping interactions
  // The object that can currently be interacted with
  GameObject currentlyInteractableObject;
  List<Interactable> interactables = new List<Interactable>();

  private void Update()
  {
    // Debug.Log("Holding object:" + holdingObject);
    // Debug.Log("Object is null: " + (currentlyInteractableObject == null).ToString());
    // Debug.Log("Interactabel Count: " + interactables.Count);
  }


  // private void Start()
  // {
  //   Cursor.lockState = CursorLockMode.None;
  //   Cursor.visible = true;
  // }

  // Pick up an object.
  public void PickUpOrDrop(GameObject obj)
  {
    if (!holdingObject)
    {
      // Picks up the object
      obj.transform.parent = pickUpLocation.transform;
      obj.transform.position = pickUpLocation.transform.position;

      // Turns off physics based movement
      obj.GetComponent<Rigidbody>().isKinematic = true;

      holdingObject = true;
      pickedUpObject = obj;
    }
    else
    {
      Drop();
    }
  }

  // Drops the object.
  public void Drop()
  {
    if (holdingObject)
    {
      //drop the object slightly in front
      //todo extract distance variable
      pickedUpObject.transform.localPosition += Vector3.forward * 0.8f;
      // Drops the object
      pickedUpObject.transform.parent = null;
      // Restores physics based movement
      pickedUpObject.GetComponent<Rigidbody>().isKinematic = false;

      //interactables.Remove(pickedUpObject.GetComponent<Interactable>());
      //currentlyInteractableObject = null;

      holdingObject = false;
      pickedUpObject = null;
    }
  }

  public void Interact()
  {
    if (holdingObject)
    {
      currentlyInteractableObject = pickedUpObject;
      // Someimes objects are destroyed (when placed into station), but the references stick
      // So make sure the interactables are not null
    }
    else if (interactables.Count > 0)
    {
      while (interactables.Count > 0)
      {
        if (!interactables[0])
        {
          interactables.RemoveAt(0);
        }
        else
        {
          currentlyInteractableObject = interactables[0].gameObject;
          break;
        }
      }
    }

    if (currentlyInteractableObject)
    {
      currentlyInteractableObject.GetComponent<Interactable>().Interact();
    }
  }

  // These two functions would be helpful for UI cues
  public void OpenInteractableIcon()
  {
    print("player open interactable icon");
  }

  public void CloseInteractableIcon()
  {
  }

  // Sets the currently interactable object when entering its trigger
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.GetComponent<Interactable>())
    {
      currentlyInteractableObject = other.gameObject;
      interactables.Add(other.gameObject.GetComponent<Interactable>());
    }
  }

  // Removes the currently interable object when leaving its trigger
  private void OnTriggerExit(Collider other)
  {
    //if(other.gameObject == currentlyInteractableObject)
    //{
    //    currentlyInteractableObject = null;
    //}else
    if (other.gameObject.GetComponent<Interactable>())
    {
      interactables.Remove(other.gameObject.GetComponent<Interactable>());
      currentlyInteractableObject = null;
    }
  }
}
