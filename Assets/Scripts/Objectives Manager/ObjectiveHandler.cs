using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHandler : MonoBehaviour
{
    public List<Objective> objective;
    public List<GameObject> objectivePositions;
    
    void Awake()
    {
        for (int i = 0; i < objective.Count; i++)
        {
            objective[i].objectivePos = objectivePositions[i].transform.position;
        }
    }

}
