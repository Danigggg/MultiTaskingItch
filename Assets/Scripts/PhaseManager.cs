using System.Collections;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public float trueTimer;
    public float timer;
    public float timerPhase2;
    public float timerPhase3;
    public float timerPhase4;
    public BackGroundManager backGroundManager;
    public DivideColliders divideColliders;
    public ButtonManager buttonManager;
    public LogicManager logicManager;
    public float animationSpeed;

    public GameObject objectPhase1;
    public GameObject objectPhase2;
    public GameObject objectPhase3;
    public GameObject objectPhase4;

    private int countPhase = 1; // we are already in phase 1

    private void Start()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        PositionEveryLevel();
        PopUp(countPhase);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        trueTimer += Time.deltaTime;

        CheckTimer();
    }

    private void CheckTimer()
    {
        if (timer < 0)
        {
            UpdatePhase();
        }
    }

    private void UpdatePhase()
    {
        ++countPhase;

        switch(countPhase)
        {
            case 2:
                PhaseTwo();
                break;
            case 3:
                PhaseThree();
                break;
            case 4:
                PhaseFour();
                break;
        }
    }

    private void PhaseTwo()
    {
        Vector3 pos1 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 1 / 4, Screen.height / 2.0f));
        pos1.z = 0.0f;

        Vector3 pos2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 3 / 4, Screen.height / 2.0f));
        pos2.z = 0.0f;

        objectPhase2.SetActive(true);

        StartCoroutine(MovePosGradually(objectPhase2, pos2));
        StartCoroutine(MovePosGradually(objectPhase1, pos1));

        divideColliders.PhaseTwo();
        backGroundManager.PhaseTwo();
        buttonManager.PhaseTwo();
        timer = timerPhase2;

        PopUp(countPhase);

    }

    private void PhaseThree()
    {
        Vector3 pos2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 3 / 4, Screen.height * 3 / 4));
        pos2.z = 0.0f;

        Vector3 pos3 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 3 / 4, Screen.height * 1 / 4));
        pos3.z = 0.0f;

        objectPhase3.SetActive(true);

        StartCoroutine(MovePosGradually(objectPhase3, pos3));
        StartCoroutine(MovePosGradually(objectPhase2, pos2));

        backGroundManager.PhaseThree();
        buttonManager.PhaseThree();
        timer = timerPhase3;

        PopUp(countPhase);

    }

    private void PhaseFour()
    {
        Vector3 pos1 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 1 / 4, Screen.height * 3 / 4));
        pos1.z = 0.0f;

        Vector3 pos4 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 1 / 4, Screen.height * 1 / 4));
        pos4.z = 0.0f;

        objectPhase4.SetActive(true);

        StartCoroutine(MovePosGradually(objectPhase4 , pos4));
        StartCoroutine(MovePosGradually(objectPhase1, pos1));

        backGroundManager.PhaseFour();
        buttonManager.PhaseFour();
        timer = timerPhase4;

        PopUp(countPhase);
    }

    private void PositionEveryLevel()
    {
        Vector3 posGameObject2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 5 / 4, Screen.height / 2.0f));
        posGameObject2.z = 0.0f;

        objectPhase2.transform.position = posGameObject2;

        Vector3 posGameObject3 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 3 / 4, Screen.height * 5 / 4));
        posGameObject3.y *= -1;
        posGameObject3.z = 0.0f;

        objectPhase3.transform.position = posGameObject3;

        Vector3 posGameObject4 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 1 / 4, Screen.height * 5 / 4));
        posGameObject4.y *= -1;
        posGameObject4.z = 0.0f;

        objectPhase4.transform.position = posGameObject4;

    }

    public IEnumerator MovePosGradually(GameObject MainObjPos, Vector3 ToReach) //dividecollider uses this to follow along the background moving 
    {
        while(Mathf.Abs(MainObjPos.transform.position.magnitude - ToReach.magnitude) >= 0.01f)
        {
            MainObjPos.transform.position = Vector3.Lerp(MainObjPos.transform.position, ToReach, animationSpeed * Time.deltaTime);
            yield return null;
        }
    }


    public void PopUp(int phase)
    {
        logicManager.ShowPopUp(phase);
    }

    //pause behavior 

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

}
