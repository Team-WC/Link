using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using Debug = UnityEngine.Debug;
using Newtonsoft.Json;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager instance = null;
    public int messageOptionNumber = -1;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }

    public PlayerProgress playerProgress = new PlayerProgress();
    public ListViewController postsView;
    public ListViewController usersView;
    public ListViewController alarmsView;
    public ListViewController messagesView;

    #region Test
    private int test = 0;
    public int Test
    {
        get { return test; }
        set
        {
            test = value;

            if (test == 1)
            {
                AddUsers(new int[] { 0, 1});
                AddPosts(new int[] { 0, 1});
                AddAlarm(0);
                AddMessageAuto(1);
            }
            else if (test == 2)
            {
                
            }
            else if (test == 3)
            {
               
            }
            else if (test == 4)
            {
              
            }
            else if (test == 5)
            {
               
            }
            else if (test == 6)
            {
            }
            Canvas.ForceUpdateCanvases();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Test++;
            Debug.Log("test num: " + Test);
        }
    }
    #endregion

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

    public void LoadView()
    {
        postsView.DisplayItems(playerProgress.PostsList.ToArray());
        usersView.DisplayItems(playerProgress.UsersList.ToArray());
        alarmsView.DisplayItems(playerProgress.AlarmsList.ToArray());
        // 메시지 추가 필요
    }

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

    public void AddUsers(int[] keys)
    {
        playerProgress.AddUsersKey(keys);
        usersView.DisplayItems(keys);
    }

    public void AddUser(int key)
    {
        playerProgress.AddUserKey(key);
        postsView.DisplayItem(key);
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

        while (currentID != destinationID)
        {
            if (currentID > destinationID)
            {
                Debug.Log("Out of range message id");
            }

            Message message = DataManager.instance.GetMessage(currentID);

            yield return new WaitForSeconds(1.5f);

            playerProgress.AddMessageKey(currentID);
            messagesView.DisplayItem(currentID);

            // 선택지가 없는 경우
            if (message.Options.Count == 1)
            {
                messageOptionNumber = 0;
            }
            // 선택지가 있는 경우
            else if(message.Options.Count == 3)
            {
                messageOptionNumber = -1;
                while (messageOptionNumber == -1)
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                Debug.LogError("선택지 개수 초과: " + message.Options.Count);
            }

            currentID = message.Options[messageOptionNumber].NextID;
        }

        playerProgress.MessageCurrentID = currentID;
    }

    public void TriggerCheck(string type, int key)
    {
        if(type.Equals("Post"))
        {
            Debug.Log("Post Overlay Event (key: " + key + ")");
        }
        else if(type.Equals("Message"))
        {
            Debug.Log("Message Event (key: " + key + ")");
        }
        else if(type.Equals("Password"))
        {
            Debug.Log("Password Event (key: " + key + ")");
        }
        else
        {
            Debug.LogError("Trigger type input error: " + type);
        }
    }
}
