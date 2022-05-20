using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGrabManager : MonoBehaviour
{
  Collider m_grabVolume;

  public Color OutlineColorInRange;
  public Color OutlineColorHighlighted;
  public Color OutlineColorOutOfRange;

  void OnTriggerEnter(Collider otherCollider)
  {
    GunDistanceGrabable dg = otherCollider.GetComponentInParent<GunDistanceGrabable>();
    if (dg)
    {
      dg.InRange = true;
    }

  }

  void OnTriggerExit(Collider otherCollider)
  {
    GunDistanceGrabable dg = otherCollider.GetComponentInParent<GunDistanceGrabable>();
    if (dg)
    {
      dg.InRange = false;
    }
  }
}
