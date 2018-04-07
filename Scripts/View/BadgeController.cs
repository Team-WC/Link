using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BadgeController : UIBehaviour
{
    public GameObject postsBadge;
    public GameObject usersBadge;
    public GameObject alarmsBadge;
    public GameObject messagesBadge;
	
	public static BadgeController instance = null;

    protected override void Awake()
    {
        postsBadge.SetActive(false);
        usersBadge.SetActive(false);
        alarmsBadge.SetActive(false);
        messagesBadge.SetActive(false);

		if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void BadgeOn(int page)
    {
        if (page == 0)
        {
            postsBadge.SetActive(true);
        }
        else if (page == 1)
        {
            usersBadge.SetActive(true);
        }
        else if (page == 2)
        {
            alarmsBadge.SetActive(true);
        }
        else if (page == 3)
        {
            messagesBadge.SetActive(true);
        }
        else
        {
            Debug.LogError("Out of badge range");
        }
    }

	public void BadgeOff(int page)
    {
        if (page == 0)
        {
            postsBadge.SetActive(false);
        }
        else if (page == 1)
        {
            usersBadge.SetActive(false);
        }
        else if (page == 2)
        {
            alarmsBadge.SetActive(false);
        }
        else if (page == 3)
        {
            messagesBadge.SetActive(false);
        }
        else
        {
            Debug.LogError("Out of badge range");
        }
    }
}
