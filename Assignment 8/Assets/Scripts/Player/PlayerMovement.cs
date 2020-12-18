using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public float speed = 0.75f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            this.transform.Rotate(Vector3.forward, speed);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            this.transform.Rotate(Vector3.forward, -speed);
        }
    }
}
