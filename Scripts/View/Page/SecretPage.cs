using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class SecretPage : UIBehaviour, IViewItem
{
    public Sprite[] buttonImages;

    public Button closeButton;

    public Image problem;
    public GameObject keyboardObj;
    public GameObject answerSheet4Obj;
    public GameObject answerSheet5Obj;
    public GameObject answerSheet6Obj;

    private Image[] button;
    private Image[] sheet;
    private int sheetNum;

    private int[] answer;
    private int[] buttonKeys;

    private int[] select;

    private int secretKey;

    protected override void Awake()
    {
        this.gameObject.SetActive(false);

        button = keyboardObj.GetComponentsInChildren<Image>();
        closeButton.GetComponent<Button>().onClick.AddListener(() => { Close(); });
    }

    public void OnUpdateItem(int key)
    {
        secretKey = key;
        Secret secret = DataManager.instance.GetSecret(key);
        buttonKeys = secret.ButtonKeys;
        answer = secret.Answer;
        problem.sprite = secret.Problem;

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

        OnUpdateKeyboard(buttonKeys);
    }

    public void OnClickButton(int num)
    {
        for (int i = 0; i < sheet.Length; i++)
        {
            if (sheet[i].sprite == null)
            {
                sheet[i].sprite = buttonImages[buttonKeys[num]];
                break;
            }
        }
        select[sheetNum] = num;
        sheetNum++;

        if (sheetNum == sheet.Length)
        {
            // 키 수정
            StartCoroutine(AnswerCheck(secretKey));

            sheetNum = 0;
        }
    }

    public IEnumerator AnswerCheck(int key)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < answer.Length; i++)
        {
            if (answer[i] != select[i])
            {
                Debug.Log("오답");
                ClearAnswerSheet();
                break;
            }

            if (i == answer.Length - 1)
            {
                Debug.Log("정답");
                DataManager.instance.GetSecret(key).Solve = true;
                ScenarioManager.instance.messageOptionNumber = 0;
                this.gameObject.SetActive(false);
            }
        }
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

    private void Close()
    {
        this.gameObject.SetActive(false);
    }
}
