using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GunGameManager : MonoBehaviour
{
  public static GunGameManager Instance { get; private set; }

  public float gameTime;
  public GameObject resultPanel;
  public GameObject timePanel;
  public Text scoreText;
  public Text timeText;

  float countDownGametimer;
  bool isPlaying = false;
  float score;
  List<Bottle> bottles = new List<Bottle>();

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(this);
    }
    else
    {
      Instance = this;
    }

    score = 0;
    isPlaying = false;
    countDownGametimer = gameTime;
    timeText.text = countDownGametimer.ToString();
    resultPanel.SetActive(false);
    timePanel.SetActive(true);


    GameObject[] pinObj = GameObject.FindGameObjectsWithTag("Bottle");
    
    for (int i = 0; i < pinObj.Length; i++)
    {
      Bottle bottle = pinObj[i].GetComponent<Bottle>();
      bottles.Add(bottle);
    }
    
  }

  private void Start()
  {
    StartCoroutine(WaitForStartGame());
  }

  void Update()
  {
    if (isPlaying)
    {
      if (countDownGametimer > 0)
      {
        countDownGametimer -= Time.deltaTime;
        updateTimer(countDownGametimer);
      }
      else
      {
        isPlaying = false;
        resultPanel.SetActive(true);
        scoreText.text = "Game OVER";
        StartCoroutine(WaitForReloadGame());
      }
    }
  }

  void updateTimer(float currentTime)
  {
    currentTime += 1;
    float minutes = Mathf.FloorToInt(currentTime / 60);
    float seconds = Mathf.FloorToInt(currentTime % 60);

    timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
  }


  IEnumerator WaitForStartGame()
  {
    yield return new WaitForSeconds(5);

    isPlaying = true;
  }

  public void bottleHit(Bottle bottle)
  {
    score += 1;
    bottles.Remove(bottle);
    bottle.gameObject.SetActive(false);
    if (bottles.Count<=0)
    {
      isPlaying = false;
      showResult();
    }
  }

  void showResult()
  {
    float calScore = score + Mathf.Floor(countDownGametimer * 2);
    resultPanel.SetActive(true);
    scoreText.text = string.Format("Score : {0}",calScore);
    StartCoroutine(WaitForReloadGame());
  }

  IEnumerator WaitForReloadGame()
  {
    yield return new WaitForSeconds(10);

    SceneManager.LoadScene("Gun");
  }
}
