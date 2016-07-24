using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreScript : MonoBehaviour {

    public GameObject NameObj;
    public GameObject ScoreObj;
    public GameObject RankObj;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowScore(string i_name , string i_score , string i_rank)
    {
        //Debug.Log("DEBUG: result.Count :" + i_list.Count);
        this.NameObj.GetComponent<Text>().text = i_name;
        this.ScoreObj.GetComponent<Text>().text = i_score;
        this.RankObj.GetComponent<Text>().text = i_rank;
    }

    public void ShowNewScore(string i_name, string i_score, string i_rank)
    {
        ShowScore(i_name, i_score, i_rank);
        this.NameObj.GetComponent<Text>().color = Color.red;
        this.ScoreObj.GetComponent<Text>().color = Color.red;
        this.RankObj.GetComponent<Text>().color = Color.red;

    }

}
