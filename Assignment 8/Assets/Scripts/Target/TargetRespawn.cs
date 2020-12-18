using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRespawn : MonoBehaviour
{
    public Vector2 min;
    public Vector2 max;
    public int score;
    public void Respawn()
    {
        this.gameObject.GetComponent<Particle2D>().mPos = new Vector2(Random.Range(min.x,max.x), Random.Range(min.y, max.y));
        this.gameObject.GetComponent<Particle2D>().mVel = Vector2.zero;
        score++;

    }
}
