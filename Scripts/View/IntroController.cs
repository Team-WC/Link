using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroController : UIBehaviour
{
    public GameObject[] introPage;
    public int currentPage = 0;

    public Sprite playerAvatar;
    public string playerName = "       ";
    public InputField inputField;

    protected override void Start()
    {
        ChangePage(currentPage);

        inputField = GameObject.Find("InputField").GetComponent<InputField>();
    }

    public void NextPage()
    {
        currentPage++;
        ChangePage(currentPage);
    }

    public void PrevPage()
    {
        currentPage--;
        ChangePage(currentPage);
    }

    public void EndIntro()
    {
        ScenarioManager.instance.playerProgress.intro = true;

        playerName = inputField.text;
        if (playerName.Equals(""))
            playerName = "       ";

        // user avatar, user name
        DataManager.instance.SetUser(0, playerName, playerAvatar);

        this.gameObject.SetActive(false);
    }

    public void ChangePage(int page)
    {
        introPage[page].transform.SetAsLastSibling();
    }
}
