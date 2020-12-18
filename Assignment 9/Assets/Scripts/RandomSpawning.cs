using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float amount = Random.Range(1,5);
        float x = Random.Range(-8.0f, 8.0f);
        float y = Random.Range(-.5f,5);
        float mass = Random.Range(.5f, 2);
        float gravity =  Random.Range(-.5f, -1.5f);

        this.gameObject.GetComponent<ParticleManager>().createParticle(mass,new Vector2(x,y), Vector2.zero, Vector2.zero, new Vector2(0,gravity),0.99f);
    }
}
