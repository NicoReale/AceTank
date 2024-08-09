using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveUiHandler
{
    TextMeshProUGUI Name;
    TextMeshProUGUI Desc;
    public string ObjectiveName;
    public string ObjectiveDescription;

    public ObjectiveUiHandler(TextMeshProUGUI UIName, TextMeshProUGUI UIDesc)
    {
        Name = UIName;
        Desc = UIDesc;
    }

    public void UIUpdate(string Name, string Desc)
    {
        this.Name.SetText(Name);
        this.Desc.SetText(Desc);

    }
}
