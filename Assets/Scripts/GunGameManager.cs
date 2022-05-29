using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GunGameManager : MonoBehaviour
{
    public static GunGameManager Instance { get; private set; }

    public float gameTime;
    public GameObject resultPanel;
    public GameObject timePanel;
    public Text scoreText;
    public Text timeText;

    float countDownGameTimer;
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

        resultPanel.SetActive(false);
        timePanel.SetActive (true);
        score = 0;
        isPlaying = false;
        countDownGameTimer = gameTime;
        timeText.text = "00:00";

        Bottle[]  bottleObj = GameObject.FindObjectsOfType<Bottle>();
        foreach(Bottle b in bottleObj)
        {
            bottles.Add(b);
        }
    }

    void Start()
    {
        StartCoroutine(WaitForStartGame());
    }


    void Update() {
        if (isPlaying) {
            if (countDownGameTimer > 0) {
                countDownGameTimer -= Time.deltaTime;
                updateTimer(countDownGameTimer);
            }
            else {
                isPlaying = false;
                resultPanel.SetActive(true);
                scoreText.text = "Game OVER";
                StartCoroutine(WaitForReloadGame());
            }
        }
    }

    void updateTimer(float currentTime) {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void bottleHit(Bottle bottle)
    {
        score += 1;
        bottles.Remove(bottle);
        Destroy(bottle.gameObject, 2.0f);
        if (bottles.Count <= 0)
        {
            isPlaying = false;
            showResult();
        }
    }

    void showResult()
    {
        float calScore = score + Mathf.Floor(countDownGameTimer * 2);
        resultPanel.SetActive(true);
        scoreText.text = string.Format("Score : {0}", calScore);
        StartCoroutine(WaitForReloadGame());
    }

    IEnumerator WaitForStartGame()
    {
        yield return new WaitForSeconds(5);

        isPlaying = true;
    }

    IEnumerator WaitForReloadGame()
    {
        yield return new WaitForSeconds(10);

        SceneManager.LoadScene("Gun");
    }
}
