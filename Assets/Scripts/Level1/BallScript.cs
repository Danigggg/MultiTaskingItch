using UnityEngine;
using UnityEngine.Events;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D ball;
    public UnityEvent deathInvoker;
    public float spawnOffset;
    private Vector2 velocity;

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
        if(newGameState == GameState.Paused)
        {
            disableRigidBody();
        }

        if(newGameState == GameState.Gameplay)
        {
            enableRigidBody();
        }
        
    }

    private void disableRigidBody()
    {
        velocity = ball.velocity;
        ball.isKinematic = true;
        ball.velocity = Vector2.zero;
    }

    private void enableRigidBody()
    {
        ball.isKinematic = false;
        ball.velocity = velocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("WallDivider"))
        {
            deathInvoker.Invoke();
        }
    }
}
