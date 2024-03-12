using UnityEngine;

public class Spawners : MonoBehaviour
{
    public SpriteRenderer background3;
    public GameObject spawnerLeft;
    public GameObject spawnerRight;
    public Rigidbody2D projectile;
    public SpriteRenderer spriteProjectile;
    public float velocityX;
    public float fireRate;

    public GameObject fatherLevel;
    public float offset;

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
        float posXLeft = background3.transform.position.x - background3.transform.localScale.x / 2.0f + offset;
        float posXRight = background3.transform.position.x + background3.transform.localScale.x / 2.0f - offset;
        float posY = Random.Range(background3.bounds.min.y + spriteProjectile.bounds.size.y, background3.bounds.max.y - spriteProjectile.bounds.size.y);

        spawnerLeft.transform.position = new Vector3(posXLeft, posY, 0.0f);
        spawnerRight.transform.position = new Vector3(posXRight, posY, 0.0f);

        float randomChoice = Random.Range(1, 3); //if number is odd, spawner left shoot, else right

        if (randomChoice % 2 == 1) 
        {
            Rigidbody2D proj = Instantiate(projectile, spawnerLeft.transform.position, Quaternion.identity);
            proj.velocity = new Vector2(velocityX, 0.0f);
            proj.transform.SetParent(fatherLevel.transform);
        }
        else
        {
            Rigidbody2D proj = Instantiate(projectile, spawnerRight.transform.position, Quaternion.identity);
            proj.velocity = new Vector2(-velocityX, 0.0f);
            proj.transform.SetParent(fatherLevel.transform);
        }
        timer = 0.0f;
    }

    //pause behavior
    private void Start()
    {
        this.enabled = false;
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
