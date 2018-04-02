﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserItem : UIBehaviour, IViewItem
{
    public RectTransform itemRect;

    public Image userAvatar;
    public Text userName;

    private int userKey;
    public GameObject userOverlay;

    protected override void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(() => { UserOverlayCall(userKey); });
    }

    public void OnUpdateItem(int key)
    {
        User user = DataManager.instance.GetUser(key);

        userKey = key;

        this.userAvatar.sprite = user.Avatar;
        this.userName.text = user.Name;
    }

    private void UserOverlayCall(int key)
    {
        userOverlay.SetActive(true);

        var overlay = userOverlay.GetComponent<IViewItem>();
        if (overlay != null)
            overlay.OnUpdateItem(key);
    }
}
