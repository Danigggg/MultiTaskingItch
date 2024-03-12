using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    public UnityEvent menuShow;
    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Menu))
        {
            PauseOrUnpause();
        }
    }

    public void PauseOrUnpause(bool showMenu = true)
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay ? GameState.Paused : GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);
        if (showMenu)
        {
            menuShow.Invoke();
        }
    }
}
