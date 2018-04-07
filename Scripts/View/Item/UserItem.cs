using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserItem : UIBehaviour, IViewItem
{
    public RectTransform itemRect;

    public Image userAvatar;
    public Text userName;

    private int userKey;
    public GameObject userPage;

    protected override void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(() => { UserPageCall(userKey); });
    }

    public void OnUpdateItem(int key)
    {
        User user = DataManager.instance.GetUser(key);

        userKey = key;

        this.userAvatar.sprite = user.Avatar;
        this.userName.text = user.Name;
    }

    private void UserPageCall(int key)
    {
        userPage.SetActive(true);

        var page = userPage.GetComponent<IViewItem>();
        if (page != null)
            page.OnUpdateItem(key);
    }
}
