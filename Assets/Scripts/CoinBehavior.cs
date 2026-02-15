using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CoinBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public AudioClip pickupSFX;
    public Transform coinVisual;
    public float liftAmount;
    public float liftSpeed;
    public float dropSpeed;

    [HideInInspector]
    public static bool hasCoin = false;

    private Vector3 initialPosition;

    private bool hoveringCoin = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = coinVisual.position;
    }

    void Update()
    {
        if(hoveringCoin && Mouse.current.leftButton.wasPressedThisFrame)
        {
            hasCoin = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(pickupSFX)
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        coinVisual.position = Vector3.Lerp(
        coinVisual.transform.position,
        new Vector3(initialPosition.x, initialPosition.y + liftAmount, initialPosition.z),
        liftSpeed * Time.deltaTime);

        hoveringCoin = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        coinVisual.position = Vector3.Lerp(coinVisual.position, initialPosition, dropSpeed * Time.deltaTime);

        hoveringCoin = false;
    }
}
