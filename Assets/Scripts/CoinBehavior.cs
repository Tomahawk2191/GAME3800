using UnityEngine;

public class CoinBehavior : MonoBehaviour

{
    public AudioClip pickupSFX;
    public Transform coinVisual;
    public GameObject highlightObject;

    [HideInInspector]
    public static bool hasCoin = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasCoin = false;
        highlightObject.SetActive(false);
    }

    public void Raise()
    {
        highlightObject.SetActive(true);
    }

    public void Lower()
    {
        highlightObject.SetActive(false);
    }

    public void Collect()
    {
        hasCoin = true;
        if (pickupSFX)
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        }
        Destroy(gameObject);
        Destroy(coinVisual.gameObject);
        Destroy(highlightObject);
    }
}
