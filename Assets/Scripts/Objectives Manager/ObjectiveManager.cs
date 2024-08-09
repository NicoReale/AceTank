using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public ObjectiveHandler handler;
    
    public PointerBehaviour pointerBehaviour;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Desc;

    public int FirstLevelObjective;

    Objective currentObj;

    ObjectiveUiHandler uiHandler;

    Dictionary<int, Objective> objectives = new Dictionary<int, Objective>();


    private void Awake()
    {
        uiHandler = new ObjectiveUiHandler(Name, Desc);
    }
    void Start()
    {

        addObj();
        SetCurrentObjective(FirstLevelObjective);

    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            SetCurrentObjective(1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            SetCurrentObjective(0);
        }
    }
    void addObj()
    {
        handler.objective.ForEach(s => objectives.Add(s.id, s));
    }

    public void SetCurrentObjective(int objectiveId)
    {
        if (!objectives.TryGetValue(objectiveId, out Objective objective))
        {
            return;
        }

        if (currentObj != null)
        {
            currentObj.OnObjectiveUpdate(false);
        }

        currentObj = objective;
        currentObj.OnObjectiveUpdate(true);
        pointerBehaviour.SetPointerObjective(currentObj.objectivePos);
        UpdateUI(currentObj);
    }
    private void UpdateUI(Objective objective)
    {
        uiHandler.UIUpdate(objective.Name, objective.Description);
    }

    public Objective getCurrentObjective()
    {
        return currentObj;
    }


}
