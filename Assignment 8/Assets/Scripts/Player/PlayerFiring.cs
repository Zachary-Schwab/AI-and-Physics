using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    BOLA,
    ROD
}

public class PlayerFiring : MonoBehaviour
{
    public ProjectileType projectileType = 0;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameObject.Find("GameManager").GetComponent<ParticleManager>().mParticleMap.Count);
        if (Input.GetKeyDown(KeyCode.W))
        {
            projectileType++;
            if(projectileType > ProjectileType.ROD)
            {
                projectileType = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.Find("GameManager").GetComponent<ParticleManager>().createProjectile(projectileType, this.transform.position,
                                                        new Vector2(Mathf.Cos(this.transform.rotation.z), Mathf.Sin(this.transform.rotation.z)));
        }
    }
}
