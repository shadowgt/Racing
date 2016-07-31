using UnityEngine;
using System.Collections;
using System;

public class ShopManagerScript : MonoBehaviour {

    Touch m_Touch;
    Vector2 m_StartPos , m_EndPos;
    public int m_Position { get; set; }
    float m_CameraX = 0;
    float m_CameraTargetX = 0;

    bool m_bMoving,m_bIsRight = false;
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
        }
    }


    void Start () {
        m_CameraObj = GameObject.Find("Main Camera");
        m_Position = 0;
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
                                if((m_bIsRight == true && m_Position == 2) || (m_bIsRight == false && m_Position == 0))
                                    break;
                            }

                            m_EndPos = m_Touch.position;
                            m_bMoving = true;
                            m_CameraX = m_CameraObj.transform.position.x;

                            if (m_StartPos.x > m_EndPos.x)
                            {
                                if (m_Position != 2)
                                {
                                    m_bIsRight = true;
                                    m_Position++;
                                    m_CameraTargetX = m_CameraTargetX + 115;
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
