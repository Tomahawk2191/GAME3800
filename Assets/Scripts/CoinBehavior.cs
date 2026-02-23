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

    private void OnDestroy()
    {
        if(pickupSFX)
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        }
    }

    public void Raise()
    {
        coinVisual.position = Vector3.Lerp(
        coinVisual.position,
        new Vector3(initialPosition.x, initialPosition.y + liftAmount, initialPosition.z),
        liftSpeed * Time.deltaTime);
    }

    public void Lower()
    {
        coinVisual.position = Vector3.Lerp(coinVisual.position, initialPosition, dropSpeed * Time.deltaTime);

    }

    public void Collect()
    {
        hasCoin = true;
        Destroy(gameObject);
    }
}
