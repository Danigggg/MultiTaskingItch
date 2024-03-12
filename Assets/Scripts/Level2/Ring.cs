using UnityEngine;
using UnityEngine.Events;

public class RotateRing : MonoBehaviour
{
    public float degreePerSec;
    public UnityEvent deathInvoker;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Steer(-degreePerSec);
        }

        if(Input.GetKey(KeyCode.Q)) 
        {
            Steer(degreePerSec);
        }
    }

    private void Steer(float deg)
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, deg * Time.deltaTime));
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
