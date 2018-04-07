using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public static Setting instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // View Setting
    // AP: anchoredPosition
    // anchorMin (0.5, 0.5)
    // anchorMax (0.5, 0.5)
    // pivot (0.5, 0.5)
    public static readonly Vector2 AspectRatio = new Vector2(9, 16);
    public static readonly Vector2 ScreenSize = new Vector2(AspectRatio.x * 80, AspectRatio.y * 80); // 720x1280

    public static readonly Vector2 StatusBarSize = new Vector2(ScreenSize.x, ScreenSize.y * 0.0375f);
    public static readonly Vector2 StatusBarAP = new Vector2(0, 616);

    // Navigation Bar
    public static readonly Vector2 NavigationBarSize = new Vector2(ScreenSize.x, ScreenSize.y * 0.1625f);
    public static readonly Vector2 NavigationBarAP = new Vector2(0, 488);
    public static readonly Vector2 PostsButtonSize = new Vector2(NavigationBarSize.x * 0.25f, NavigationBarSize.y * 0.5f);
    public static readonly Vector2 PostsButtonAP = new Vector2(-270, -52);
    public static readonly Vector2 UsersButtonSize = new Vector2(NavigationBarSize.x * 0.25f, NavigationBarSize.y * 0.5f);
    public static readonly Vector2 UsersButtonAP = new Vector2(-90, -52);
    public static readonly Vector2 AlarmsButtonSize = new Vector2(NavigationBarSize.x * 0.25f, NavigationBarSize.y * 0.5f);
    public static readonly Vector2 AlarmsButtonAP = new Vector2(90, -52);
    public static readonly Vector2 MessagesButtonSize = new Vector2(NavigationBarSize.x * 0.25f, NavigationBarSize.y * 0.5f);
    public static readonly Vector2 MessagesButtonAP = new Vector2(270, -52);

    // Timeline
    public static readonly Vector2 TimelineSize = new Vector2(ScreenSize.x, ScreenSize.y * 0.8f);
    public static readonly Vector2 TimelineAP = new Vector2(0, -128);

    // Item
    public static readonly float ItemPadding = 20f;
    public static readonly Vector2 PostItmeDefaultSize = new Vector2(ScreenSize.x, ItemPadding * 9);
    public static readonly Vector2 PostPageDefaultSize = new Vector2(ScreenSize.x, ScreenSize.y * 0.875f);

    public static readonly int PhotoMaxCount = 4;
    public static readonly int InformationMaxCount = 3;

    public static string Post_filepath
    {get{ return Path.Combine(Application.dataPath, "Data/Posts.json");}}

    public static string User_filepath
    { get { return Path.Combine(Application.dataPath, "Data/Users.json");}}

    public static string Alarms_filepath
    { get {return Path.Combine(Application.dataPath, "Data/Alarms.json");}}

    public static string Messages_filepath
    {get {return Path.Combine(Application.dataPath, "Data/Messages.json");}}
}
