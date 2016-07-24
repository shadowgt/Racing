using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour {

    public GameObject StartCountTextObj , TimeTextObj , PointTextObj;
    private int i = 3;
    private int TimeCount = 0;
    private int Min,Sec,MliSec = 0;
    private int GamePoint = 100000;

    // Use this for initialization
    void Start () {
	
	}

    public void GetScore()
    {
        GameManager GameMng = GetComponent<GameManager>();
        GameMng.Name = "shadowgt223";
        GameMng.Score = GamePoint;
        GameMng.Time = Min.ToString("D2") + ":" + Sec.ToString("D2") + ":" + MliSec.ToString("D2");
    }

    public void SaveData()
    {
        Debug.Log("DEBUG:  SaveData() ");
        dbAccess db = GetComponent<dbAccess>();
    
        db.OpenDB("race_db.sqlite");
        db.InsertScore("shadowgt223", GamePoint.ToString(), Min.ToString("D2") +":"+ Sec.ToString("D2") + ":" + MliSec.ToString("D2"));
        db.CloseDB();
        
        Debug.Log("DEBUG:  SaveEnd ");
    }
	
	// Update is called once per frame
	void Update () {
        // 1초를 60 프레임이라고 가정하고 초를 구한다.
        if (TimeCount == 60 && 0 <= i)
        {
            this.StartCountTextObj.GetComponent<Text>().text = i.ToString();
            i--;
            TimeCount = 0;
        }
        else if (i < 0)
        {
            if (this.StartCountTextObj.active)
            {
                this.StartCountTextObj.active = false ;
            }
            // 밀리 초 마다 -1 포인트
            this.PointTextObj.GetComponent<Text>().text = GamePoint.ToString() + "P";
            GamePoint--;

            string time = Min.ToString("D2") + ":" + Sec.ToString("D2") + ":" + MliSec.ToString("D2");
            this.TimeTextObj.GetComponent<Text>().text = time;
            // 1초를 60 프레임이라고 가정하고 초를 구한다.
            if (MliSec == 60)
            {
                MliSec = 0;
                Sec++;
                if (Sec == 60)
                {
                    Sec = 0;
                    Min++;
                    if (Min == 60)
                    {
                        // 게임종료
                    }
                }
            }

            MliSec++;
        }
        TimeCount++;
        
    }
}
