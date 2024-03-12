using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float timeBeforeLaunch;
    public Rigidbody2D rb;
    public float uniformSpeed;
    public float timer = 0.0f;

    private Vector2 velocity;
    private bool isFlying;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeBeforeLaunch)
        {
            Launch();
        }
    }

    private void Launch()
    {
        isFlying = true;
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        rb.velocity = direction * uniformSpeed;
    }

    //pause behavior
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
        if(isFlying && newGameState == GameState.Paused)
        {
            Freeze();
        }
        if(isFlying && newGameState == GameState.Gameplay)
        {
            UnFreeze();
        }

        enabled = newGameState == GameState.Gameplay;
    }

    private void Freeze()
    {
        velocity = rb.velocity;
        rb.velocity = Vector2.zero;
    }

    private void UnFreeze()
    {
        rb.velocity = velocity;
    }


}
