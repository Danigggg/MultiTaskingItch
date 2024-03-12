using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public Canvas deathScreen;
    public Canvas Menu;
    public PostProcessVolume ppVolumeCamera;
    public PhaseManager phaseManager;
    public PauseController pauseController;
    public ButtonManager buttonManager;
    public BaseBallMove baseBall;

    public RectTransform panelPopUpMessage;
    public TextMeshProUGUI panelText;

    public string panelTxtLevel1;
    public string panelTxtLevel2;
    public string panelTxtLevel3;
    public string panelTxtLevel4;

    public void Start()
    {

    }

    public void Death()
    {
        deathScreen.gameObject.SetActive(true);
        ppVolumeCamera.enabled = true;
        DisableScripts();
    }

    public void DisableScripts()
    {
        phaseManager.enabled = false;
        pauseController.enabled = false;
        buttonManager.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuCanva()
    {
        ppVolumeCamera.enabled = !ppVolumeCamera.enabled;
        Menu.gameObject.SetActive(!Menu.gameObject.activeInHierarchy);
    }

    public void ShowPopUp(int phase)
    {
        switch (phase)
        {
            case 1:
                pauseController.PauseOrUnpause(false);
                panelPopUpMessage.gameObject.SetActive(true);
                panelText.text = panelTxtLevel1;
                break;
            case 2:
                pauseController.PauseOrUnpause(false);
                panelPopUpMessage.gameObject.SetActive(true);
                panelText.text = panelTxtLevel2;
                break;
            case 3:
                pauseController.PauseOrUnpause(false);
                panelPopUpMessage.gameObject.SetActive(true);
                panelText.text = panelTxtLevel3;
                break;
            case 4:
                pauseController.PauseOrUnpause(false);  
                panelPopUpMessage.gameObject.SetActive(true);
                panelText.text = panelTxtLevel4;
                break;

        }
    }


    public void Quit()
    {
        Application.Quit();
    }

}
