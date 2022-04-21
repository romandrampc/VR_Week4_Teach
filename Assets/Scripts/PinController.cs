using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
  public int pinNumber = 0;

  private bool isCrash = false;
  private bool isPinDown = false;

  private void Start()
  {
    isCrash = false;
    isPinDown = false;
  }

  private void Update()
  {
    if (isCrash)
    {
      var objTranform = gameObject.transform;
      if ((objTranform.rotation.eulerAngles.x > 45 || objTranform.rotation.eulerAngles.x < -45)
        || (objTranform.rotation.eulerAngles.z > 45 || objTranform.rotation.eulerAngles.z < -45))
      {
        isCrash = false;
        isPinDown = true;
        BownlingController.Instance.PinDown(this);
      }
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("BowlingBall") && !isPinDown)
    {
      isCrash = true;
    }
  }
}
