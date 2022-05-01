using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct PinProp
{
  public int pinNumber;
  public Vector3 pinPos;
  public PinProp(int pinNumber, Vector3 pinPos)
  {
    this.pinNumber = pinNumber;
    this.pinPos = pinPos;
  }
}

struct FrameScore
{
  public int score { set; get; }
  public int extraPoint { set; get; }
  public int frameRound1 { set; get; }
  public int frameRound2 { set; get; }
  public int frameRound3 { set; get; }
}

public class BownlingController : MonoBehaviour
{
  private PinProp[] pinProps;

  public static BownlingController Instance { get; private set; }

  public GameObject bowlingBall;
  public GameObject bowlingSpawnPoint;

  List<FrameScore> frameScores = new List<FrameScore>();
  List<PinController> activePins = new List<PinController>();
  List<PinController> downPins = new List<PinController>();
  List<int> scores = new List<int>();
  int frameCount = 0;
  int ballCount = 0;
  int score = 0;


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
  }

  private void Start()
  {
    GameObject[] pinObj = GameObject.FindGameObjectsWithTag("PinBowling");
    pinProps = new PinProp[pinObj.Length];
    for (int i = 0; i < pinObj.Length; i++)
    {
      PinController pin = pinObj[i].GetComponent<PinController>();
      PinProp prop = new PinProp(pin.pinNumber, pin.gameObject.transform.position);
      pinProps[i] = prop;
      activePins.Add(pin);
    }
    frameCount = 0;
    ballCount = 0;
  }


  public void PinDown(PinController Pin)
  {
    Debug.Log("Pin Number :" + Pin.pinNumber);
    downPins.Add(Pin);
    activePins.Remove(Pin);
    Debug.Log(downPins.Count);
    Debug.Log(activePins.Count);
  }

  void RespawnBowling()
  {
    bowlingBall.SetActive(true);
    bowlingBall.transform.position = bowlingSpawnPoint.transform.position;
    bowlingBall.transform.rotation = Quaternion.identity;
  }

  public void OnRespawnBowling()
  {
    ballCount++;
    CalculateScore();
    if (ballCount >= 2 && frameCount < 9)
    {
      ballCount = 0;
      frameCount++;
      OnResetFrame();
      RespawnBowling();
    }
    else if (frameCount == 9)
    {
      if (ballCount == 2 && activePins.Count <= 0)
      {
        OnResetFrame();
        RespawnBowling();
      }
    }
    else
    {
      SetPin();
      RespawnBowling();
    }

  }

  void OnResetFrame()
  {
    activePins.AddRange(downPins);
    downPins.RemoveAll(downPins.Contains);
    SetPin();
  }

  void SetPin()
  {
    foreach (PinController pin in downPins)
    {
      pin.gameObject.SetActive(false);
    }

    foreach (PinController pin in activePins)
    {
      pin.gameObject.SetActive(true);
      Vector3 pos = Vector3.zero;

      foreach (PinProp prop in pinProps)
      {
        if (prop.pinNumber == pin.pinNumber)
          pos = prop.pinPos;
      }
      pin.gameObject.transform.position = pos;
      pin.gameObject.transform.rotation = Quaternion.identity;
      pin.Init();
    }
  }

  void CalculateScore()
  {
    bool isAllDown = activePins.Count <= 0;
    bool isStrike = isAllDown && ballCount == 1;
    if (isStrike)
    {
      if (frameScores.Count < frameCount + 1)
      {
        FrameScore frame = new FrameScore();
        frame.score = 10;
        frame.extraPoint = 2;
        frameScores.Add(frame);
      }

      if (frameScores.Count >= 2)
      {
        FrameScore framePre = (FrameScore)frameScores[frameCount - 1];
        if (framePre.extraPoint > 0)
        {
          framePre.score += 10;
          framePre.extraPoint -= 1;
        }
      }

      if (frameScores.Count >= 3)
      {
        FrameScore framePre = (FrameScore)frameScores[frameCount - 2];
        if (framePre.extraPoint > 0)
        {
          framePre.score += 10;
          framePre.extraPoint -= 1;
        }
      }
    }
    else
    {
      if (isAllDown)
      {
        FrameScore frame = (FrameScore)frameScores[frameCount];
        if (frameScores.Count > 2)
        {
          FrameScore framePre = (FrameScore)frameScores[frameCount - 1];
          if (framePre.extraPoint > 0)
          {
            framePre.score += (10 - frame.score);
            framePre.extraPoint -= 1;
          }
        }
        frame.score = 10;
      }
      else
      {
        if (frameScores.Count < frameCount + 1)
        {
          FrameScore frame = new FrameScore();
          frame.score = downPins.Count;
          frame.extraPoint = 0;
          frameScores.Add(frame);
        }
        else if (frameScores.Count == frameCount + 1)
        {
          FrameScore frame = (FrameScore)frameScores[frameCount];
          frame.score += downPins.Count;
        }


        if (frameScores.Count > 2)
        {
          FrameScore framePre = (FrameScore)frameScores[frameCount - 1];
          if (framePre.extraPoint > 0)
          {
            framePre.score += downPins.Count;
            framePre.extraPoint -= 1;
          }
        }
      }
    }
  }
}
