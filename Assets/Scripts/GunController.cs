using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private SimpleShoot simpleShoot;
    private GunDistanceGrabable gunDistanceGrabable;
    public OVRInput.Button shootingButton;

    private void Start()
    {
        gunDistanceGrabable = GetComponent<GunDistanceGrabable>();
        simpleShoot = GetComponentInChildren<SimpleShoot>();
    }

    private void Update()
    {
        if (!gunDistanceGrabable.isGrabbed)
            return;
        GunDistanceGrabber gunGrabber = gunDistanceGrabable.grabbedBy as GunDistanceGrabber;
        if (OVRInput.GetDown(shootingButton,gunGrabber.GetController()))
        {
            simpleShoot.TriggerShoot();
        }
    }
}
