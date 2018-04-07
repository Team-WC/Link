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
        for (int i = 0; i < 4; i++)
        {
            badge[i].SetActive(false);
        }

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void BadgeOn(int page)
    {
        badge[page].SetActive(true);
    }

    public void BadgeOff(int page)
    {
        badge[page].SetActive(false);
    }
}
