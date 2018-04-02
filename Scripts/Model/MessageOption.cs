using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MessageOption
{
    [SerializeField]
    private string text;
    [SerializeField]
    private int nextID;

    public string Text { get { return text; } }
    public int NextID { get { return nextID; } }

    public MessageOption(string text, int nextID)
    {
        this.text = text;
        this.nextID = nextID;
    }
}
