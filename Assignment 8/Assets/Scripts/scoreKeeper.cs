using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scoreKeeper : MonoBehaviour
{
    public Text scoreBoard;
    TargetRespawn scoreHolder;

    // Update is called once per frame
    void Update()
    {
        scoreBoard.text = "Score: " + GameObject.FindGameObjectWithTag("Target").GetComponent<TargetRespawn>().score.ToString();
    }
}
