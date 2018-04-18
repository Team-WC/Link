using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroController : UIBehaviour
{
    public GameObject[] introPage;
    public int currentPage = 0;

    protected override void Start()
    {
        ChangePage(currentPage);
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

        this.gameObject.SetActive(false);
	}

    public void ChangePage(int page)
    {
		if(page < 0 || introPage.Length <= page)
		{
			Debug.LogError("Out of range intro page");
		}

        for (int i = 0; i < introPage.Length; i++)
        {
            if (i == page)
            {
                introPage[i].SetActive(true);
            }
            else
            {
				introPage[i].SetActive(false);
            }
        }
    }
}
