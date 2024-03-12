using System.Collections;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public Camera worldCamera;
    public SpriteRenderer background1;
    public SpriteRenderer background2;
    public SpriteRenderer background3;
    public SpriteRenderer background4;
    public float animationSpeed; //this should be different from phasemanager animation speed

    void Start()
    {
        SetAllBackGrounds(); 
    }


    private void SetAllBackGrounds()
    {
        Bounds cameraBounds = CameraBounds(worldCamera);

        Bounds spriteBounds = background1.bounds;
        Vector3 spriteSize = spriteBounds.size;

        float scaleX = cameraBounds.size.x / spriteSize.x;
        float scaleY = cameraBounds.size.y / spriteSize.y;

        //this all will be modified every new phase
        background1.transform.localScale = new Vector3(scaleX, scaleY, 1.0f);
        background2.transform.localScale = new Vector3(scaleX, scaleY, 1.0f);
        background3.transform.localScale = new Vector3(scaleX, scaleY, 1.0f);
        background4.transform.localScale = new Vector3(scaleX, scaleY, 1.0f);

    }

    Bounds CameraBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2f;
        Bounds bounds = new Bounds(camera.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    //called in phasemanager
    public void PhaseTwo()
    {
        background2.transform.localScale = new Vector3(background2.transform.localScale.x / 2.0f, background1.transform.localScale.y, 1.0f);

        Vector3 Background1ScaleToReach = new Vector3(background1.transform.localScale.x / 2.0f, background1.transform.localScale.y, 1.0f);
        StartCoroutine(LerpScaleBackground(background1, Background1ScaleToReach));
    }

    public void PhaseThree()
    {
        background3.transform.localScale = new Vector3(background3.transform.localScale.x / 2.0f, background1.transform.localScale.y / 2.0f, 1.0f);

        Vector3 Background2ScaleToReach = new Vector3(background2.transform.localScale.x, background2.transform.localScale.y / 2.0f, 1.0f);
        StartCoroutine(LerpScaleBackground(background2, Background2ScaleToReach));
    }


    public void PhaseFour()
    {
        background4.transform.localScale = new Vector3(background4.transform.localScale.x / 2.0f, background4.transform.localScale.y / 2.0f, 1.0f);

        Vector3 Background1ScaleToReach = new Vector3(background1.transform.localScale.x, background1.transform.localScale.y / 2.0f, 1.0f);
        StartCoroutine(LerpScaleBackground(background1, Background1ScaleToReach));
    }

    private IEnumerator LerpScaleBackground(SpriteRenderer Background, Vector3 ScaleToReach)
    {
        while (Mathf.Abs(Background.transform.localScale.magnitude - ScaleToReach.magnitude) >= 0.01f)
        {
            Background.transform.localScale = Vector3.Lerp(Background.transform.localScale, ScaleToReach, animationSpeed * Time.deltaTime);
            yield return null;
        }

    }
}

