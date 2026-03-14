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

    private float count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = spawnVals.frequency;
    }

    private void Update()
    {
        if(HappyFarmerLevelManager.timer > 0)
        {
            SpawnCrops();
        }
    }

    private void SpawnCrops()
    {
        if(count <= 0)
        {
            Instantiate(crop, new Vector3(Random.Range(spawnVals.minXValue, spawnVals.maxXValue), transform.position.y, Random.Range(spawnVals.minZValue, spawnVals.maxZValue)), transform.rotation);
            count = spawnVals.frequency;
        } else
        {
            count -= Time.deltaTime;
        }
    }
}
