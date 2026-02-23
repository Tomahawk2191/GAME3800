using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CropSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct spawnValues
    {
        public float frequency;
        public float minXValue;
        public float maxXValue;
        public float minZValue;
        public float maxZValue;
    }

    [SerializeField]
    private GameObject crop;
    [SerializeField]
    private spawnValues spawnVals;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("SpawnCrops");
    }

    private IEnumerator SpawnCrops()
    {
        while(HappyFarmerLevelManager.timer > 0)
        {
            yield return new WaitForSeconds(spawnVals.frequency);
            Instantiate(crop, new Vector3(Random.Range(spawnVals.minXValue, spawnVals.maxXValue), transform.position.y, Random.Range(spawnVals.minZValue, spawnVals.maxZValue)), transform.rotation);
        }
        yield return null;
    }
}
