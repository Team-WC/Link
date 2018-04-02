using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UserOverlay : UIBehaviour, IViewItem
{
    public Button closeButton;

    public Image userAvatar;
    public Text userName;

    public Sprite[] informationTypeImages;
    public Image[] informationType;
    public Text[] informationText;

    public GameObject overlay;

    public Scrollbar scrollbar;
    ListViewController listViewController;

    private int informationMaxCount = Setting.InformationMaxCount;

    protected override void Awake()
    {
        listViewController = overlay.GetComponent<ListViewController>();
        this.gameObject.SetActive(false);
        closeButton.GetComponent<Button>().onClick.AddListener(() => { Close(); });
    }

    public void OnUpdateItem(int key)
    {
        User user = DataManager.instance.GetUser(key);

        this.userAvatar.sprite = user.Avatar;
        this.userName.text = user.Name;

        Information[] informations = user.Informations;

        for (int i = 0; i < informationMaxCount; i++)
        {
            informationType[i].sprite = informationTypeImages[(int)informations[i].Type];
            informationText[i].text = informations[i].Text;
        }

        // 자신이 쓴 post 호출
        listViewController.ListClear();
        listViewController.InsertSpace(-listViewController.itemPrototype.anchoredPosition.y);

        int[] userPosts = ScenarioManager.instance.playerProgress.GetUserPosts(key).ToArray();

        listViewController.DisplayItems(userPosts);
    }

    private void Close()
    {
        scrollbar.value = 1;
        this.gameObject.SetActive(false);
    }
}
