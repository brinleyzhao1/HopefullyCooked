using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    [SerializeField] LoadScene loadScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFinishedFadingOut(){
        loadScene.OnFinishedFadingOut();
    }

    public void OnFinishedFadingIn(){
        loadScene.OnFinishedFadingIn();
    }
}
