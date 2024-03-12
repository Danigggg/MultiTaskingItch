using UnityEngine;
using UnityEngine.Events;

public class PlayerLevel4 : MonoBehaviour
{
    public float grav;
    public Rigidbody2D rb;
    public SpriteRenderer background4;
    public SpriteRenderer playerSprite;
    public UnityEvent deathInvoker;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetGravity(-grav);
        }
        if(Input.GetKeyUp(KeyCode.Space))        
        {
            SetGravity(grav);
        }

        float minY = background4.bounds.min.y + playerSprite.size.y / 2.0f;
        float maxY = background4.bounds.max.y - playerSprite.size.y / 2.0f;

        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, 0.0f);
    }

    public void SetGravity(float gravity)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = gravity;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Death"))
            deathInvoker.Invoke();
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
        if(newGameState == GameState.Paused)
        {
            Freeze();
        }
        if(newGameState == GameState.Gameplay)
        {
            UnFreeze();
        }
    }

    private void Freeze()
    {
        rb.isKinematic = true;
    }

    private void UnFreeze()
    {
        rb.isKinematic = false;
    }
}
