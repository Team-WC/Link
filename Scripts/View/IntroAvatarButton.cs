using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroAvatarButton : UIBehaviour
{
    private IntroController introController;

    private Button[] avatarButton;
    private ColorBlock nColor;
    private ColorBlock pColor;

    protected override void Awake()
    {
        introController = GameObject.Find("Intro").GetComponent<IntroController>();
        avatarButton = GetComponentsInChildren<Button>();

        nColor = avatarButton[0].colors;
        pColor = nColor;
        pColor.normalColor = pColor.pressedColor;
        pColor.highlightedColor = pColor.pressedColor;
        avatarButton[0].colors = pColor;
    }

    public void OnPressedButton(int num)
    {
        for (int i = 0; i < avatarButton.Length; i++)
        {
            if (i == num)
            {
                avatarButton[i].colors = pColor;
                introController.playerAvatar = avatarButton[num].GetComponent<Image>().sprite;
            }
            else
            {
                avatarButton[i].colors = nColor;
            }
        }
    }
}
