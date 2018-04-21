using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Secret : Data
{
    // 문제 이미지
    [SerializeField]
    private Sprite problem;
    // 버튼 이미지 [12]
    [SerializeField]
    private int[] buttonKeys;
    // 정답 [4, 5, 6]
    [SerializeField]
    private int[] answer;
    // 문제 해결 여부
    [SerializeField]
    private bool solve;

    public Sprite Problem
    {
        get { return problem; }
    }

    public int[] ButtonKeys
    {
        get { return buttonKeys; }
    }

    public int[] Answer
    {
        get { return answer; }
    }

    public bool Solve
    {
        get { return solve; }
        set { solve = value; }
    }

    // 생성자
    public Secret(int primaryKey, Sprite problem, int[] buttonKeys, int[] answer) : base(primaryKey)
    {
        this.problem = problem;

        if (buttonKeys.Length != 12)
            Debug.LogError("buttonsKeys의 개수 설정 오류");
        else
            this.buttonKeys = buttonKeys;

        if (answer.Length < 4 || answer.Length > 6)
            Debug.LogError("answer의 길이 설정 오류");
        else
            this.answer = answer;

        solve = false;

    }
}
