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
        this.options = options;
        this.trigger = trigger;
    }
}
