using UnityEngine;
using UnityEngine.InputSystem;

public class ColliderRaycast : MonoBehaviour
{
    public Collider colliderToHit;
    public float distance = 100f;

    Ray ray;
    private CoinBehavior coin;

    private void Start()
    {
        coin = GameObject.FindFirstObjectByType<CoinBehavior>();
    }

    private void Update()
    {
        if(!CoinBehavior.hasCoin)
        {
            ray = new Ray(transform.position, transform.forward);
            CheckForColliders();
        }
    }

    private void CheckForColliders()
    {
        if(colliderToHit.Raycast(ray, out RaycastHit hit, distance))
        {
            coin.Raise();

            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                coin.Collect();
            }
        } else
        {
            coin.Lower();
        }
    }
}
