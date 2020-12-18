using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Board : MonoBehaviour
{
    public const int ROW_COUNT = 6;
    public const int COL_COUNT = 7;

    public bool started = false;
    public Text winIndicator;

    Vector2Int lastPlayedPiece;
    int currentPiece = 1;
    public int[,] board = new int[ROW_COUNT, COL_COUNT]
    {
		{0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0}
	};
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool DropPiece(int col, int playerCode)
    {
        bool placed = false;
        if (playerCode == currentPiece)
        {
            for (int i = ROW_COUNT - 1; i >= 0; i--)
            {
                if (board[i, col] == 0)
                {
                    board[i, col] = currentPiece;
                    lastPlayedPiece.x = i;
                    lastPlayedPiece.y = col;
                    this.gameObject.GetComponent<GameManager>().gameObjectBoard[i, col].GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<GameManager>().pieces[currentPiece];
                    placed = true;
                    string win = CheckForWin();
                    if(win == "")
                    { 
                        EndTurn();
                    }
                    else
                    {
                        winIndicator.text = win;
                    }
                    break;
                }
            }
        }
        return placed;
    }
    public string ColorTurn()
    {
        string returnVal = "";
        if (currentPiece == 1)
            returnVal = "RED";
        else
            returnVal = "BLUE";
        return returnVal;
    }
    public void EndTurn()
    {
        if (currentPiece == 1)
        {
            currentPiece = 2;
            DropPiece(this.gameObject.GetComponent<AlphaBetaMinMax>().AITurn(board),2);
        }
        else
        {
            currentPiece = 1;
        }
    }
    public void Clear()
    {
        for (int i = 0; i < ROW_COUNT; i++)
        {
            for (int j = 0; j < COL_COUNT; j++)
            {
                board[i,j] = 0;
            }
        }
        currentPiece = 1;
        winIndicator.text = "";
    }

    public string CheckForWin()
    {
        int row = lastPlayedPiece.x;
        int col = lastPlayedPiece.y;
        string returnVal = "";
        if (VertCheckWin(row, col) || HorizCheckWin(row) || RtLDiagCheckWin(row, col) || LtRDiagCheckWin(row, col))
        {
            returnVal = ColorTurn() + " WON!!";
        }
        if (returnVal == "")
            returnVal = TieCheck();
        return returnVal;
    }
    public string TieCheck()
    {
        string returnVal = "Its a Tie!";
        for (int i = 0; i < ROW_COUNT; i++)
        {
            for (int j = 0; j < COL_COUNT; j++)
            {
                //if any pieces are white its not a tie yet
                if (board[i,j] == 0)
                    returnVal = "";
            }
        }
        if (returnVal != "")
        {
            started = false;
        }
        return returnVal;
    }

    //checks if there are any vertical wins
    bool VertCheckWin(int row, int col)
    {
        bool win = false;
        //if the piece is not placed high enough no reason to check for vertical wins
        if (row > 2)
            win = false;
        else
        {
            int count = 0;
            for (int i = row; i < ROW_COUNT; i++)
            {
                if (board[i,col] == currentPiece)
                    count++;
                if (board[i,col] != currentPiece)
                    count = 0;
                if (count == 4)
                    win = true;
            }
        }
        return win;
    }
    //checks the row that was just placed for horizontal win
    bool HorizCheckWin(int row)
    {
        bool win = false;
        int count = 0;
        for (int i = 0; i < COL_COUNT; i++)
        {
            if (board[row,i] == currentPiece)
                count++;
            if (board[row,i] != currentPiece)
                count = 0;
            if (count >= 4)
                win = true;
        }
        return win;
    }
    //checks wins where the left side of the diagnal is higher than the right and if those are wins
    bool LtRDiagCheckWin(int row, int col)
    {
        int[] diagonal = new int[Mathf.Min(ROW_COUNT, COL_COUNT)];

        int maxRow = row;
        int maxCol = col;
        //leftmost position that the diagnal could be
        while (maxRow < 5 && maxCol< 6)
		{
            maxRow++;
            maxCol++;
        }

        int minRow = row;
        int minCol = col;

        //rightmost position that the diagnal could be
        while (minRow > 0 && minCol > 0)
		{
            minRow--;
            minCol--;
        }

        int currentRow = minRow;
        int currentCol = minCol;
        //add the whole diagnal to the diagonal array
        for (int i = 0; i < (maxRow - minRow + 1); i++)
        {
            diagonal[i] = board[currentRow,currentCol];
            currentRow++;
            currentCol++;
        }

        bool win = false;
        int count = 0;
        //iterate through the diagonal array and count number of the current turn piece in a row
        for (int j = 0; j < 6; j++)
        {
            if (diagonal[j] == currentPiece)
                count++;
            if (diagonal[j] != currentPiece)
                count = 0;
            if (count == 4)
                win = true;
        }
        return win;
    }
    //checks wins where the right side of the diagnal is higher than the left and if those are wins
    bool RtLDiagCheckWin(int row, int col)
    {
        int[] diagonal = new int[Mathf.Min(ROW_COUNT,COL_COUNT)];

        int minRow = row;
        int maxCol = col;

        //leftmost position that the diagnal could be
        while (minRow > 0 && maxCol < 6)
        {
            minRow--;
            maxCol++;
        }

        int maxRow = row;
        int minCol = col;

        //rightmost position that the diagnal could be
        while (maxRow < 5 && minCol > 0)
        {
            maxRow++;
            minCol--;
        }

        int currentRow = minRow;
        int currentCol = maxCol;
        //add the whole diagnal to the diagonal array
        for (int i = 0; i < (maxRow - minRow + 1); i++)
        {
            diagonal[i] = board[currentRow,currentCol];
            currentRow++;
            currentCol--;
        }

        bool win = false;
        int count = 0;
        //iterate through the diagonal array and count number of the current turn piece in a row
        for (int j = 0; j < 6; j++)
        {
            if (diagonal[j] == currentPiece)
                count++;
            if (diagonal[j] != currentPiece)
                count = 0;
            if (count == 4)
                win = true;
        }
        return win;
    }

}