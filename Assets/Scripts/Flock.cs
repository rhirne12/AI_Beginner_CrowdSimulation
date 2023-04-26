using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    float speed;
    bool turning = false;   // turn on when fish hits outer edge


    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Bounds b = new Bounds(FlockManager.FM.transform.position, FlockManager.FM.swimLimits * 2);  // Sets bounds for fish spawning and swim location

        if (!b.Contains(transform.position))  // If fish is outside of the bounds
        {
            turning = true;
        }
        else
            turning = false;

        if (turning)
        {
            Vector3 direction = FlockManager.FM.transform.position - transform.position;  // Sets vector to get fish back inside bounds
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockManager.FM.rotationSpeed * Time.deltaTime);  // Turns fish back into boundaries
        }
        else
        {


            if (Random.Range(0, 100) < 10)
            {
                speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);
            }

            if (Random.Range(0, 100) < 10)
            {
                ApplyRules();
            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);  // Move fish forward
    }

    void ApplyRules()
    {
        // Grab all the fish objects that have been instantiated
        GameObject[] gos;
        gos = FlockManager.FM.allFish;

        Vector3 vCenter = Vector3.zero; // the center fish are trying to swim to
        Vector3 vAvoid = Vector3.zero;  // The avoid vector of other fish
        float gSpeed = 0.01f;           // 
        float nDistance;                // Distance to neighbor fish
        int groupSize = 0;              // Total number of neighbor fish in group - initially set to zero

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= FlockManager.FM.neighborDistance)      // compares distance to fish to determine if it is within range to be considered a neighbor fish
                {
                    vCenter += go.transform.position;  // Add position of all fish together
                    groupSize++;   //  Adds 1 to total number of neighbor fish

                    if (nDistance < 1.0f)  // if fish is really close to it's neighbor, need to avoid it
                    {
                        vAvoid = vAvoid + (this.transform.position - go.transform.position);
                    }

                    // Gets flock script on current comparison fish to add speed of flock
                    Flock anotherFlock = go.GetComponent<Flock>();      
                    gSpeed = gSpeed + anotherFlock.speed;

                }
            }
        }

        // test group size is larger than 0 and get the average center and speed and set new direction
        if (groupSize > 0)
        {
            vCenter = vCenter / groupSize + (FlockManager.FM.goalPos - this.transform.position);  // Intergrates the new goal direction from Flockmanager
            speed = gSpeed / groupSize;

            // Sets max speed so that the fish speed doesn't get out of control
            if (speed > FlockManager.FM.maxSpeed)
                speed = FlockManager.FM.maxSpeed;

            Vector3 direction = (vCenter + vAvoid) - transform.position;  // new direction for fish and slowly turn fish to new direction
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        FlockManager.FM.rotationSpeed * Time.deltaTime);

        }
    }
}
