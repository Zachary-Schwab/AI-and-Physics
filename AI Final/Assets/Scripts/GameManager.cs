using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite[] pieces;
    public GameObject basePiece;
    float scale = 2.5f;
    public Vector2 intialPosition;
    Board board;
    //seperate gameObject array from game to speed up AI processing
    public GameObject[,] gameObjectBoard;
    // Start is called before the first frame update
    void Start()
    {
        gameObjectBoard = new GameObject[Board.ROW_COUNT, Board.COL_COUNT];
        board = gameObject.GetComponent<Board>();
        GameObject pieceHolder = new GameObject("PieceHolder");
        pieceHolder.transform.position = intialPosition;
        for (int y = 0; y < Board.COL_COUNT; y++)
        {
            for (int x = Board.ROW_COUNT - 1; x >= 0; x--)
            {
                gameObjectBoard[x, y] = Instantiate(basePiece, pieceHolder.transform);
                gameObjectBoard[x, y].name = x + ", " + y;
                gameObjectBoard[x, y].transform.localPosition = new Vector2(y * (scale / 2), -x * (scale / 2));
                gameObjectBoard[x, y].transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < Board.ROW_COUNT; i++)
        {
            for (int j = 0; j < Board.COL_COUNT; j++)
            {
                gameObjectBoard[i, j].GetComponent<SpriteRenderer>().sprite = pieces[0];
            }
        }
    }
}
