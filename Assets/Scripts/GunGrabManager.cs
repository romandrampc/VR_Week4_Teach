using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGrabManager : MonoBehaviour
{
  Collider m_grabVolume;

  public Color OutlineColorInRange;
  public Color OutlineColorHighlighted;
  public Color OutlineColorOutOfRange;

    private void OnTriggerEnter(Collider other)
    {
        if (!GunGameManager.Instance.isPlaying) return;

        GunDistanceGrabable dg = other.GetComponentInParent<GunDistanceGrabable>();
        if (dg)
        {
            dg.InRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GunDistanceGrabable dg = other.GetComponentInParent<GunDistanceGrabable>();
        if (dg)
        {
            dg.InRange = false;
        }
    }
}
