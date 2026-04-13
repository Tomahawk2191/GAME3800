using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] pickupSFX;
    public int score;
    public float lifespan;

    [HideInInspector]
    public static int totalScore = 0;

    private static int soundCounter = 0;

    private void Start()
    {
        Invoke("Despawn", lifespan);
    }

    private void OnTriggerEnter(Collider other)   
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
    private void PickUp()
    {
        totalScore += score;
        PlaySound();
        Destroy(gameObject);
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

    private void PlaySound()
    {
        if (pickupSFX[soundCounter])
        {
            AudioSource.PlayClipAtPoint(pickupSFX[soundCounter], Camera.main.transform.position);
        }

        if (soundCounter + 1 >= pickupSFX.Length)
        {
            soundCounter -= soundCounter;
        }
        else
        {
            soundCounter++;
        }
    }
}
