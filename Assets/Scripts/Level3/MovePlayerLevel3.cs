using UnityEngine;
using UnityEngine.Events;

public class MovePlayer : MonoBehaviour
{
    public float speed;
    public SpriteRenderer background3;
    public SpriteRenderer playerSprite;
    public UnityEvent deathInvoker;
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(Input.GetAxis("Vertical"));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(Input.GetAxis("Vertical"));
        }

        float minY = background3.bounds.min.y + playerSprite.size.y / 2.0f;
        float maxY = background3.bounds.max.y - playerSprite.size.y / 2.0f;

        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, 0.0f);
    }

    public void Move(float axisVertical)
    {
        var input = new Vector2(0.0f, axisVertical);
        transform.position = (Vector2)transform.position + input * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        enabled = newGameState == GameState.Gameplay;
    }
}
