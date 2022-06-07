using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    float timer = 0.0f;
    [SerializeField] List<GameObject> foodPrefabs;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f){
           int idx = Random.Range(0, foodPrefabs.Count);
           var newItem = Instantiate<GameObject>(foodPrefabs[idx], transform.position, transform.rotation);

           if (newItem.GetComponent<AudioSource>())
           {
             newItem.GetComponent<AudioSource>().enabled = false;
           }

           timer = Random.Range(0, 0.5f);

        }
    }
}
