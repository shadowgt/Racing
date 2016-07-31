using UnityEngine;
using System.Collections;
using System;
using System.Data;


public class GarageManagerScript : MonoBehaviour
{
    Touch m_Touch;
    Vector2 m_StartPos, m_EndPos;
    public int m_Position { get; set; }
    int m_MaxPlace = 0;
    float m_CameraX = 0;
    float m_CameraTargetX = 0;

    public GameObject m_SedanPrefab, m_CowPrefab , m_TankPrefab;
    private GameObject m_SelectBtn, m_BuyBtn;

    bool m_bMoving, m_bIsRight = false;
    GameObject m_CameraObj;
    // Use this for initialization


    void MoveCamera()
    {
        m_CameraObj.transform.position = Vector3.Lerp(new Vector3(m_CameraObj.transform.position.x,
                                                                   m_CameraObj.transform.position.y,
                                                                   m_CameraObj.transform.position.z),
                                                     new Vector3(m_CameraTargetX,
                                                                   m_CameraObj.transform.position.y,
                                                                   m_CameraObj.transform.position.z),
                                                                   Time.deltaTime * 3);

        if (Math.Abs(m_CameraTargetX - m_CameraObj.transform.position.x) < 1)
        {
            m_bMoving = false;
            m_CameraObj.transform.position = new Vector3(m_CameraTargetX,
                                                         m_CameraObj.transform.position.y,
                                                         m_CameraObj.transform.position.z);
            if (m_MaxPlace == m_Position )
            {
                m_BuyBtn.active = true;
                m_SelectBtn.active = false;
            }
            else 
            {
                m_BuyBtn.active = false;
                m_SelectBtn.active = true;
            }


        }
    }


    public void Start()
    {
        m_Position = 0;
        m_SelectBtn = GameObject.Find("SelectButton");
        m_BuyBtn = GameObject.Find("BuyButton");

        m_CameraObj = GameObject.Find("Main Camera");
        dbAccess db = GetComponent<dbAccess>();
        Debug.Log("DEBUG: GetComponent");
        db.OpenDB("race_db.sqlite");
        IDataReader reader = db.BasicQuery("SELECT * FROM my_car_tb");


        //db.BasicQuery("DELETE FROM my_car_tb WHERE is_select=0");


        int i = 0;
        GameObject CarObj = null;
        while (reader.Read())
        {
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

            Rigidbody rd = CarObj.GetComponent<Rigidbody>();
            rd.useGravity = false;

            CarObj.transform.position = new Vector3(0+(115*i) , 0, 0);
            CarObj.transform.Rotate(new Vector3(0,180,0));

            if (carType == 1)
            {
                CarObj.transform.localScale = new Vector3(20,20,20);
            }
            else if (carType == 2)
            {
                CarObj.transform.localScale = new Vector3((float)0.5 , (float)0.5, (float)0.5);
            }
            else
            { }

            i++;
            
        }

            m_MaxPlace = i;

        if (m_MaxPlace != 0)
        {
            m_BuyBtn.active = false;
            m_SelectBtn.active = true;
        }
        else
        {
            m_BuyBtn.active = true;
            m_SelectBtn.active = false;
        }

        db.CloseDB();
    }

    // Update is called once per frame
    void Update()
    {

        if (m_bMoving)
        {
            MoveCamera();
        }

        int nCount = Input.touchCount;
        if (nCount > 0)
        {
            for (int i = 0; i <= nCount; i++)
            {
                if (i == 0) // 핸들 터치관련
                {
                    m_Touch = Input.GetTouch(0);

                    switch (m_Touch.phase)
                    {   // 터치 시작 했을 때 이벤트
                        case TouchPhase.Began:
                            m_StartPos = m_Touch.position;
                            break;
                        // 드래그
                        case TouchPhase.Moved:
                            break;
                        case TouchPhase.Ended:
                            if (m_bMoving == true)
                            {
                                if ((m_bIsRight == true && m_Position == m_MaxPlace) || (m_bIsRight == false && m_Position == 0))
                                    break;
                            }

                            m_EndPos = m_Touch.position;
                            m_bMoving = true;
                            m_CameraX = m_CameraObj.transform.position.x;

                            if (m_StartPos.x > m_EndPos.x)
                            {
                                if (m_Position != m_MaxPlace)
                                {
                                    if (m_Position == 3)
                                    {
                                        m_bMoving = false;
                                    }
                                    else
                                    {
                                        m_bIsRight = true;
                                        m_Position++;
                                        m_CameraTargetX = m_CameraTargetX + 115;
                                    }
                                }
                                else
                                    m_bMoving = false;
                            }
                            else
                            {
                                if (m_Position != 0)
                                {
                                    m_bIsRight = false;
                                    m_Position--;
                                    m_CameraTargetX = m_CameraTargetX - 115;
                                }
                                else
                                    m_bMoving = false;
                            }
                            break;

                    }
                }
            }
        }
    }
}
