using UnityEngine;

public class ProjecetileBehaviour : MonoBehaviour
{

    public Rigidbody2D rb;
    private Vector2 velocity;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("WallDivider"))
        {
            Destroy(this.gameObject);
        }
    }

    //pause behaviour
    private void Start()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Paused)
        {
            disableRigidBody();
        }

        if (newGameState == GameState.Gameplay)
        {
            enableRigidBody();
        }

    }

    private void disableRigidBody()
    {
        velocity = rb.velocity;
        rb.velocity = Vector2.zero;
    }

    private void enableRigidBody()
    {
        rb.velocity = velocity;
    }
}
