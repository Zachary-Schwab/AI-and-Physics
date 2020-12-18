using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetColision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] particles = GameObject.FindGameObjectsWithTag("particle");
        
        foreach(GameObject particle in particles)
        {
            if (Vector2.Distance(this.transform.position, particle.transform.position) < this.transform.localScale.x)
            {
                this.gameObject.GetComponent<TargetRespawn>().Respawn();
                Destroy(particle);
            }
        }
    }
}
