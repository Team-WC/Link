using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class User : Data
{
    [SerializeField]
    private string name;
    [SerializeField]
    private Sprite avatar;
    [SerializeField]
    private Information[] informations;

    public string Name { get { return name; } set { name = value; } }

    public Sprite Avatar
    {
        get { return avatar; }
        set { avatar = value; }
    }

    public Information[] Informations
    {
        get { return informations; }
        set { informations = value; }
    }

    public User(string name, Sprite avatar, Information[] informations, int primaryKey) : base(primaryKey)
    {
        this.name = name;
        this.avatar = avatar;
        this.informations = informations;
    }
}
