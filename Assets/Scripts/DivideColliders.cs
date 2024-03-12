using UnityEngine;

public class DivideColliders : MonoBehaviour
{
    public BoxCollider2D colliderUp;
    public BoxCollider2D colliderDown;
    public BoxCollider2D colliderLeft;
    public BoxCollider2D colliderRight;
    public BoxCollider2D colliderMidHeight;

    public PhaseManager phaseManager;

    private void Start()
    {
        SetFourToBounds();
    }

    private void SetFourToBounds()
    {
        var screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        float worldScreenWidth = screenToWorld.x * 2.0f;
        float worldScreenHeigth = screenToWorld.y * 2.0f;

        colliderUp.gameObject.SetActive(true);
        colliderUp.size = new Vector2(worldScreenWidth, 0.1f);
        colliderUp.transform.position = new Vector2(0.0f, Camera.main.orthographicSize);
        
        colliderDown.gameObject.SetActive(true);
        colliderDown.size = new Vector2(worldScreenWidth, 0.1f);
        colliderDown.transform.position = new Vector2(0.0f, Camera.main.orthographicSize * -1);

        colliderLeft.gameObject.SetActive(true);
        colliderLeft.size = new Vector2(0.1f, worldScreenHeigth);
        colliderLeft.transform.position = new Vector2(-worldScreenWidth / 2.0f, 0.0f);

        colliderRight.gameObject.SetActive(true);
        colliderRight.size = new Vector2(0.1f, worldScreenHeigth);
        colliderRight.transform.position = new Vector2(worldScreenWidth / 2.0f, 0.0f);
    }

    public void PhaseTwo()
    {
        var screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        float worldScreenHeigth = screenToWorld.y * 2.0f;

        colliderMidHeight.gameObject.SetActive(true);
        colliderMidHeight.size = new Vector2(0.1f, worldScreenHeigth);

        colliderMidHeight.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2.0f, 0.0f));

        Vector3 desiredPos = new Vector3(0.0f, 0.0f, 0.0f); //origin

        StartCoroutine(phaseManager.MovePosGradually(colliderMidHeight.gameObject, desiredPos));

    }

}
