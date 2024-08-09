using UnityEngine;


public class PopUp : MonoBehaviour
{
    public GameObject popUpPrefab;

    public PopUpWindow ShowPopUp()
    {
        GameObject popUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);

        popUp.transform.SetParent(GameObject.Find("PopUpCanvas").transform, false);
        RectTransform rectTransform = popUp.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;

        return popUp.GetComponent<PopUpWindow>();
    }
}