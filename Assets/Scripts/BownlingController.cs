using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BownlingController : MonoBehaviour
{
  public static BownlingController Instance { get; private set; }

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
}
