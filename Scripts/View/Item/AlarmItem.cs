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

    public GameObject userOverlay;
    public GameObject postOverlay;

    private float itemPadding = Setting.ItemPadding;

    protected override void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(() => { OverlayCall(alarmlinkID); });
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

    public void OverlayCall(int linkID)
    {
        if (alarmType == AlarmType.Post)
        {
            PostOverlayCall(linkID);
        }
        else if (alarmType == AlarmType.Information)
        {
            UserOverlayCall(linkID);
        }
    }

    private void UserOverlayCall(int key)
    {
        userOverlay.SetActive(true);

        var overlay = userOverlay.GetComponent<IViewItem>();
        if (overlay != null)
            overlay.OnUpdateItem(key);
    }

    private void PostOverlayCall(int key)
    {
        postOverlay.SetActive(true);

        var overlay = postOverlay.GetComponent<IViewItem>();
        if (overlay != null)
            overlay.OnUpdateItem(key);
    }
}