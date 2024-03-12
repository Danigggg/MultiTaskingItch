using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public float animationSpeed;

    [Header("Level 1")]
    [SerializeField] RectTransform buttonLeft;
    [SerializeField] RectTransform buttonRight;

    [Header("Level 2")]
    [SerializeField] RectTransform buttonRing;

    [Header("Level 3")]
    [SerializeField] RectTransform buttonUp;
    [SerializeField] RectTransform buttonDown;

    [Header("Level 3")]
    [SerializeField] RectTransform buttonLevel4;

    void Start()
    {
        PhaseOne();
    }

    private void PhaseOne()
    {
        float cameraHeight = Screen.height;
        float cameraWidth = Screen.width;

        Vector2 posLeft = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 1 / 4, Screen.height / 2.0f));
        Vector2 buttonLeftSize = new Vector2( cameraWidth / 2, cameraHeight);
        buttonLeft.position = new Vector3(posLeft.x, posLeft.y , 0.0f);
        buttonLeft.sizeDelta = new Vector2(buttonLeftSize.x, buttonLeftSize.y);

        Vector2 posRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 3 / 4, Screen.height / 2.0f));
        Vector2 buttonRightSize = new Vector2( cameraWidth / 2, cameraHeight);
        buttonRight.position = new Vector3(posRight.x, posRight.y, 0.0f);
        buttonRight.sizeDelta = new Vector2(buttonRightSize.x, buttonRightSize.y);

    }

    public void PhaseTwo()
    {
        StartCoroutine(LerpButton(buttonLeft, new Vector2(buttonLeft.sizeDelta.x / 2.0f, buttonLeft.sizeDelta.y)));
        StartCoroutine(LerpButton(buttonRight, new Vector2(buttonRight.sizeDelta.x / 2.0f, buttonRight.sizeDelta.y)));

        Vector2 posButton1Object1 = new Vector2(Screen.width * 1 / 8, Screen.height / 2);
        Vector2 posButton2Object1 = new Vector2(Screen.width * 3 / 8, Screen.height / 2);

        StartCoroutine(MovePosGradually(buttonLeft, posButton1Object1));
        StartCoroutine(MovePosGradually(buttonRight, posButton2Object1));


        Vector2 posRing = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 3 / 4, Screen.height / 2.0f));
        Vector2 buttonRingSize = new Vector2(Screen.width / 2, Screen.height);
        buttonRing.position = new Vector3(posRing.x, posRing.y, 0.0f);
        StartCoroutine(LerpButton(buttonRing, new Vector2(buttonRingSize.x, buttonRingSize.y)));

    }

    public void PhaseThree()
    {
        Vector2 posRing = new Vector2(Screen.width * 3 / 4, Screen.height * 3 / 4);
        StartCoroutine(MovePosGradually(buttonRing, posRing));
        Vector2 ringLerp = new Vector2(Screen.width / 2 , Screen.height / 2);
        StartCoroutine(LerpButton(buttonRing, ringLerp));

        Vector2 posButtonUp = new Vector2(Screen.width * 3 / 4, Screen.height * 3 / 8);
        Vector2 posButtonDown = new Vector2(Screen.width * 3 / 4 , Screen.height * 1 /8);

        Vector2 button3Size = new Vector2(Screen.width / 2, Screen.height / 4);

        StartCoroutine(MovePosGradually(buttonUp, posButtonUp));
        StartCoroutine(MovePosGradually(buttonDown, posButtonDown));
        StartCoroutine(LerpButton(buttonUp, button3Size));
        StartCoroutine(LerpButton(buttonDown, button3Size));

    }

    public void PhaseFour()
    {
        Vector2 button1LeftPos = new Vector2(Screen.width * 1 / 8, Screen.height * 3 / 4);
        Vector2 button1RightPos = new Vector2(Screen.width * 3 / 8 ,Screen.height * 3 / 4);
        StartCoroutine(MovePosGradually(buttonLeft, button1LeftPos));
        StartCoroutine(MovePosGradually(buttonRight, button1RightPos));

        Vector2 button1LeftSize = new Vector2(buttonLeft.sizeDelta.x, buttonLeft.sizeDelta.y / 2);
        Vector2 button1RightSize = new Vector2(buttonRight.sizeDelta.x, buttonRight.sizeDelta.y / 2);
        StartCoroutine(LerpButton(buttonLeft, button1LeftSize));
        StartCoroutine(LerpButton(buttonRight, button1RightSize));

        Vector2 buttonLvl4Pos = new Vector2(Screen.width * 1 / 4, Screen.height * 1 / 4);
        StartCoroutine(MovePosGradually(buttonLevel4,buttonLvl4Pos));

        Vector2 buttonLvl4Size = new Vector2(Screen.width * 1 / 2 , Screen.height * 1 / 2);
        StartCoroutine (LerpButton(buttonLevel4,buttonLvl4Size));


    }

    private IEnumerator LerpButton(RectTransform button, Vector2 SizeToReach)
    {
        while (Math.Abs(button.sizeDelta.magnitude - SizeToReach.magnitude) >= 0.1f)
        {
            Vector2 current = button.sizeDelta;
            Vector2 newShape = Vector2.Lerp(current, SizeToReach, animationSpeed * Time.deltaTime);
            button.sizeDelta = new Vector2(newShape.x, newShape.y);
            yield return null;
        }
    }

    public IEnumerator MovePosGradually(RectTransform rect, Vector2 ToReach) //dividecollider uses this to follow along the background moving 
    {
        while(Mathf.Abs(rect.anchoredPosition.magnitude - ToReach.magnitude) >= 0.01f)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, ToReach, animationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
