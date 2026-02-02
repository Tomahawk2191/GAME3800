using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public AudioClip pickupSFX;
    public Transform coinVisual;
    public float liftAmount;
    public float liftSpeed;
    public float dropSpeed;

    [HideInInspector]
    public static bool hasCoin = false;

    private Vector3 initialPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = coinVisual.position;
    }

    private void OnMouseEnter()
    {
        coinVisual.position = Vector3.Lerp(
            coinVisual.transform.position,
            new Vector3(initialPosition.x, initialPosition.y + liftAmount, initialPosition.z),
            liftSpeed * Time.deltaTime);
    }

    private void OnMouseExit()
    {
        coinVisual.position = Vector3.Lerp(coinVisual.position, initialPosition, dropSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        hasCoin = true;
        if(pickupSFX)
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        }
    }
}
