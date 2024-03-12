using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonLevel1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent moveFunc;
    private bool pass;
    public void OnPointerDown(PointerEventData eventData)
    {
        pass = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pass = false;
    }

    void Update()
    {
        if (pass)
        {
            moveFunc.Invoke();
        }    
    }
}
