using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroController : UIBehaviour
{
    public GameObject[] introPage;
    public int currentPage = 0;

    public int playerAvatarNumber = 0;
    public string playerName = "Player";
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

        // user avatar, user name  
        Debug.Log(playerAvatarNumber + " " + playerName);

        this.gameObject.SetActive(false);
    }

    public void ChangePage(int page)
    {
        introPage[page].transform.SetAsLastSibling();
    }
}
