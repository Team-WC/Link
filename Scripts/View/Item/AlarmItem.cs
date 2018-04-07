using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlarmItem : UIBehaviour, IViewItem
{
    public RectTransform itemRect;

    public Image userAvatar;
    public Text alarmText;
    private AlarmType alarmType;
    private int alarmlinkID;

    public GameObject postPage;
    public GameObject userPage;

    private float itemPadding = Setting.ItemPadding;

    protected override void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(() => { PageCall(alarmlinkID); });
    }

    public void OnUpdateItem(int key)
    {
        Alarm alarm = DataManager.instance.GetAlarm(key);

        alarmText.text = alarm.Text;
        alarmType = alarm.Type;
        alarmlinkID = alarm.LinkID;

        if (alarmType == AlarmType.Post)
        {
            userAvatar.sprite = DataManager.instance.GetUser(DataManager.instance.GetPost(alarmlinkID).UserID).Avatar;
        }
        else if (alarmType == AlarmType.Information)
        {
            userAvatar.sprite = DataManager.instance.GetUser(alarmlinkID).Avatar;
        }

        Canvas.ForceUpdateCanvases();
        Vector2 itemSize = alarmText.rectTransform.getSize() + new Vector2(0, itemPadding * 2);
        itemRect.setSize(itemSize);
    }

    public void PageCall(int linkID)
    {
        if (alarmType == AlarmType.Post)
        {
            PostPageCall(linkID);
        }
        else if (alarmType == AlarmType.Information)
        {
            UserPageCall(linkID);
        }
    }
    
    private void PostPageCall(int key)
    {
        postPage.SetActive(true);

        var page = postPage.GetComponent<IViewItem>();
        if (page != null)
            page.OnUpdateItem(key);
    }

    private void UserPageCall(int key)
    {
        userPage.SetActive(true);

        var page = userPage.GetComponent<IViewItem>();
        if (page != null)
            page.OnUpdateItem(key);
    }
}