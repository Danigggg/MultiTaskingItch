using UnityEngine;
using UnityEngine.Events;

public class EdgeCollider : MonoBehaviour
{
    public UnityEvent deathInvoker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        deathInvoker.Invoke();
    }
}
