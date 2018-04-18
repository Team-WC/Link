using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public struct Progress {
    public int stage;
    public int phase;

    public String ToString()
    {
        return stage.ToString() + " - " + phase.ToString();
    }
}

public class PlayerProgress
{
    //스테이지 진행 정도 ex) 1-1, 3-2
    [JsonProperty]
    private Progress progress;
    [JsonProperty]
    private List<int> usersList;
    [JsonProperty]
    public Dictionary<int, List<int>> userPost;
    [JsonProperty]
    private List<int> postsList;
    [JsonProperty]
    private List<int> alarmsList;
    [JsonProperty]
    private List<int> messagesList;
    [JsonProperty]
    private int messageCurrentID = 0;

    public Progress Progress {
        get { return progress;}
    }
    
    public List<int> UsersList
    {
        get { return new List<int>(usersList); }
    }

    public List<int> PostsList
    {
        get { return new List<int>(postsList); }
    }

    public List<int> AlarmsList
    {
        get { return new List<int>(alarmsList); }
    }

    public List<int> MessagesList
    {
        get { return new List<int>(messagesList); }
    }

    public int MessageCurrentID
    {
        get { return messageCurrentID; }
        set { messageCurrentID = value; }
    }

    public List<int> GetUserPosts(int user_key)
    {
        if (userPost.ContainsKey(user_key))
            return new List<int>(userPost[user_key]);
        else
        {
            throw (new NullDataException("[NullDataException] PlayerProgress: " + progress.ToString() + " : User[" + user_key.ToString() + "] is NULL."));
        }
    }

    public PlayerProgress()
    {
        progress.stage = 0;
        progress.phase = 0;
        usersList = new List<int>();
        postsList = new List<int>();
        userPost = new Dictionary<int, List<int>>();
        alarmsList = new List<int>();
        messagesList = new List<int>();
    }

    public void AddUsersKey(int[] user_keys)
    {
        foreach (int user_key in user_keys)
        {
            usersList.Add(user_key);
        }
    }

    public void AddUserKey(int user_key)
    {
        usersList.Add(user_key);
    }

    public void ClearUsersKey()
    {
        usersList.Clear();
    }

    public void AddPostsKey(int[] posts)
    {
        foreach (int post_key in posts)
        {
            postsList.Add(post_key);
        }
    }

    public void AddPostKey(int post_key)
    {
        postsList.Add(post_key);
    }

    public void AddUserPostKey(int user_key, int post_key)
    {
        if (userPost.ContainsKey(user_key))
        {
            if (userPost[user_key].Contains(post_key))
                throw (new DuplicateDataException("[DuplicateDataException]PlayerProgress " + progress.ToString() + " : User[" + user_key.ToString() + "] already has the Post[" + post_key.ToString() + "]."));
            else
                userPost[user_key].Add(post_key);
        }
        else
        {
            List<int> posts = new List<int> { post_key };
            userPost.Add(user_key, posts);
        }
    }

    public void ClearPostsKey()
    {
        postsList.Clear();
    }

    public void AddAlarmKey(int alarm_key)
    {
        alarmsList.Add(alarm_key);
    }

    public void ClearAlarmsKey()
    {
        alarmsList.Clear();
    }

    public void AddMessageKey(int message_key)
    {
        messagesList.Add(message_key);
    }
}

public class DuplicateDataException : Exception
{
    public DuplicateDataException(string message) : base(message) { }
}

public class NullDataException : Exception
{
    public NullDataException(string message) : base(message) { }
}
