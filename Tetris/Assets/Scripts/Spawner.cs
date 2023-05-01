using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public GameObject[] groups;
    private List<GameObject> bag = new List<GameObject>();
    public Text completedLines;
    public Text level;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SoundEffects>().LevelMusic();
        spawnNext();
    }

    private void Update()
    {
        completedLines.text = "Lines" + Environment.NewLine + Playfield.FullLines.ToString();
        level.text = "Level" + Environment.NewLine + Playfield.level.ToString();
        score.text = "Score" + Environment.NewLine + Playfield.Score.ToString();
    }

    public void spawnNext()
    {
        //generate random index
        //int i = Random.Range(0, groups.Length);
        //Debug.Log("O valor de i é:" + i);
        //spawn group at current position, instantiate throw pieces at the world
        Instantiate(SelectPiece(), transform.position, Quaternion.identity);
    }

    public GameObject SelectPiece()
    {
        if (bag.Count == 0)
        {
            bag.AddRange(groups);
        }
        int index = UnityEngine.Random.Range(0, bag.Count);
        GameObject p = bag[index];
        bag.RemoveAt(index);
        return p;
    }


}
