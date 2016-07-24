using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;


public class Main : MonoBehaviour 
{
    private List<HighScore> highScores = new List<HighScore>();
    private string description;

    public GameObject scorePrefab;
    public Transform scoreParent;

	// Use this for initialization
	void Start () {
		Debug.Log("DEBUG: starting SQLiteLoad app");
		
		// Retrieve next word from database
		description = "something went wrong with the database";

        dbAccess db =GetComponent<dbAccess>();
        if (db == null)
        {
            Debug.Log("DEBUG: GetComponent ist Null");
        }

        //test code

        
        /*
        ScoreScript Score1 = GetComponent<ScoreScript>();
            GameObject tmpObject1 = Instantiate(scorePrefab);
            tmpObject1.GetComponent<ScoreScript>().ShowScore("shadowgt","1000","2");
            tmpObject1.transform.SetParent(scoreParent);
            tmpObject1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            return;
            */

        Debug.Log("DEBUG: GetComponent");
        db.OpenDB("race_db.sqlite");
        db.BasicQuery("DELETE FROM score_tb WHERE name='shadowgt22'");
        /*
        db.InsertScore("minakim","3000","1:00:00");
        db.InsertScore("sangbong", "4000", "3:00:00");
        */
        Debug.Log("DEBUG: OpenDB");
        IDataReader reader = db.BasicQuery("SELECT * FROM score_tb ORDER BY score DESC");
        

        ScoreScript Score = GetComponent<ScoreScript>();

        int i = 0;
        while (reader.Read())
        {
            Debug.Log("DEBUG: while (reader.Read())  i = " + i);
            GameObject tmpObject = Instantiate(scorePrefab);
            tmpObject.GetComponent<ScoreScript>().ShowScore(reader.GetString(0), reader.GetInt32(2).ToString(), "#" + (i + 1).ToString());
            tmpObject.transform.SetParent(scoreParent);
            tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            if (i == 9)
            {
                break;
            }
            else
                i++;
        }


        db.CloseDB();
        reader.Close();
        reader.Dispose();
    }

	// Update is called once per frame
	void Update () {
		
			
	}
	
	void OnGUI () {
        
        
	}
}
