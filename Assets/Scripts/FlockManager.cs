using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swimLimits = new Vector3(5, 5, 5);
    public static FlockManager FM;

    [Header("Fish Settings")]
    [Range(0.0f, 5.0F)]
    public float minSpeed;
    [Range(0.0f, 5.0F)]
    public float maxSpeed;


    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i=0; i<numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x,swimLimits.x), 
                Random.Range(-swimLimits.y, swimLimits.y), 
                Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = Instantiate(fishPrefab, pos, Quaternion.identity);
        }
        FM = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
