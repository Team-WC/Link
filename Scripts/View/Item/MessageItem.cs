﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageItem : UIBehaviour, IViewItem
{
    public RectTransform itemRect;

    public RectTransform bubbleRect;
    public Text messageText;
    public Image userAvatar;

    public RectTransform secret;

    public GameObject messageOptionPage;

    private float itemPadding = Setting.ItemPadding;

    public void OnUpdateItem(int key)
    {
        Message message = DataManager.instance.GetMessage(key);
        User user = DataManager.instance.GetUser(message.UserID);

        userAvatar.sprite = user.Avatar;

        // Message or Option
        if (message.Options.Count != 2)
        {
            bubbleRect.gameObject.SetActive(true);
            secret.gameObject.SetActive(false);

            messageText.text = message.Options[0].Text;

            // Text 사이즈 조절
            Canvas.ForceUpdateCanvases();
            if (messageText.rectTransform.getSize().y < 40f) // 글자 사이즈 높이에 해당하는 값을 입력
            {
                messageText.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            }

            // Bubble, Item 사이즈 조절
            Canvas.ForceUpdateCanvases();
            Vector2 bubbleSize = messageText.rectTransform.getSize() + new Vector2(itemPadding * 2, itemPadding * 2);
            bubbleRect.setSize(bubbleSize);
            itemRect.setSize(new Vector2(itemRect.getSize().x, bubbleRect.getSize().y));

            // Message Item 위치 조절
            if (message.UserID == 0)
            {
                bubbleRect.setOffset(-bubbleRect.offsetMax.x, bubbleRect.offsetMin.y, -bubbleRect.offsetMin.x, bubbleRect.offsetMax.y);
                userAvatar.rectTransform.setOffset(-userAvatar.rectTransform.offsetMax.x, userAvatar.rectTransform.offsetMin.y, -userAvatar.rectTransform.offsetMin.x, userAvatar.rectTransform.offsetMax.y);
            }

            // 선택지가 있는 경우 호출
            if (message.Options.Count != 1)
            {
                messageOptionPageCall(key);
            }
        }
        else // Secret
        {
            bubbleRect.gameObject.SetActive(false);
            secret.gameObject.SetActive(true);

            itemRect.setSize(new Vector2(itemRect.getSize().x, secret.getSize().y));
        }

        // Event Trigger
        if (message.Trigger)
        {
            ScenarioManager.instance.TriggerCheck("Message", key);
        }
    }

    private void messageOptionPageCall(int key)
    {
        messageOptionPage.SetActive(true);

        var page = messageOptionPage.GetComponent<IViewItem>();
        if (page != null)
            page.OnUpdateItem(key);
    }
}