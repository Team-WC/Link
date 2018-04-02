using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    [SerializeField]
    private int primaryKey; //기본키
    public int PrimaryKey { get { return primaryKey; } }

    protected Data(int primaryKey = 0)
    {
        this.primaryKey = primaryKey;
    }
}
