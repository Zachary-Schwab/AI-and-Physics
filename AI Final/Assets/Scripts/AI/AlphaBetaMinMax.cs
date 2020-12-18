using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AlphaBetaMinMax : MonoBehaviour
{
    public const int WHITE = 0;
    public const int RED = 1;
    public const int BLUE = 2;
    const int wins = 1000000;

    public int iWidth = 7;
    public int iHeight = 6;
    public int maxDepth = 10;
    public int piecesPlayed = 0;
    public int blueWins = wins;
    public int redWins = -wins;
    public double TempTimeToScore;

    public enum Mycell
    {
        White = 0,
        Blue = 1,
        Red = -1
    }

    public struct GameBoard
    {
        public Mycell[] _slots;

        public void intialize(int height, int width)
        {
            int index = (height * width);
            _slots = new Mycell[index];
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public int AITurn(int[,] board)
    {
        int drop = -1;
        drop = takeTurn(board);
        return drop;
    }
    public int scoreboard(GameBoard board)
    {
        //Apply a score for the board based on how many cells have the same color in a span of 4 cells

        int[] counters = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int score = 0;


        // Score Rows
        for (int row = 0; row < iHeight; row++)
        {
            score = (int)board._slots[iWidth * (row) + 0] + (int)board._slots[iWidth * (row) + 1] + (int)board._slots[iWidth * (row) + 2];
            for (int col = 3; col < iWidth; col++)
            {
                score += (int)board._slots[iWidth * (row) + col];
                counters[score + 4] += 1;
                score -= (int)board._slots[iWidth * (row) + col - 3];
            }
        }
        // Score Columns
        for (int col = 0; col < iWidth; col++)
        {
            score = (int)board._slots[iWidth * (0) + col] + (int)board._slots[iWidth * (1) + col] + (int)board._slots[iWidth * (2) + col];
            for (int row = 3; row < iHeight; row++)
            {
                score += (int)board._slots[iWidth * (row) + col];
                counters[score + 4] += 1;
                score -= (int)board._slots[iWidth * (row - 3) + col];
            }
        }
        // Score LtR Diagonals
        for (int row = 0; row <= iHeight - 4; row++)
        {
            for (int col = 0; col <= iWidth - 4; col++)
            {
                score = 0;
                for (int ofs = 0; ofs <= 3; ofs++)
                {
                    int yy = row + ofs;
                    int xx = col + ofs;
                    score += (int)board._slots[iWidth * (yy) + xx];
                }
                counters[score + 4] += 1;
            }
        }
        // Score RtL Diagonals
        for (int row = 3; row < iHeight; row++)
        {
            for (int col = 0; col <= iWidth - 4; col++)
            {
                score = 0;
                for (int ofs = 0; ofs <= 3; ofs++)
                {
                    int yy = row - ofs;
                    int xx = col + ofs;
                    score += (int)board._slots[iWidth * (yy) + xx];
                }
                counters[score + 4] += 1;
            }
        }

        if (counters[0] != 0)
        {
            return redWins;
        }
        else if (counters[8] != 0)
        {
            return blueWins;
        }
        else
        {
            return counters[5] + 2 * counters[6] + 5 * counters[7] - counters[3] - 2 * counters[2] - 5 * counters[1];
        }
    }

    public int dropDisk(ref GameBoard board, int col, Mycell color)
    {
        //Drop the disk in the column parameter and return the row it stopped at or -1
        //if all the rows are full for that column
        for (int row = iHeight - 1; row >= 0; row--)
        {
            if (board._slots[iWidth * (row) + col] == Mycell.White)
            {
                board._slots[iWidth * (row) + col] = color;
                piecesPlayed += 1;
                return row;
            }
        }
        return -1;
    }
    public GameBoard loadBoard(int[,] args)
    {
        //Create a new GameBoard and next initialize it to the correct number of spaces
        GameBoard newboard = new GameBoard();
        newboard.intialize(iHeight, iWidth);

        for (int j = 0; j < iWidth; j++)
        {
            for (int i = 0; i < iHeight; i++)
            {
                //The array is of Type Mycell which is initalized to 0 by default so no need to convert White Cells
                if (args[i, j] == RED)
                {
                    newboard._slots[i*(iWidth)+j] = Mycell.Red;
                    piecesPlayed += 1;
                }
                else if (args[i, j] == BLUE)
                {
                    newboard._slots[i*(iWidth)+j] = Mycell.Blue;
                    piecesPlayed += 1;
                }
            }
        }
        return newboard;
    }

    public void undoMove(ref GameBoard board, int col, int row)
    {
        board._slots[iWidth * row + col] = Mycell.White;
        piecesPlayed -= 1;
    }

    public int checkWinMove(Mycell color, ref GameBoard board)
    {
        int winMove = -1;

        for (int column = 0; column < iWidth; column++)
        {
            //Check each column of the top most row to confirm it is not filled (valid move)
            //if it is not empty { skip this column because it//s already full
            if (board._slots[column] != Mycell.White)
            {
                continue;
            }

            //Drop a playing piece in the column and return the row
            int rowFilled = dropDisk(ref board, column, color);

            //Score the current board
            int s = scoreboard(board);

            if ((color == Mycell.Blue && s == blueWins) || (color == Mycell.Red && s == redWins))
            {
                winMove = column;
                //Remove the last piece dropped on the GameBoard
                undoMove(ref board, column, rowFilled);
                break;
            }

            //Remove the last piece dropped on the GameBoard
            undoMove(ref board, column, rowFilled);
        }
        return winMove;
    }

    public int alphaBeta( bool maximizeOrMinimize   , Mycell color   , int depth   , ref GameBoard board   , int alpha   , int beta   , ref int move)
    {
        int bestMove = -1;

        if (depth == 0 || piecesPlayed == iHeight * iWidth)
        {
            int score = 0;
            score = scoreboard(board);
            return score;
        }

        for (int column = 0; column < iWidth; column++)
        { 
            //Check each column of the top most row to confirm it is not filled(valid move)
            //if the column is full then skip the column
            if(board._slots[column] != Mycell.White) 
            {
               continue;
            }

            //Drop a playing piece in the column and return the row
            int rowFilled = dropDisk(ref board, column, color);

            if (maximizeOrMinimize)
            {
                int score = 0;
                score = alphaBeta(!maximizeOrMinimize, (Mycell)((int)color * -1), depth - 1, ref board, alpha, beta, ref move);

                if (alpha < score)
                {
                    alpha = score;
                    bestMove = column;
                }

                if (beta <= alpha)
                {
                    undoMove(ref board, column, rowFilled);
                    break; 
                }
            }
            else
            {
                int score = 0;
                score = alphaBeta(!maximizeOrMinimize, (Mycell)((int)color * -1), depth - 1, ref board, alpha, beta, ref move);

                if (beta > score)
                {
                    beta = score;
                    bestMove = column;
                }

                if (beta <= alpha)
                {
                    undoMove(ref board, column, rowFilled);
                    break;
                }
            }

            undoMove(ref board, column, rowFilled);

            //Check if the last move was a winning move
            if((maximizeOrMinimize && alpha == blueWins) || (!maximizeOrMinimize && beta == redWins))
            {
                if (move == -1)
                    bestMove = column;
                else
                    bestMove = move;
                break;
            }
        }

        move = bestMove;
        return (maximizeOrMinimize ? alpha : beta);
    }
    

    public int takeTurn(int[,] gameState)
    {
        GameBoard board = loadBoard(gameState);

        maxDepth = 8 + Mathf.FloorToInt(piecesPlayed / 15);

        int iMove = -1;
        int alpha = -10000000;
        int beta = 10000000;
        int score = 0;

        //Check if blue can win in the next move
        iMove = checkWinMove(Mycell.Blue, ref board);
        if (iMove == -1)
        {
            //Check if Red can win in the next move (hence block)
            iMove = checkWinMove(Mycell.Red, ref board);
            if (iMove == -1)
            {
                score = alphaBeta(true, Mycell.Blue, maxDepth, ref board, alpha, beta, ref iMove);
            }
        }

        //reset piecesPlayed before next turn
        piecesPlayed = 0;

        return iMove;
    }
}
