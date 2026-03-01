using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (OnClicked != null)
            {
                OnClicked();
            }
        }
    }
}