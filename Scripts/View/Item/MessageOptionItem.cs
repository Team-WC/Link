using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageOptionItem : UIBehaviour, IViewItem
{
    public Text messageOption1;
    public Text messageOption2;

    protected override void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void OnUpdateItem(int key)
    {
        Message message = DataManager.instance.GetMessage(key);

        messageOption1.text = message.Options[1].Text;
        messageOption2.text = message.Options[2].Text;
    }

    public void MessageOption1()
    {
        ScenarioManager.instance.messageOptionNumber = 1;
        this.gameObject.SetActive(false);
    }

    public void MessageOption2()
    {
        ScenarioManager.instance.messageOptionNumber = 2;
        this.gameObject.SetActive(false);
    }
}
