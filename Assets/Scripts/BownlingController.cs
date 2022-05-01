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

public class BownlingController : MonoBehaviour
{
  private PinProp[] pinProps;

  private void Start()
  {
    GameObject[] pinObj = GameObject.FindGameObjectsWithTag("PinBowling");
    pinProps = new PinProp[pinObj.Length];
    for (int i = 0; i < pinObj.Length; i++)
    {
      PinController pin = pinObj[i].GetComponent<PinController>();
      PinProp prop = new PinProp(pin.pinNumber, pin.gameObject.transform.position);
      pinProps[i] = prop;
    }
  }
  public static BownlingController Instance { get; private set; }

  public GameObject bowlingBall;
  public GameObject bowlingSpawnPoint;



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



  public void PinDown(PinController Pin)
  {
    Debug.Log("Pin Number :" + Pin.pinNumber);
  }

  public void OnRespawnBowling()
  {
    bowlingBall.SetActive(true);
    bowlingBall.transform.position = bowlingSpawnPoint.transform.position;
    bowlingBall.transform.rotation = Quaternion.identity;
  }
}
