using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BadgeController : UIBehaviour
{
    public GameObject[] badge;

    public static BadgeController instance = null;

    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void BadgeOn(int page)
    {
        if (!badge[page].activeInHierarchy)
            badge[page].SetActive(true);
    }

    public void BadgeOff(int page)
    {
        if (badge[page].activeInHierarchy)
            badge[page].SetActive(false);
    }
}
