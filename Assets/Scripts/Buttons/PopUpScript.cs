using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public PauseController pauseController;
    void Update()
    {
        if (Input.anyKey)
        {
            this.gameObject.SetActive(false);
            pauseController.PauseOrUnpause(false);
        }
    }
}
