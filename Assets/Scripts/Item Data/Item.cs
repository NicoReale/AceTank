
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "CustomScriptable/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public int cost;
    public int id;
    public int amount;
    public float modifier = 3;

}
