using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollisionDetector
{
    public static bool CollisionDetection(Particle2D particle1, Particle2D particle2)
    {
        bool collided = false;
        if(Vector2.Distance(particle1.mPos,particle2.mPos) < (particle1.transform.localScale.x + particle2.transform.localScale.x)/2)
        {
            collided = true;
        }
        return collided;
    }
}
