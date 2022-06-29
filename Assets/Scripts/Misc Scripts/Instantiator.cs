using System.Collections.Generic;
using UnityEngine;

namespace Misc_Scripts
{
  public class Instantiator : MonoBehaviour
  {
    float timer = 0.0f;

    // [SerializeField] private AudioClip foodDropSfx;
    private int sequenceCounter = 0;
    [SerializeField] private AudioSource BMG;
    [SerializeField] private float longWaitTime;
    [SerializeField] private float shortWaitTime;

    [SerializeField] List<GameObject> foodPrefabs;

    // Start is called before the first frame update
    void Start()
    {
      BMG.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
      // if (!BMG.gameObject.activeSelf)
      // {
      //   BMG.gameObject.SetActive(true);
      // }

      timer += Time.deltaTime;

      if (timer >= longWaitTime)
      {
        int idx = Random.Range(0, foodPrefabs.Count);
        var newItem = Instantiate<GameObject>(foodPrefabs[idx], transform.position, transform.rotation);

        if (newItem.GetComponent<AudioSource>())
        {
          newItem.GetComponent<AudioSource>().enabled = false;
        }

        // timer = Random.Range(0, 0.5f);

        if (sequenceCounter % 4 == 0)
        {
          timer = 0f;
        }
        else
        {
          timer = longWaitTime - shortWaitTime;
        }

        sequenceCounter += 1;
      }
    }
  }
}
