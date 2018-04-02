using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlarmType { Post, Information }

[System.Serializable]
public class Alarm : Data
{
    [SerializeField]
    private AlarmType type; // 알람 종류
    [SerializeField]
    private string text; // 알람
    [SerializeField]
    private int linkID; // 연결된 post의 id, information일 경우 user id를 통한 프로필 연결

    public AlarmType Type { get { return type; } }
    public string Text { get { return text; } }
    public int LinkID { get { return linkID; } }

    //c# 에선 부모 생성자 base()
    public Alarm(AlarmType type, int linkID, string text, int primaryKey) : base(primaryKey)
    {
        this.type = type;
        this.text = text;
        this.linkID = linkID;
    }
}
