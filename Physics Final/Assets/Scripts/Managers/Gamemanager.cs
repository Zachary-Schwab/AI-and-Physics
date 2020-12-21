using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    ParticleManager particleManager;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        particleManager = this.gameObject.GetComponent<ParticleManager>();

        Particle2D sun1 = particleManager.createParticle(50000, new Vector2(400, -2), new Vector2(0, -2f), Vector2.zero, Vector2.zero, 0.99f, 50);
        sun1.gameObject.name = "sun1";
        sun1.GetComponent<SpriteRenderer>().sprite = sprites[0];
        GravityForceGenerator sunGravity;
        sunGravity = new GravityForceGenerator();
        sunGravity.GravityForceGeneratorIntializer(sun1);
        ForceManager.addForceGenerator(sunGravity);

        Particle2D sun2 = particleManager.createParticle(50000, new Vector2(-325,2), new Vector2(0, 2f), Vector2.zero, Vector2.zero, 0.99f, 50);
        sun2.gameObject.name = "sun2";
        sun2.GetComponent<SpriteRenderer>().sprite = sprites[0];
        GravityForceGenerator sun2Gravity;
        sun2Gravity = new GravityForceGenerator();
        sun2Gravity.GravityForceGeneratorIntializer(sun2);
        ForceManager.addForceGenerator(sun2Gravity);

        Particle2D planetP1 = particleManager.createParticle(1f, new Vector2(-432, 0), new Vector2(-5, 9f), Vector2.zero, Vector2.zero, 0.99f,15);
        planetP1.gameObject.name = "planet1";
        planetP1.GetComponent<SpriteRenderer>().sprite = sprites[1];
        GravityForceGenerator planet1Gravity;
        planet1Gravity = new GravityForceGenerator();
        planet1Gravity.GravityForceGeneratorIntializer(planetP1);
        ForceManager.addForceGenerator(planet1Gravity);
        Particle2D planetP2 = particleManager.createParticle(0.3f, new Vector2(450f, 40), new Vector2(-8f, 15f), Vector2.zero, Vector2.zero, 0.99f, 30);
        planetP2.gameObject.name = "planetP2";
        planetP2.GetComponent<SpriteRenderer>().sprite = sprites[1];
        GravityForceGenerator planet3Gravity;
        planet3Gravity = new GravityForceGenerator();
        planet3Gravity.GravityForceGeneratorIntializer(planetP2);
        ForceManager.addForceGenerator(planet3Gravity);
        
        /*
        Particle2D planet4 = particleManager.createParticle(0.1f, new Vector2(80f, 0), new Vector2(0, 40f), Vector2.zero, Vector2.zero, 0.99f);
        planet4.gameObject.name = "planet4";
        planet4.GetComponent<SpriteRenderer>().sprite = sprites[1];
        GravityForceGenerator planet4Gravity;
        planet4Gravity = new GravityForceGenerator();
        planet4Gravity.GravityForceGeneratorIntializer(planet4);
        ForceManager.addForceGenerator(planet4Gravity);

        Particle2D planet5 = particleManager.createParticle(0.5f, new Vector2(138f, 0), new Vector2(0, 15.5f), Vector2.zero, Vector2.zero, 0.99f);
        planet5.gameObject.name = "planet2";
        planet5.GetComponent<SpriteRenderer>().sprite = sprites[1];
        GravityForceGenerator planet2Gravity;
        planet2Gravity = new GravityForceGenerator();
        planet2Gravity.GravityForceGeneratorIntializer(planet5);
        ForceManager.addForceGenerator(planet2Gravity);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
