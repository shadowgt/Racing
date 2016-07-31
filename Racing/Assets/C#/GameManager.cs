using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Data;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    public GameObject CubePrefab;
    public GameObject ScoreTitelPrefab, ScoreColumPrefab, ScoresPrefab;
    private GameObject CubeObj , CubeObj1 , CubeObj2 , CubeObj3, CubeObj4;
    private GameObject ScoreTitelObj, ScoreColumObj, ScoresObj;
    
    // 선택된 자동차 관련
    public GameObject m_SedanPrefab, m_CowPrefab, m_TankPrefab;
    public GameObject m_MyCar { get; set; }

    private bool b1, b2, b3, b4 = false;
    public bool bGameEnd = false;
    private int MSec = 0;
    public Transform ScoreBaseParent;
    public Transform CanvasParent;

    public int Score { get; set; }
    public string Name { get; set; }
    public string Time { get; set; }

    // Use this for initialization
    void Start () {
        CubeObj = Instantiate(CubePrefab);
        CubeObj1 = Instantiate(CubePrefab);
        CubeObj2 = Instantiate(CubePrefab);
        CubeObj3 = Instantiate(CubePrefab);
        CubeObj4 = Instantiate(CubePrefab);

        CubeObj.transform.position =new Vector3(0, 40, 0);
        CubeObj1.transform.position = new Vector3(180, 10, 180);
        CubeObj2.transform.position = new Vector3(-180, 10, 180);
        CubeObj3.transform.position = new Vector3(180, 10, -180);
        CubeObj4.transform.position = new Vector3(-180, 10, -180);

        Cube cube = CubeObj.GetComponent<Cube>();
        cube.gameManager = this;
        cube = CubeObj1.GetComponent<Cube>();
        cube.gameManager = this;
        cube = CubeObj2.GetComponent<Cube>();
        cube.gameManager = this;
        cube = CubeObj3.GetComponent<Cube>();
        cube.gameManager = this;
        cube = CubeObj4.GetComponent<Cube>();
        cube.gameManager = this;


        /////////////////////////////////////////// 여기서 부터 선택된 자동차 찾고 보여주기

        dbAccess db = GetComponent<dbAccess>();
        if (db == null)
        {
            Debug.Log("DEBUG: GetComponent ist Null");
        }

        db.OpenDB("race_db.sqlite");

        IDataReader reader = db.BasicQuery("SELECT * FROM my_car_tb");
        m_MyCar = null;
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
                    m_MyCar = Instantiate(m_SedanPrefab);
                    break;
                case 1:
                    m_MyCar = Instantiate(m_CowPrefab);
                    break;
                case 2:
                    m_MyCar = Instantiate(m_TankPrefab);
                    break;
                default:
                    m_MyCar = Instantiate(m_SedanPrefab);
                    break;
            }

            m_MyCar.transform.position = new Vector3(0, 0, 0);

            if (carType == 1)
            {
                m_MyCar.transform.localScale = new Vector3(20, 20, 20);
            }
            else if (carType == 2)
            {
                m_MyCar.transform.localScale = new Vector3((float)0.5, (float)0.5, (float)0.5);
            }
            else
            { }

        }

        GameObject MainCameraObj = GameObject.Find("Main Camera");
        MainCameraObj.transform.SetParent(m_MyCar.transform);

        db.CloseDB();
    }


    public void signale(GameObject i_obj)
    {
        
        if (CubeObj == i_obj 
            && b1 == true && b2 == true && b3 == true && b4 == true )
        {
            Debug.Log("DEBUG: if (CubeObj == i_obj && b1 == true && b2 == true && b3 == true && b4 == true)");
            
            ShowScore();
            b1 = b2 = b3 = b4 = false;
            bGameEnd = true;

        }
        else if (CubeObj1 == i_obj)
        {
            Debug.Log("DEBUG: b1");
            b1 = true;
        }
        else if (CubeObj2 == i_obj)
        {
            Debug.Log("DEBUG: b2");
            b2 = true;
        }
        else if (CubeObj3 == i_obj)
        {
            Debug.Log("DEBUG: b3");
            b3 = true;
        }
        else if (CubeObj4 == i_obj)
        {
            Debug.Log("DEBUG: b4");
            b4 = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (bGameEnd == true)
        {
            MSec++;
            if (MSec > 400)
            {
                GameDataManager GameDataMng = GetComponent<GameDataManager>();
                if (GameDataMng != null)
                    GameDataMng.SaveData();
                SceneManager.LoadScene(0);
            }
        }


        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void GameEnd()
    {
        ShowScore();
        b1 = b2 = b3 = b4 = false;
        bGameEnd = true;
    }

    private void ShowScore()
    {
        ScoreTitelObj = Instantiate(ScoreTitelPrefab);
        ScoreColumObj = Instantiate(ScoreColumPrefab);

        ScoreTitelObj.transform.SetParent(CanvasParent);
        ScoreTitelObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        ScoreColumObj.transform.SetParent(ScoreBaseParent);
        ScoreColumObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);


        dbAccess db = GetComponent<dbAccess>();
        if (db == null)
        {
            Debug.Log("DEBUG: GetComponent ist Null");
        }

        Debug.Log("DEBUG: GetComponent");
        db.OpenDB("race_db.sqlite");
        db.BasicQuery("DELETE FROM score_tb WHERE name='shadowgt22'");
        /*
        db.InsertScore("minakim","3000","1:00:00");
        db.InsertScore("sangbong", "4000", "3:00:00");
        */
        Debug.Log("DEBUG: OpenDB");
        IDataReader reader = db.BasicQuery("SELECT * FROM score_tb ORDER BY score DESC");


        //ScoreScript Score = GetComponent<ScoreScript>();
        GameDataManager DataMng = GetComponent<GameDataManager>();
        DataMng.GetScore(); //  Name , Time Score 가 세팅 된다.

        // 기존 데이터를 가져와서 List 에 저장합니다.
        List<HighScore> ScoreList = new List<HighScore>();
        int i = 0;
        int k = 1;

        bool bNew = false;
        while (reader.Read())
        {
            if (Score >= reader.GetInt32(2) && bNew != true)
            {
                Debug.Log("DEBUG: i=" + i + " , k=" + k);
                ScoreList.Add(new HighScore(Score.ToString(), Name, Time, (i + k), true));
                k++;
                ScoreList.Add(new HighScore(reader.GetInt32(2).ToString(), reader.GetString(0), Time, (i + k)));
                bNew = true;
            }
            else
            {
                Debug.Log("DEBUG: i=" + i + " , k=" + k);
                ScoreList.Add(new HighScore(reader.GetInt32(2).ToString(), reader.GetString(0), Time, (i + k)));
            }

            i++;
        }

        if (bNew == false)
        {
            ScoreList.Add(new HighScore(reader.GetInt32(2).ToString(), reader.GetString(0), Time, (i + k)));
        }

        
       
        // 새 Score 의 순위를 계산하고 빨간색으로 표시합니다. 기존데이터는 검은색
        bNew = false;
        for (int j = 0; j < ScoreList.Count; j++)
        {
            Debug.Log("DEBUG: while (reader.Read())  i = " + j);

            GameObject tmpObject = Instantiate(ScoresPrefab);


            if (ScoreList[j].isNew)
            {
                bNew = true;
                tmpObject.GetComponent<ScoreScript>().ShowNewScore(ScoreList[j].Name, ScoreList[j].Score, "#" + ScoreList[j].Rank);
            }
            else
            {
                if (j > 9)
                {
                    if (bNew == true)
                        break;
                    else
                        continue;
                }

                tmpObject.GetComponent<ScoreScript>().ShowScore(ScoreList[j].Name, ScoreList[j].Score, "#" + ScoreList[j].Rank);
            }

            tmpObject.transform.SetParent(ScoreBaseParent);
            tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        db.CloseDB();
    }
}
