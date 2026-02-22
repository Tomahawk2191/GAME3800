using UnityEngine;

public class ColliderRaycast : MonoBehaviour
{
    public Collider colliderToHit;
    public float distance = 100f;

    Ray ray;

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        CheckForColliders();
    }

    private void CheckForColliders()
    {
        if(colliderToHit.Raycast(ray, out RaycastHit hit, distance))
        {
            CoinBehavior coin = colliderToHit.gameObject.GetComponent<CoinBehavior>();

            if (coin != null)
            {
                Debug.Log("There's a coin there");
            }
        }
    }
}
