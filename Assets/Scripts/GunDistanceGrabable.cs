using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class GunDistanceGrabable : OVRGrabbable
{
  public string m_materialColorField;

  GrabbableCrosshair m_crosshair;
  GunGrabManager m_crosshairManager;
  Renderer m_renderer;
  MaterialPropertyBlock m_mpb;

  public bool InRange
  {
    get { return m_inRange; }
    set
    {
      m_inRange = value;
      RefreshCrosshair();
    }
  }
  bool m_inRange;

  public bool Targeted
  {
    get { return m_targeted; }
    set
    {
      m_targeted = value;
      RefreshCrosshair();
    }
  }
  bool m_targeted;

  protected override void Start()
  {
    base.Start();
    m_crosshair = gameObject.GetComponentInChildren<GrabbableCrosshair>();
     m_renderer = gameObject.GetComponent<Renderer>();
    m_crosshairManager = FindObjectOfType<GunGrabManager>();
    m_mpb = new MaterialPropertyBlock();
    RefreshCrosshair();
    if (m_renderer)
      m_renderer.SetPropertyBlock(m_mpb);
  }

  protected virtual void RefreshCrosshair()
  {
    if (m_crosshair)
    {
      if (isGrabbed) m_crosshair.SetState(GrabbableCrosshair.CrosshairState.Disabled);
      else if (!InRange) m_crosshair.SetState(GrabbableCrosshair.CrosshairState.Disabled);
      else m_crosshair.SetState(Targeted ? GrabbableCrosshair.CrosshairState.Targeted : GrabbableCrosshair.CrosshairState.Enabled);
    }
    if (m_renderer &&m_materialColorField != null)
    {
      m_renderer.GetPropertyBlock(m_mpb);
      if (isGrabbed || !InRange) m_mpb.SetColor(m_materialColorField, m_crosshairManager.OutlineColorOutOfRange);
      else if (Targeted) m_mpb.SetColor(m_materialColorField, m_crosshairManager.OutlineColorHighlighted);
      else m_mpb.SetColor(m_materialColorField, m_crosshairManager.OutlineColorInRange);
      m_renderer.SetPropertyBlock(m_mpb);
    }
  }
}
