using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using Debug = UnityEngine.Debug;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

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
        LoadPosts();
        LoadUsers();
        LoadAlarms();
        LoadMessages();
        LoadSecrets();
    }

    private static Dictionary<int, User> users = new Dictionary<int, User>();
    private static Dictionary<int, Post> posts = new Dictionary<int, Post>();
    private static Dictionary<int, Alarm> alarms = new Dictionary<int, Alarm>();
    private static Dictionary<int, Message> messages = new Dictionary<int, Message>();
    private static Dictionary<int, Secret> secrets = new Dictionary<int, Secret>();

    #region GetData
    public User GetUser(string name)
    {
        foreach (KeyValuePair<int, User> user in users)
        {
            if (user.Value.Name == name)
                return users[user.Key];
        }
        return null;
    }

    public User GetUser(int key)
    {
        if (users.ContainsKey(key))
            return users[key];
        else
        {
            NullPrimaryKeyLogError<User>(key);
            return null;
        }
    }

    public void SetUser(int key, string name, Sprite avatar)
    {
        users[key].Name = name;
        users[key].Avatar = avatar;
    }

    public Post GetPost(int key)
    {
        if (posts.ContainsKey(key))
            return posts[key];
        else
        {
            NullPrimaryKeyLogError<Post>(key);
            return null;
        }
    }

    public Alarm GetAlarm(int key)
    {
        if (alarms.ContainsKey(key))
            return alarms[key];
        else
        {
            NullPrimaryKeyLogError<Alarm>(key);
            return null;
        }
    }

    public Message GetMessage(int key)
    {
        if (messages.ContainsKey(key))
            return messages[key];
        else
        {
            NullPrimaryKeyLogError<Message>(key);
            return null;
        }
    }
    
    public Secret GetSecret(int key)
    {
        if (secrets.ContainsKey(key))
            return secrets[key];
        else
        {
            NullPrimaryKeyLogError<Secret>(key);
            return null;
        }
    }

    #endregion
    
    #region LoadData

    public void LoadPosts()
    {
        string filePath = Setting.Post_filepath;
        try
        {
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataAsJson);

                foreach (var post in (JArray)data["posts"])
                {
                    int userID = Convert.ToInt32(post["userID"]);
                    string text = Convert.ToString(post["text"]);
                    DateTime timestamp =
                        DateTime.ParseExact(Convert.ToString(post["timestamp"]), "yyyy-MM-dd HH:mm tt", null);
                    int primaryKey = Convert.ToInt32(post["primaryKey"]);
                    JArray photos_path = (JArray)post["photos"];
                    Sprite[] photos = new Sprite[photos_path.Count];

                    for (int i = 0; i < photos.Length; i++)
                    {
                        string photoPath = Path.Combine(Application.dataPath, "Resources/Post_Images/" + photos_path[i]);
                        if (File.Exists(photoPath))
                            photos[i] = IMG2Sprite.LoadNewSprite(photoPath);
                        else
                        {
                            Debug.LogError("Post photos doesnt exist." + photoPath);
                        }

                    }
                    AddPost(new Post(userID, text, timestamp, primaryKey, photos)); 
                }
                
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void LoadUsers()
    {
        string filePath = Setting.User_filepath;
        try
        {
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataAsJson);

                foreach (var user in (JArray) data["users"])
                {
                    int primaryKey = Convert.ToInt32(user["primaryKey"]);
                    string name = Convert.ToString(user["name"]);
                    string avatar_path = Path.Combine(Application.dataPath,
                        "Resources/User_Images/" + Convert.ToString(user["avatar"]));
                    Sprite avatar = IMG2Sprite.LoadNewSprite(avatar_path);
                    JArray informations_json = (JArray)user["informations"];
                    Information[] informations = new Information[informations_json.Count];
                    for (int i = 0; i < informations_json.Count; i++)
                    {
                        informations[i] = new Information((InformationType) Enum.Parse(typeof(InformationType), informations_json[i]["type"].ToString()), informations_json[i]["text"].ToString());
                    }

                    AddUser(new User(name, avatar, informations, primaryKey));
                    
                }

            }
            else
            {
                throw new FileNotFoundException();
            }

        }
        catch (KeyNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void LoadAlarms()
    {
        string filePath = Setting.Alarms_filepath;
        try
        {
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataAsJson);
                foreach (var alarm in (JArray)data["alarms"])
                {
                    int primaryKey = Convert.ToInt32(alarm["primaryKey"]);
                    AlarmType type = (AlarmType) Enum.Parse(typeof(AlarmType), alarm["type"].ToString());
                    string text = alarm["text"].ToString();
                    int linkID = Convert.ToInt32(alarm["linkID"]);
                    AddAlarm(new Alarm(type,linkID, text, primaryKey));
                }
                
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void LoadMessages()
    {
        string filePath = Setting.Messages_filepath;
        try
        {
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataAsJson);

                foreach (var message in (JArray) data["messages"])
                {
                    int primaryKey = Convert.ToInt32(message["primaryKey"]);
                    int userID = Convert.ToInt32(message["userID"]);
                    bool trigger =  (bool)message["trigger"];
                    List<MessageOption> options = new List<MessageOption>();
                    foreach (var option in (JArray) message["options"])
                    {
                        options.Add(new MessageOption(option["text"].ToString(),  Convert.ToInt32(option["nextID"])));
                    }
                    AddMessage(new Message(userID,options, primaryKey, trigger));
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void LoadSecrets(){
        string filePath = Setting.Secrets_filepath;
        try
        {
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataAsJson);
                Debug.Log(dataAsJson);
                foreach (var secret in (JArray) data["secrets"])
                {
                    int primaryKey = Convert.ToInt32(secret["primaryKey"]);
                    string problem_path = Path.Combine(Application.dataPath,
                        "Resources/Problem_Images/" + Convert.ToString(secret["problem"]));
                    Sprite problem = IMG2Sprite.LoadNewSprite(problem_path);

                    int[] buttonKeys = new int[12];
                    List<int> answer = new List<int>();
                
                    JArray buttonKeysJ = (JArray)secret["buttonKeys"];
                    for(int i=0; i < 12; i++)
                    {
                        buttonKeys[i] = Convert.ToInt32(buttonKeysJ[i]);
                    }

                    foreach(int answerNum in secret["answer"])
                    {
                        answer.Add(answerNum);
                    }

                    AddSecret(new Secret(primaryKey, problem, buttonKeys, answer.ToArray()));
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError(e.ToString());
        }
    }
    #endregion

    #region AddData
    private void AddUser(User user)
    {
        if (!(users.ContainsKey(user.PrimaryKey)))
            users.Add(user.PrimaryKey, user);
        else
        {
            DuplicatePrimaryKeyLogError<User>(user.PrimaryKey);
        }
    }

    private void AddPost(Post post)
    {
        if (!(posts.ContainsKey(post.PrimaryKey)))
            posts.Add(post.PrimaryKey, post);
        else
        {
            DuplicatePrimaryKeyLogError<Post>(post.PrimaryKey);
        }
    }

    private void AddAlarm(Alarm alarm)
    {
        if (!(alarms.ContainsKey(alarm.PrimaryKey)))
            alarms.Add(alarm.PrimaryKey, alarm);
        else
        {
            DuplicatePrimaryKeyLogError<Alarm>(alarm.PrimaryKey);
        }
    }

    private void AddMessage(Message message)
    {
        if (!(messages.ContainsKey(message.PrimaryKey)))
            messages.Add(message.PrimaryKey, message);
        else
        {
            DuplicatePrimaryKeyLogError<Message>(message.PrimaryKey);
        }
    }

    private void AddSecret(Secret secret)
    {
        if (!(secrets.ContainsKey(secret.PrimaryKey)))
            secrets.Add(secret.PrimaryKey, secret);
        else
        {
            DuplicatePrimaryKeyLogError<Secret>(secret.PrimaryKey);
        }

    }

    #endregion

    private void DuplicatePrimaryKeyLogError<T>(int key)
    {
        Debug.LogError("Key 중복으로 " + typeof(T).Name + "의 " + key + "번째 키 중복으로 추가 실패");
    }

    private void NullPrimaryKeyLogError<T>(int key)
    {
        Debug.LogError(typeof(T).Name + "의 키 값이 " + key + "인 값은 DB에 존재하지 않는다.");
    }
}
