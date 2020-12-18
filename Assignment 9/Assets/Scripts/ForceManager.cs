using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ForceManager
{
    static List<ForceGenerator2D> forceGenerators = new List<ForceGenerator2D>();

    public static void addForceGenerator(ForceGenerator2D forceGenerator)
    {
        forceGenerators.Add(forceGenerator);
    }
    public static void deleteForceGenerator(ForceGenerator2D forceGenerator)
    {
        forceGenerators.Remove(forceGenerator);
    }
    public static void updateAllForces(double dt)
    {
        
        for(int i = 0; i <forceGenerators.Count; i++)
        {
            ForceGenerator2D forceGenerator = forceGenerators[i];

            if (forceGenerator.mShouldEffectAll)
            {
                foreach(Particle2D particle in GameObject.Find("GameManager").GetComponent<ParticleManager>().mParticleList)
                {
                    forceGenerator.updateForce(particle, dt); 
                }
            }
            else
            {
                forceGenerator.updateForce(null,dt);
            }
        }
    } 
}
