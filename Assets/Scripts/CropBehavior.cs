using Unity.VisualScripting;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    public AudioClip pickupSFX;
    public int score;

    [HideInInspector]
    public static int totalScore = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            totalScore += score;
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
