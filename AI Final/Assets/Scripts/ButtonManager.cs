using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button drop1;
    public Button drop2;
    public Button drop3;
    public Button drop4;
    public Button drop5;
    public Button drop6;
    public Button drop7;
    public Button reset;
    // Start is called before the first frame update
    void Start()
    {
        drop1.onClick.AddListener(delegate { DropPiece(0); });
        drop2.onClick.AddListener(delegate { DropPiece(1); });
        drop3.onClick.AddListener(delegate { DropPiece(2); });
        drop4.onClick.AddListener(delegate { DropPiece(3); });
        drop5.onClick.AddListener(delegate { DropPiece(4); });
        drop6.onClick.AddListener(delegate { DropPiece(5); });
        drop7.onClick.AddListener(delegate { DropPiece(6); });
        reset.onClick.AddListener(ResetBoard);
    }

    void DropPiece(int col)
    {
        GameObject.Find("GameManager").GetComponent<Board>().DropPiece(col,1);
    }

    private void ResetBoard()
    {
        GameObject.Find("GameManager").GetComponent<Board>().Clear();
        GameObject.Find("GameManager").GetComponent<GameManager>().Clear();
    }
}
