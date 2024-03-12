using UnityEngine;

public class ObstacleLevel4Script : MonoBehaviour
{
    public Rigidbody2D obstacleRB;
    private Vector2 velocity;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WallDivider"))
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
        velocity = obstacleRB.velocity;
        obstacleRB.velocity = Vector2.zero;
    }

    private void enableRigidBody()
    {
        obstacleRB.velocity = velocity;
    }
}
