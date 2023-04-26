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
    public Vector3 goalPos = Vector3.zero;

    // Add in new Settings in the Unity UI under the FlockManager GameObject
    [Header("Fish Settings")]
    [Range(0.0f, 5.0F)]
    public float minSpeed;
    [Range(0.0f, 5.0F)]
    public float maxSpeed;
    [Range(1.0f, 10.0F)]
    public float neighborDistance;
    [Range(1.0f, 5.0F)]
    public float rotationSpeed;



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
        goalPos = this.transform.position;  // Sets the Goal to the FlockManager object
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range (0,100) < 10)  // Sets the change to 10% the goal will move (basically the Flockmanager object)
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));
        }
    }
}
