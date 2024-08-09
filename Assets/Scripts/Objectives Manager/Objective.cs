using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective", menuName = "Custom/Create Objective", order = 1)]
public class Objective : ScriptableObject
{
    public string Name;
    public string Description;
    public bool isCurrent;

    public Vector3 objectivePos;

    public int id;


    public void OnObjectiveUpdate(bool flag)
    {
        isCurrent = flag;
    }
}
