using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    private SimpleShoot simpleShoot;
    private GunDistanceGrabable gunDistanceGrabable;
    public OVRInput.Button shootingButton;
    public OVRInput.Button reloadButton;
    public int maxOfBullet = 10;
    public Text bulletText;

    int numberOfBullet;

    private void Start()
    {
        gunDistanceGrabable = GetComponent<GunDistanceGrabable>();
        simpleShoot = GetComponentInChildren<SimpleShoot>();

        numberOfBullet = maxOfBullet;
        bulletText.text = string.Format("{0} / {1}", numberOfBullet , maxOfBullet);
        bulletText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!gunDistanceGrabable.isGrabbed && !GunGameManager.Instance.isPlaying)
        {
          if (!bulletText.gameObject.activeInHierarchy) bulletText.gameObject.SetActive(false);
          return;
        }
         
        if (!bulletText.gameObject.activeInHierarchy) bulletText.gameObject.SetActive(true);

        GunDistanceGrabber gunGrabber = gunDistanceGrabable.grabbedBy as GunDistanceGrabber;
        if (OVRInput.GetDown(shootingButton,gunGrabber.GetController()) && numberOfBullet > 0)
        {
            simpleShoot.TriggerShoot();
            numberOfBullet--;
            bulletText.text = string.Format("{0} / {1}", numberOfBullet , maxOfBullet);
        }
        else if (OVRInput.GetDown(reloadButton, gunGrabber.GetController()))
        {
          numberOfBullet = maxOfBullet;
          bulletText.text = string.Format("{0} / {1}", numberOfBullet, maxOfBullet);
        }
    }
}
