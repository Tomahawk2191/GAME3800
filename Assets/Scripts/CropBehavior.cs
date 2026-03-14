using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    public AudioClip pickupSFX;
    public int score;
    public float lifespan;

    [HideInInspector]
    public static int totalScore = 0;

    private void Start()
    {
        Invoke("Despawn", lifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
    private void PickUp()
    {
        totalScore += score;
        if (pickupSFX)
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        }
        Destroy(gameObject);
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
