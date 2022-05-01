using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
  public float waitTime = 5.0f;

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("BowlingBall"))
    {
      collision.gameObject.SetActive(false);
      StartCoroutine(WaitToRespawn());
    }
  }

  IEnumerator WaitToRespawn()
  {
    yield return new WaitForSeconds(waitTime);
    BownlingController.Instance.OnRespawnBowling();
  }
}
