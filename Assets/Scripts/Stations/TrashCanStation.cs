using UnityEngine;

namespace Stations
{
  public class TrashCanStation : Interactable
  {
    // Start is called before the first frame update
    void OnCollisionEnter(Collision other)
    {
      if (other.gameObject.GetComponent<Ingredient>()){

        Destroy(other.gameObject);
      }
    }

    public override void Interact()
    {
      return;
    }
  }
}
