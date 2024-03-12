using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
