using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System;

public class Main : MonoBehaviour 
{
    // 스코어 관련
    private List<HighScore> highScores = new List<HighScore>();
    public GameObject scorePrefab;
    public Transform scoreParent;


    // 선택된 자동차 관련
    public GameObject m_SedanPrefab, m_CowPrefab, m_TankPrefab;

    // Use this for initialization
    void Start () {
        Debug.Log("DEBUG: starting SQLiteLoad app");

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
       // db.BasicQuery("DROP TABLE my_car_tb" );
        /*db.BasicQuery("CREATE TABLE my_car_tb " +
                "(is_select BOOL ," +
                "car_type INT NOT NULL," +
                "speed TEXT ," +
                "aceleration INT ," +
                "handling INT ," +
                "energy INT )");*/
        

         /*db.BasicQuery("CREATE TABLE score_tb "+
                         "(name TEXT NOT NULL, " +
                         "time TEXT NOT NULL,"+
                         "score INT NOT NULL)");*/
         

        //db.OpenDB("race_db.sqlite");
        //db.BasicQuery("DELETE FROM score_tb WHERE name='shadowgt22'");

         //db.InsertScore("minakim","3000","1:00:00");
         // db.InsertScore("sangbong", "4000", "3:00:00");

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

        ////////////////////////////////////// 여기까지 스코어
        /////////////////////////////////////////// 여기서 부터 선택된 자동차 찾고 보여주기
        reader = db.BasicQuery("SELECT * FROM my_car_tb");
        GameObject CarObj = null;
        while (reader.Read())
        {
            if (reader.GetBoolean(0) != true)
                continue;

            //reader.GetInt32(0); // is_select
            //reader.GetInt32(1); // car_type
            int carType = reader.GetInt32(1);
            Debug.Log("DEBUG: switch (carType)");
            Debug.Log("DEBUG: carType : " + carType);

            switch (carType)
            {
                case 0:
                    CarObj = Instantiate(m_SedanPrefab);
                    break;
                case 1:
                    CarObj = Instantiate(m_CowPrefab);
                    break;
                case 2:
                    CarObj = Instantiate(m_TankPrefab);
                    break;
                default:
                    CarObj = Instantiate(m_SedanPrefab);
                    break;
            }

            CarObj.transform.position = new Vector3(0, 0, 0);
            CarObj.transform.Rotate(new Vector3(0, 180, 0));

            if (carType == 1)
            {
                CarObj.transform.localScale = new Vector3(20, 20, 20);
            }
            else if (carType == 2)
            {
                CarObj.transform.localScale = new Vector3((float)0.5, (float)0.5, (float)0.5);
            }
            else
            { }

        }

        Rigidbody rd = CarObj.GetComponent<Rigidbody>();
        rd.useGravity = false;

        db.CloseDB();
    }

	// Update is called once per frame
	void Update () {
		
			
	}
	
	void OnGUI () {
        
        
	}
}
