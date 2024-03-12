using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpriteRenderer background;
    public float fireRate;
    public GameObject arrow;
    public float radius;
    public GameObject fatherArrow;

    public float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > fireRate)
        {
            SpawnArrow();
        }
    }

    private void SpawnArrow()
    {
        float angle = Random.Range(0.0f, 360.0f);
        float x = transform.position.x + (Mathf.Cos(angle) * radius);
        float y = transform.position.y + (Mathf.Sin(angle) * radius);

        Vector3 spawnPosition = new Vector3(x, y, 0.0f);

        float rotationArrow = RotateArrowToCenter(x, y);

        GameObject arrowCopy = Instantiate(arrow, spawnPosition, Quaternion.Euler(0.0f, 0.0f, rotationArrow * 180.0f / Mathf.PI));

        arrowCopy.transform.SetParent(fatherArrow.transform);

        timer = 0.0f;
    }

    private float RotateArrowToCenter(float x, float y)
    {
        //spawner will be the same location of the ring
        float Xx = transform.position.x - x;
        float Yy = transform.position.y - y;
        return Mathf.Atan2(Yy, Xx);
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
