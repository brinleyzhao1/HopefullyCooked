using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField] OrderManager orderManager;
  [SerializeField] bool inLevel1;
  [SerializeField] bool inLevel2;
  [SerializeField] StarterAssets.StarterAssetsInputs playerInputs;
  [SerializeField] float score = 0.0f;
  [SerializeField] float scoreToWin = 20f;
  [SerializeField] GameObject Level1StartScreen;
  [SerializeField] GameObject Level2StartScreen;
  [SerializeField] GameObject endUI;
  [SerializeField] GameObject WinBanner;
  [SerializeField] GameObject LoseBanner;
  [SerializeField] TMPro.TextMeshProUGUI time;
  [SerializeField] TMPro.TextMeshProUGUI scoreText;
  [SerializeField] float timeLimit;

  [Header("Hint Texts")] [SerializeField]
  private TextMeshPro floorHintMessager;

  //[SerializeField] Animation endBannerAnimation;

  //static bool startedLevel1 = false;
  //static bool startedLevel2 = false;

  bool startedCoroutine = false;

  bool stopCounting = false;

  //[SerializeField] LoadScene loadScene;
  // Start is called before the first frame update
  void Start()
  {
    //PauseGame();
    // if (inLevel1){
    //     if (startedLevel1){
    //         Level1StartScreen.SetActive(false);
    //         StartLevel();
    //     }else{
    //         Level1StartScreen.SetActive(true);
    //         startedLevel1 = true;
    //     }
    // }
    // if(inLevel2){
    //     if (startedLevel2){
    //         Level2StartScreen.SetActive(false);
    //         StartLevel2();
    //     }else{
    //         Level2StartScreen.SetActive(true);
    //         startedLevel2 = true;
    //     }
    // }
    // StartLevel();
    endUI.SetActive(false);
    if (inLevel2){
        orderManager.freeze = false;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (!inLevel1)
    {
      //no update needed for tutorial bc no time limit
      if (!stopCounting)
      {
        timeLimit -= Time.deltaTime;
      }

      if (timeLimit <= 0 && !startedCoroutine)
      {
        EndLevel();
        startedCoroutine = true;
        timeLimit = 0;
        stopCounting = true;
      }

      time.text = ((int) timeLimit).ToString();
    }
  }

  public void Score(float inScore)
  {
    if (stopCounting) return;
    score += inScore;
    scoreText.text = "Score: " + score;
  }

  public void StartLevel()
  {
    if (inLevel1)
    {
      StartLevel1();
    }

    if (inLevel2)
    {
      StartLevel2();
    }
  }

  public void StartLevel1()
  {
    // Level1StartScreen.SetActive(true);
    UnPauseGame();
  }

  public void StartLevel2()
  {
    if (Level2StartScreen.activeSelf)
    {
      Level2StartScreen.GetComponent<Animation>().Play();
    }

    UnPauseGame();
  }

  void EndLevel()
  {
    //StartCoroutine(WaitCoroutine());
    if (score >= scoreToWin)
    {
      WinBanner.SetActive(true);
    }
    else
    {
      LoseBanner.SetActive(true);
    }

    scoreText.text = "Final Score: " + score;
    endUI.SetActive(true);
    PauseGame();
  }

  public void RestartLevel()
  {
    StartLevel();
  }

  void UnPauseGame()
  {
    playerInputs.disableInput = false;
    if (!inLevel1)
    {
      //order manager not relevant for tutorial
      orderManager.freeze = false;
    }

    stopCounting = false;
  }

  void PauseGame()
  {
    playerInputs.disableInput = true;
    if (!inLevel1)
    {
      //order manager not relevant for tutorial
      orderManager.freeze = true;
    }

    stopCounting = true;
  }

  public IEnumerator DisplayPooText()
  {
    // floorHintMessager.SetActive(true);
    floorHintMessager.text = "huh something went wrong.";
    // floorHintMessager.transform.GetChild(0).gameObject.SetActive(true);
    yield return new WaitForSeconds(1.5f);
    floorHintMessager.text = "";
    // floorHintMessager.SetActive(false);
    // floorHintMessager.transform.GetChild(0).gameObject.SetActive(false);
  }

  public IEnumerator DisplayUndercookedText()
  {
    // floorHintMessager.SetActive(true);
    // print(floorHintMessager.GetComponent<TextMeshProUGUI>());
    floorHintMessager.text = "oops it's undercooked";
    // floorHintMessager.transform.GetChild(1).gameObject.SetActive(true);
    yield return new WaitForSeconds(1.5f);
    floorHintMessager.text = "";
    // floorHintMessager.SetActive(false);
    // floorHintMessager.transform.GetChild(1).gameObject.SetActive(false);
  }


  public IEnumerator DisplayOvercookedText()
  {
    // floorHintMessager.SetActive(true);
    floorHintMessager.text = "oops it's overcooked";
    yield return new WaitForSeconds(1.5f);
    floorHintMessager.text = "";
    // floorHintMessager.SetActive(false);
    // floorHintMessager.transform.GetChild(2).gameObject.SetActive(false);
  }
}
