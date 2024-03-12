using System.Collections;
using UnityEngine;

public class BaseBallMove : MonoBehaviour
{
    public float torque;
    public Rigidbody2D rb;
    public float afkTimer = 0.0f;
    public float timeBeforeAfk;
    private float angularVel;
    
    // Update is called once per frame
    void Update()
    {

        afkTimer += Time.deltaTime;

        if(Input.GetKey(KeyCode.RightArrow))
        {
            RotateObj(torque);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            RotateObj(-torque);
        }

        if(afkTimer > timeBeforeAfk)
        {
            ShakeBase();
        }
    }

    public void RotateObj(float amount)
    {
        rb.AddTorque(amount * Time.deltaTime);
        afkTimer = 0.0f;
    }

    public void ShakeBase()
    {
        if(Random.value < 0.5f)
        {
            RotateObj(torque * 10);
            return;
        }

        RotateObj(torque * -1 * 10);

        timeBeforeAfk *= timeBeforeAfk;
       
    }



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
        enabled = newGameState == GameState.Gameplay;
    }

    private void Freeze()
    {
        angularVel = rb.angularVelocity;
        rb.angularVelocity = 0.0f;
    }

    private void UnFreeze()
    {
        rb.angularVelocity = angularVel;
    }
}
