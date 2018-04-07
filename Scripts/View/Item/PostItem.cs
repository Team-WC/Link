using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PostItem : UIBehaviour, IViewItem
{
    public RectTransform itemRect;

    public Image userAvatar;
    public Text userName;
    public Text postTimestamp;
    public Text postText;
    public Image postPhoto;

    private int postKey;
    private int userKey;
    public GameObject postPage;
    public GameObject userPage;

    // 여백 조절
    private float itemPadding = Setting.ItemPadding;

    protected override void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(() => { PostPageCall(postKey); });
        Button button;
        if (button = userAvatar.GetComponent<Button>())
            button.onClick.AddListener(() => { UserPageCall(userKey); });
    }

    public void OnUpdateItem(int key)
    {
        Post post = DataManager.instance.GetPost(key);
        User user = DataManager.instance.GetUser(post.UserID);

        this.postKey = key;
        this.userKey = post.UserID;

        userAvatar.sprite = user.Avatar;
        userName.text = user.Name;
        //postTimestamp.text = post.Timestamp.ToString();
        postText.text = post.Text;

        // postText, userName rect 강제 갱신
        //LayoutRebuilder.ForceRebuildLayoutImmediate(userName.rectTransform);
        //LayoutRebuilder.ForceRebuildLayoutImmediate(postText.rectTransform);
        Canvas.ForceUpdateCanvases();

        float itemSizeY = 2 * itemPadding + userName.rectTransform.getSize().y + postText.rectTransform.getSize().y;

        // 이미지가 있을 경우, 없을 경우
        if (post.Photos.Length == 0)
        {
            postPhoto.gameObject.SetActive(false);
            postPhoto.rectTransform.setSize(new Vector2(0, 0));
        }
        else
        {
            postPhoto.gameObject.SetActive(true);
            postPhoto.sprite = post.Photos[0];

            itemSizeY += postPhoto.rectTransform.getSize().y;
        }

        // anchoredPosition 갱신
        postTimestamp.rectTransform.anchoredPosition += new Vector2(itemPadding * 0.5f + userName.rectTransform.getSize().x, 0);
        postPhoto.rectTransform.anchoredPosition -= new Vector2(0, postText.rectTransform.getSize().y);

        if (Setting.PostItmeDefaultSize.y > itemSizeY)
        {
            itemSizeY = Setting.PostItmeDefaultSize.y;
        }

        // item의 높이에 관련된 object만 더해서 setSize()
        Vector2 itemSize = new Vector2(Setting.PostItmeDefaultSize.x, itemSizeY);
        itemRect.setSize(itemSize);
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
