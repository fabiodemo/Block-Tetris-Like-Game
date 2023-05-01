using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];
    public static int level = 0;
    private static int fullLines = 0;
    private static int score = 0;

    public static int FullLines { get => fullLines; set => fullLines = value; }
    public static int Score { get => score; set => score = value; }

    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }

    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                // Move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void decreaseRowAbove (int y)
    {
        for (int i = y; i < h; ++i) decreaseRow(i);
    }

    public static bool isRowFull (int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] == null)
                return false;
        }
        FullLines++;
        if((FullLines%10) == 0 && level <= 10)
        {
            level++;
        }
        return true;
    }

    public static int deleteFullRows()
    {
        int numberOfFullRows = 0;
        for (int y = 0; y < h; ++y)
            if (isRowFull(y))
            {
                numberOfFullRows++;
                deleteRow(y);
                decreaseRowAbove(y + 1);
                --y;
            }
        SumScore(numberOfFullRows);
        return numberOfFullRows;
    }
    
    public static bool gameOver()
    {
        int y = 19;
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                Debug.Log("Game over");
                return true;
            }
        }
        return false;
    }

    public static void SumScore(int numberOfFullRows)
    {
        if (numberOfFullRows == 1) Score += 40 * (level + 1);
        else if (numberOfFullRows == 2) Score += 100 * (level + 1);
        else if (numberOfFullRows == 3) Score += 300 * (level + 1);
        else if (numberOfFullRows == 4) Score += 1200 * (level + 1);
        Debug.Log("Pontuação: "+ Score);
    }

    // level is incremented after every 10 lines completed
    public static void IncreaseLevel() => level++;

}
