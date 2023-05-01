using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group : MonoBehaviour
{
    //time since last gravity tick
    float lastFall = 0;
    float timeOfFall = 1f;
    int localLevel = 0;
    int numberOfFullRows;

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            //not inside border?
            if (!Playfield.insideBorder(v)) return false;

            // Block in grid cell (and not part of same group)?
            if (Playfield.grid[(int)v.x, (int)v.y] != null &&
                Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }


    void Start()
    {
        if (!isValidGridPos())
        {
            FindObjectOfType<SoundEffects>().DeathSound();
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
    }


    void Update()
    {
        //Debug.Log(message: "Linhas completas: " +Playfield.FullLines);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //modify position
            transform.position += new Vector3(-1, 0, 0);

            //check if it is valid, if so update the grid
            if (isValidGridPos())
                updateGrid();
            //if not, reverse.
            else
                transform.position += new Vector3(1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (isValidGridPos())
                updateGrid();
            else
                transform.position += new Vector3(-1, 0, 0);
        }
        
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            if (isValidGridPos())
            {
                FindObjectOfType<SoundEffects>().MoveSound();
                updateGrid();
            }
            else
                transform.Rotate(0, 0, 90);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= timeOfFall)
        {
            transform.position += new Vector3(0, -1 , 0);
            //see if is valid
            if (isValidGridPos())
                updateGrid();
            //if it is not valid reverse changes
            else
            {
                transform.position += new Vector3(0, 1, 0);

                //clear filled horizontal lines
                numberOfFullRows = Playfield.deleteFullRows();
                //Debug.Log("Quantidade de linhas completas:"+numberOfFullRows);

                if (Playfield.gameOver())
                {
                    Destroy(gameObject);
                }

                //spawn next pice
                FindObjectOfType<Spawner>().spawnNext();

                enabled = false;
            }

            lastFall = Time.time;
        }

        if (Playfield.level != localLevel)
        {
            timeOfFall = timeOfFall / 3;
            localLevel = Playfield.level;
        }

    }

    void updateGrid()
    {
        //remove old children from grid
        for (int y = 0; y < Playfield.h; ++y)
            for (int x = 0; x < Playfield.w; ++x)
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == transform)
                        Playfield.grid[x, y] = null;

        //add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;

        }

    }
}
