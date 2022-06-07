using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Not gonna lie this is a mess
public class LoadScene : MonoBehaviour
{

    AsyncOperation loadingOperation;
    public Animation anim;
    static int buildIdx = 0;
    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    public void Load(int index){
        buildIdx = index;
        FadeOut();
    }

    // After finishing fading in, if in the loading scene, immediately fade out
    // Our game loads too fast to use a progress bar lol
    public void OnFinishedFadingIn(){
        anim.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            FadeOut();
        }
    }

    // After fading out, if in the loading scene, go to the next scene
    // Otherwise, go to the loading scene.
    public void OnFinishedFadingOut(){
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(LoadAsyncScene());
        }else{
            GoToLoadingScene();
        }
    }

    void GoToLoadingScene(){
        loadingOperation = SceneManager.LoadSceneAsync(1); 
    }

    public void FadeIn(){
        anim.gameObject.SetActive(true);
        anim.Play("fade_in");
    }

    public void FadeOut(){
        anim.gameObject.SetActive(true);
        anim.Play("fade_out");
    }

    // Load scene asynchronously (so we could use loading bars but rip)
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIdx);
        yield return null;
    }
}
