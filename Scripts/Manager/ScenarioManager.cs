using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using Debug = UnityEngine.Debug;
using Newtonsoft.Json;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager instance = null;
    public int messageOptionNumber = -1;

    

    public PlayerProgress playerProgress = new PlayerProgress();
    public ListViewController postsView;
    public ListViewController usersView;
    public ListViewController alarmsView;
    public ListViewController messagesView;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (playerProgress.intro == true)
        {
            GameObject.Find("Intro").SetActive(false);
        }
    }
    public void LoadView()
    {
        postsView.DisplayItems(playerProgress.PostsList.ToArray());
        usersView.DisplayItems(playerProgress.UsersList.ToArray());
        alarmsView.DisplayItems(playerProgress.AlarmsList.ToArray());
        messagesView.DisplayItems(playerProgress.MessagesList.ToArray());
    }

    #region PlayerProgress
    public static string ProgressFilePath
    {
        get
        {
            return Path.Combine(Application.dataPath, "Resources/player_Progress.json");
        }
    }

    private void LoadPlayerProgress()
    {
        if (File.Exists(ProgressFilePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(ProgressFilePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            playerProgress = JsonConvert.DeserializeObject<PlayerProgress>(dataAsJson);

            LoadView();
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    private void SavePlayerProgress()
    {
        string dataAsJson = JsonConvert.SerializeObject(playerProgress, Formatting.Indented);
        File.WriteAllText(ProgressFilePath, dataAsJson);
    }

    #endregion

    #region Add

    public void AddPosts(int[] keys)
    {
        playerProgress.AddPostsKey(keys);
        postsView.DisplayItems(keys);

        for (int i = 0; i < keys.Length; i++)
        {
            playerProgress.AddUserPostKey(DataManager.instance.GetPost(keys[i]).UserID, keys[i]);
        }
    }

    public void AddPost(int key)
    {
        playerProgress.AddPostKey(key);
        postsView.DisplayItem(key);

        playerProgress.AddUserPostKey(DataManager.instance.GetPost(key).UserID, key);
    }

    public void AddUserPosts(int[] keys)
    {
        for(int i = 0; i < keys.Length; i++)
        {
            playerProgress.AddUserPostKey(DataManager.instance.GetPost(keys[i]).UserID, keys[i]);
        }
    }

    public void AddUserPost(int key)
    {
        playerProgress.AddUserPostKey(DataManager.instance.GetPost(key).UserID, key);
    }

    public void AddUsers(int[] keys)
    {
        playerProgress.AddUsersKey(keys);
        usersView.DisplayItems(keys);
    }

    public void AddUser(int key)
    {
        playerProgress.AddUserKey(key);
        usersView.DisplayItem(key);
    }

    public void AddAlarm(int key)
    {
        playerProgress.AddAlarmKey(key);
        alarmsView.DisplayItem(key);
    }

    public void ClearAll()
    {
        playerProgress.ClearUsersKey();
        playerProgress.ClearPostsKey();
        playerProgress.ClearAlarmsKey();

        usersView.ListClear();
        postsView.ListClear();
        alarmsView.ListClear();
    }

    public void AddMessageAuto(int destinationID)
    {
        StartCoroutine(AddMessageAutoCo(destinationID));
    }

    private IEnumerator AddMessageAutoCo(int destinationID)
    {
        int currentID = playerProgress.MessageCurrentID;

        while (currentID <= destinationID)
        {
            if (currentID > destinationID)
            {
                Debug.Log("Out of range message id");
            }

            Message message = DataManager.instance.GetMessage(currentID);

            yield return new WaitForSeconds(1.5f);

            playerProgress.AddMessageKey(currentID);
            messagesView.DisplayItem(currentID);

            messageOptionNumber = 0;

            // count = 1, 선택지 없음
            if (message.Options.Count == 1)
            {
                Debug.Log("Normal Message: " + message.PrimaryKey.ToString());
            }
            // count = 2, 시크릿 페이지 호출
            // count = 3, 선택지 있음
            else if (message.Options.Count == 2 || message.Options.Count == 3)
            {
                messageOptionNumber = -1;
                while (messageOptionNumber == -1)
                {
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else
            {
                Debug.LogError("선택지 설정 오류: " + message.Options.Count);
            }

            currentID = message.Options[messageOptionNumber].NextID;
        }

        //current ID가 next를 미리 가르키고 있음 정상 작동은 하는데 current ID랑 의미가 맞는지 확인 필요 
        playerProgress.MessageCurrentID = currentID;
    }

    #endregion
    
    #region Trigger
    public void MainTrigger(int stage)
    {
        ScenarioManager.instance.playerProgress.Stage = stage;
        if (stage == 0)
        {
            // 초기 뷰 설정
            AddUsers(new int[] { 1, 2, 3, 4, 5 });
            AddPosts(new int[] { 0, 1, 2, 3, 4, 5});
            AddMessageAuto(5);
        }
        else if (stage == 1)
        {

        }
        else
        {
            Debug.LogError("MainTrigger stage 변수 오류");
        }
    }

    public void SubTrigger<T>(int key)
    {
        if (ScenarioManager.instance.playerProgress.Stage == 0)
        {
            SubTriggerStage0<T>(key);
        }
        else if (ScenarioManager.instance.playerProgress.Stage == 1)
        {
            SubTriggerStage1<T>(key);
        }
    }

    public void SubTriggerStage0<T>(int key)
    {
        // type: Message, Post
        /* 트리거 예제
        if(typeof(T).Name.Equals(typeof(Message).Name) && key == 0)
        {
            // post view 호출
        }
        

        //else
        {
            Debug.LogError("Event Trigger 설정 오류");
        }
        */
    }

    public void SubTriggerStage1<T>(int key)
    {

    }

    #endregion
    
}
