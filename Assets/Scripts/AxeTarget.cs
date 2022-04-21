using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AxeTarget : MonoBehaviour
{
  [SerializeField]
  GameObject centerOffset;

  int score = 0;
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("AxeHead"))
    {
      Rigidbody rigid = collision.gameObject.transform.parent.GetComponent<Rigidbody>();
      rigid.useGravity = false;
      rigid.isKinematic = true;

      float distance = Vector3.Distance(collision.gameObject.transform.position, centerOffset.transform.position);
      Debug.Log(distance);
      if (distance < 0.15) {
        score += 10;
      }
      else if (distance >= 0.15 && distance < 0.4) {
        score += 4;
      }
      else {
        score += 1;
      }
      Debug.Log("score : " + score);
    }
  }
}
