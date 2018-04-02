using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using UnityEngine;

[System.Serializable]
public class Post : Data
{
    [SerializeField]
    private int userID;
    [SerializeField]
    private string text;
    [SerializeField]
    private DateTime timestamp;
    [SerializeField]
    private Sprite[] photos;
    //video 추가 예정

    [SerializeField]
    private bool trigger;

    public int UserID
    {
        get { return userID; }
    }

    public string Text
    {
        get { return text; }
    }

    public Sprite[] Photos
    {
        get { return photos; }
        private set { photos = value; }
    }

    public DateTime Timestamp
    {
        get { return timestamp; }
    }

    public bool HasPhoto()
    {
        if (photos != null && photos.Length > 0)
            return true;
        return false;
    }

    public int PhotoCount
    {
        get
        {
            if (HasPhoto())
                return photos.Length;
            return 0;
        }
    }

    public bool Trigger
    {
        get { return trigger; }
    }

    public Post(int userID, string text, DateTime timestamp, int primaryKey, Sprite[] photos = null) : base(primaryKey)
    {
        this.userID = userID;
        this.text = text;
        this.timestamp = timestamp;
        this.photos = photos;
    }
}
