using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PostOverlay : UIBehaviour, IViewItem
{
    public Button closeButton;

    public RectTransform contentRect;

    public Image userAvatar;
    public Text userName;
    public Text postText;
    public Image[] postPhotos;

    public Scrollbar scrollbar;

    private float itemPadding = Setting.ItemPadding;

    protected override void Awake()
    {
        this.gameObject.SetActive(false);
        closeButton.GetComponent<Button>().onClick.AddListener(() => { Close(); });
    }

    public void OnUpdateItem(int key)
    {
        Post post = DataManager.instance.GetPost(key);
        User user = DataManager.instance.GetUser(post.UserID);

        this.userAvatar.sprite = user.Avatar;
        this.userName.text = user.Name;
        this.postText.text = post.Text;

        // postText 높이 강제 갱신
        LayoutRebuilder.ForceRebuildLayoutImmediate(postText.rectTransform);
        //Canvas.ForceUpdateCanvases();

        Vector2 baseAnchoredPosition = postText.rectTransform.anchoredPosition - new Vector2(0f, postText.rectTransform.getSize().y + itemPadding);

        for (int i = 0; i < Setting.PhotoMaxCount; i++)
        {
            if (i < post.PhotoCount)
            {
                postPhotos[i].gameObject.SetActive(true);
                postPhotos[i].sprite = post.Photos[i];
                // 이미지 위치 세팅
                postPhotos[i].rectTransform.anchoredPosition = baseAnchoredPosition - new Vector2(0f, (postPhotos[i].rectTransform.getSize().y + itemPadding) * i);
            }
            else
            {
                postPhotos[i].gameObject.SetActive(false);
            }
        }

        float itemSizeY = (postPhotos[0].rectTransform.getSize().y + Setting.ItemPadding) * post.Photos.Length - baseAnchoredPosition.y;

        if (itemSizeY < Setting.PostOverlayDefaultSize.y)
            itemSizeY = Setting.PostOverlayDefaultSize.y;

        Vector2 itemSize = new Vector2(Setting.PostOverlayDefaultSize.x, itemSizeY);

        contentRect.setSize(itemSize);

        // Event Trigger
        if(post.Trigger)
        {
            ScenarioManager.instance.TriggerCheck("Post", key);
        }
    }

    private void Close()
    {
        scrollbar.value = 1;
        this.gameObject.SetActive(false);
    }
}
