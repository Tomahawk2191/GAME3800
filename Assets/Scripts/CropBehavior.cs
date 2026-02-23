using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    public AudioClip pickupSFX;
    public int score;
    public GameObject fullCrop;

    [HideInInspector]
    public static int totalScore = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            totalScore += score;
            Destroy(fullCrop);
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
