using Unity.VisualScripting;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    public AudioClip pickupSFX;

    [HideInInspector]
    public static int collected = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collected++;
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
}
