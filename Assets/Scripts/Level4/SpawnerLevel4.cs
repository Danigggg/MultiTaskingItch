using UnityEngine;

public class SpawnerLevel4 : MonoBehaviour
{
    public Rigidbody2D obstacle;
    public SpriteRenderer obstacleSprite;
    public SpriteRenderer background4;
    public float fireRate;
    public float velX;
    public float offSet;
    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > fireRate)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        float minY = background4.bounds.min.y + obstacleSprite.bounds.size.y / 2.0f;
        float maxY = background4.bounds.max.y - obstacleSprite.bounds.size.y / 2.0f;
        
        Vector2 randomPos = new Vector2(transform.position.x - offSet, Random.Range(minY, maxY));

        Rigidbody2D obstacl = Instantiate(obstacle, randomPos, Quaternion.identity);
        obstacl.velocity = new Vector2(-velX, 0.0f);
        timer = 0.0f;
    }

    //pause behavior
    private void Start()
    {
        this.enabled = false;
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        transform.position = new Vector3(background4.bounds.max.x, background4.bounds.max.y / 2.0f);
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
