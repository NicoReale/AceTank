using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveBehaviour : MonoBehaviour
{
    public ObjectiveManager manager;
    public int nextLevel = 2;
    public bool changeLevel;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerBehaviour>() != null)
        {
            if(changeLevel)
            {
                ScenesManager.Instance.StartLoadScene(nextLevel);
                GameManager.instance.AddLevelFlag(GameManager.instance.levelsFlags + 1);
            }
            manager.SetCurrentObjective(manager.getCurrentObjective().id + 1);
            GameManager.instance.AddCurr(12);
            if(GameManager.instance.saveData != null)
                GameManager.instance.SaveGame();
            Destroy(gameObject);
        }
    }
}
