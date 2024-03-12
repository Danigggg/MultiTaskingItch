using UnityEngine;
using TMPro;

public class TimerText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public PhaseManager phaseManager;
    void Update()
    {
        text.text = phaseManager.trueTimer.ToString("f2");
    }

}