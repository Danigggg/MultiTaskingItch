using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLevel2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform        ring;
    public RectTransform    button;
    public Rect             trackingZone;
    public float            animationSpeed;

    private bool            pass;
    private int             fingerId;
    private Vector2         touchPos;
    private Vector2         pixelPos;

    public void OnPointerDown(PointerEventData eventData)
    {
        pass = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pass = false;
    }

    void Update()
    {
        if (pass)
        {
            UpdateTrackingZone();
            UpdatePos();
            
            if (Input.touchCount > 0)
            {
                float fingerAngle = RotateTowardsInput();
                Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, fingerAngle);
                ring.transform.rotation = Quaternion.Lerp(ring.transform.rotation, targetRotation, animationSpeed * Time.deltaTime);
            }
        }

        //from computer
        if(Input.GetMouseButton(0) && trackingZone.Contains(Input.mousePosition))
        {
            pixelPos = Input.mousePosition;
            touchPos = Camera.main.ScreenToWorldPoint(pixelPos);

            float angle = RotateTowardsInput();
            Quaternion targetRotation = Quaternion.Euler(0.0f, 0.0f, angle);
            ring.transform.rotation = Quaternion.Lerp(ring.transform.rotation, targetRotation, animationSpeed * Time.deltaTime);
        }
    }


    private void UpdatePos()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (trackingZone.Contains(touch.position))
            {
                //idk why it bugs if i use touch.fingerid so i just use i
                fingerId = i;
                pixelPos = Input.GetTouch(fingerId).position;
                touchPos = Camera.main.ScreenToWorldPoint(pixelPos);
                break;
            }
        }

    }

    private void UpdateTrackingZone()
    {

        trackingZone = new Rect(
            Screen.width / 2.0f,
            ConvertBottomLeftToCenter(Camera.main.WorldToScreenPoint(ring.transform.position)).y * 2,
            button.rect.width,
            button.rect.height);
    }

    public Vector2 ConvertBottomLeftToCenter(Vector2 bottomLeftPosition)
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 centerAnchor = screenSize / 2f;

        Vector2 centerPosition = new Vector2(bottomLeftPosition.x - centerAnchor.x, bottomLeftPosition.y - centerAnchor.y);

        return centerPosition;
    }


    private float RotateTowardsInput()
    {
        Transform ringTransform = ring.transform;
        Vector3 worldPositionRing = ringTransform.position;

        float x = worldPositionRing.x - touchPos.x;
        float y = worldPositionRing.y - touchPos.y;
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg + 180.0f;

    }

}
