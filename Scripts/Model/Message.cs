using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message : Data
{
    [SerializeField]
    private int userID;
    [SerializeField]
    List<MessageOption> options;
    
    [SerializeField]
    private bool trigger;

    public int UserID
    {
        get { return userID; }
    }

    public List<MessageOption> Options
    {
        get { return options; }
    }

    public bool Trigger
    {
        get { return trigger; }
    }

    public Message(int userID, List<MessageOption> options, int primaryKey, bool trigger) : base(primaryKey)
    {
        this.userID = userID;
        foreach (MessageOption option in options)
        {
            if (this.PrimaryKey >= option.NextID)
            {
                Debug.LogError("Message Next ID 는 메세지 key 보다 더 커야됩니다. (" + primaryKey.ToString() + ")");
            }
        }
        this.options = options;
        this.trigger = trigger;
    }
}
