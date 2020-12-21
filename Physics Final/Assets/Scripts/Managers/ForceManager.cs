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
                foreach(KeyValuePair<Int32,Particle2D> particlePair in GameObject.Find("GameManager").GetComponent<ParticleManager>().mParticleMap)
                {
                    forceGenerator.updateForce(particlePair.Value, dt); 
                }
            }
            else
            {
                forceGenerator.updateForce(null,dt);
            }
        }
    } 
}
