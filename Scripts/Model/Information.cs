using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum InformationType { Default, Event, Education, Career } //필요시 더 추가

[System.Serializable]
public class Information
{
    [SerializeField]
    private InformationType type;
    [SerializeField]
    private string text;

    public InformationType Type { get { return type; } }
    public string Text { get { return text; } }

    public Information(InformationType type, string text)
    {
        this.type = type;
        this.text = text;
    }

    public Information(string type, string text)
    {
        this.text = text;

        switch (type)
        {
            case "Default":
                this.type = InformationType.Default;
                break;
            case "Event":
                this.type = InformationType.Event;
                break;
            case "Education":
                this.type = InformationType.Education;
                break;
            case "Career":
                this.type = InformationType.Career;
                break;
            default:
                Debug.LogError("Wrong Information Type");
                break;
        }
    }
}
