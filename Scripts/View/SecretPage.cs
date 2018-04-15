using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class SecretPage : UIBehaviour, IViewItem
{
    public Sprite[] buttonImages;

    public GameObject keyboardObj;
    public GameObject answerSheet4Obj;
    public GameObject answerSheet5Obj;
    public GameObject answerSheet6Obj;

    private Image[] button;
    private Image[] sheet;
    private int sheetNum;

    private int[] answer;
    private int[] problemKeys;

    private int[] select;

    protected override void Awake()
    {
        this.gameObject.SetActive(false);

        button = keyboardObj.GetComponentsInChildren<Image>();
    }

    public void OnUpdateItem(int key)
    {
        // key는 문제 번호
        // 임시 문제
        problemKeys = new int[] { 11, 1, 2, 3, 5, 4, 7, 8, 9, 10, 0, 6 };
        answer = new int[] { 1, 2, 3, 4 };

        sheetNum = 0;
        if (answer.Length == 4)
        {
            answerSheet4Obj.SetActive(true);
            answerSheet5Obj.SetActive(false);
            answerSheet6Obj.SetActive(false);
            sheet = answerSheet4Obj.GetComponentsInChildren<Image>();
            select = new int[4];
        }
        else if (answer.Length == 5)
        {
            answerSheet4Obj.SetActive(false);
            answerSheet5Obj.SetActive(true);
            answerSheet6Obj.SetActive(false);
            sheet = answerSheet5Obj.GetComponentsInChildren<Image>();
            select = new int[5];
        }
        else if (answer.Length == 6)
        {
            answerSheet4Obj.SetActive(false);
            answerSheet5Obj.SetActive(false);
            answerSheet6Obj.SetActive(true);
            sheet = answerSheet6Obj.GetComponentsInChildren<Image>();
            select = new int[6];
        }

        OnUpdateKeyboard(problemKeys);
    }

    public void OnClickButton(int key)
    {
        for (int i = 0; i < sheet.Length; i++)
        {
            if (sheet[i].sprite == null)
            {
                sheet[i].sprite = buttonImages[problemKeys[key]];
                break;
            }
        }
        select[sheetNum] = key;
        sheetNum++;

        if (sheetNum == sheet.Length)
        {
            StartCoroutine(AnswerCheck());

            sheetNum = 0;
        }
    }

    public IEnumerator AnswerCheck()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < answer.Length; i++)
        {
            if (answer[i] != select[i])
            {
                Debug.Log("오답");
                break;
            }

            if (i == answer.Length - 1)
            {
                Debug.Log("정답");
            }
        }

        ClearAnswerSheet();
    }

    public void OnUpdateKeyboard(int[] problemKeys)
    {
        for (int i = 0; i < button.Length; i++)
        {
            button[i].sprite = buttonImages[problemKeys[i]];
        }
    }

    public void ClearAnswerSheet()
    {
        for (int i = 0; i < sheet.Length; i++)
        {
            sheet[i].sprite = null;
        }
    }
}
