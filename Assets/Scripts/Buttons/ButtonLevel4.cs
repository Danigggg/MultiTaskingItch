using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonLevel4 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public UnityEvent up;
    public UnityEvent down;
    public void OnPointerDown(PointerEventData eventData)
    {
        up.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        down.Invoke();
    }

}
