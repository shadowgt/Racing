using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class MyCar : MonoBehaviour {


    private Vector2 StartPos;
    private Vector3 ObjectPos , StartObjPos ,NewObjPos;
    private float x, y, fAltAngle , fAngle;
    private Rigidbody rb;
    private bool bJump = false;
    private Touch touch;
    private Quaternion newValue, altValue;
    float counter;
    int SpeedCounter = 0;
    private float SpeedHand, AltSpeedHand = 0;
    private Quaternion SpeedHandRolate;

    public bool bPlayed { get; set; } // Mycar 시작 / 정지


    // Use this for initialization
    void Start () {
        StartPos = GameObject.Find("HandleBtn").transform.position;
        
        ObjectPos = transform.position;
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -200.0F, 0);
        SpeedHandRolate = GameObject.Find("SpeedHand").transform.rotation;
        bPlayed = false;
    }

    // Update is called once per frame
    void Update () {
        if (bPlayed == false)
            return;

        int nCount = Input.touchCount;

        // 전복 방지
        if (Math.Abs(transform.rotation.z) > 30)
        {
            if (transform.rotation.z < 0)
                transform.Rotate(new Vector3(0, 0, 10 * Time.deltaTime));
            else
                transform.Rotate(new Vector3(0, 0, -10 * Time.deltaTime));

        }
        //속도 체크
        if ( 0.25 < SpeedCounter * Time.deltaTime)
        {
            CountSpeed();
            SpeedCounter = 0;
        }
        SpeedCounter++;


        ///////////////////////////

        if (nCount > 0)
        {
            for (int i = 0; i <= nCount; i++)
            {
                if (i == 0) // 핸들 터치관련
                {
                    touch = Input.GetTouch(0);


                    switch (touch.phase)
                    {   // 터치 시작 했을 때 이벤트
                        case TouchPhase.Began:
                            //StartPos = touch.position;
                            break;
                        // 드래그
                        case TouchPhase.Moved:

                            Move();
                            

                            //******각도 계산
                            fAngle = Math.Abs((float)(Math.Atan2(y, x) * 180 / 3.1415926535));

                            Debug.Log("DEBUG fAngle = " + fAngle);


                            if (fAngle > 15 && fAngle < 165)
                            {
                                if (transform.position.y < 2) // 점프사용중 회전 금지
                                {
                                    if (y > 0)
                                    {
                                        if (x > 0)
                                            transform.Rotate(Vector3.up * counter);
                                        else
                                            transform.Rotate(-Vector3.up * counter);
                                    }
                                    else
                                    {
                                        if (x > 0)
                                            transform.Rotate(-Vector3.up * counter);
                                        else
                                            transform.Rotate(Vector3.up * counter);
                                    }
                                }
                            }

                            ObjectPos = transform.position;
                            
                            if (counter < 0.5)
                                counter += Time.deltaTime;

                            
                            break;
                        // 터치 종료
                        case TouchPhase.Ended:
                            fAltAngle = 0;
                            counter = 0;
                            break;
                        case TouchPhase.Stationary:

                            Move();
                            /*
                            newValue = Quaternion.LookRotation(touch.position);
                            altValue = Quaternion.LookRotation(StartPos);
                            fAngle = Quaternion.Angle(altValue, newValue); // 각도 계산
                            */

                            fAngle = Math.Abs((float)(Math.Atan2(y, x) * 180 / 3.1415926535));

                            Debug.Log("DEBUG fAngle = " + fAngle);

                            if (transform.position.y < 2) // 점프사용중 회전 금지
                            {
                                if (fAngle >15 && fAngle <165)
                                {
                                    if (y > 0)
                                    {
                                        if (x > 0)
                                            transform.Rotate(Vector3.up * counter);
                                        else
                                            transform.Rotate(-Vector3.up * counter);
                                    }
                                    else
                                    {
                                        if (x > 0)
                                            transform.Rotate(-Vector3.up * counter);
                                        else
                                            transform.Rotate(Vector3.up * counter);
                                    }
                                }
                            }

                            if (counter <0.5)
                                counter += Time.deltaTime;

                            //방향 구한다
                            //Vector3 _dir = (newTouchPos - StartPos).normalized;

                            //transform.Rotate(_dir);
                            //transform.Translate(_dir * 100);
                            ObjectPos = transform.position;
                           
                            break;
                            
                    }
                }
            }
        }
    }


    void CountSpeed()
    {
        NewObjPos = transform.position;
        if (NewObjPos != StartObjPos)
        {

            // 속도 계산 *************************************************
            float distance = Vector2.Distance(new Vector2(StartObjPos.z , StartObjPos.x), new Vector2(NewObjPos.z, NewObjPos.x) );
            double dBuffer = ((distance / Time.deltaTime) / 100) * 1.7;
            float ObjectSpeed = (float)dBuffer;
            Text txt = GameObject.Find("SpeedText").GetComponent<Text>();
            txt.text = (int)ObjectSpeed + "Km/h";
            //*************************************************************

            Debug.Log("DEBUG Position distance = " + distance*100);
            Debug.Log("DEBUG Position ObjectSpeed = " + ObjectSpeed);
            Debug.Log("DEBUG Position Time.deltaTime = " + Time.deltaTime);

            Debug.Log("DEBUG Position StartObjPos.position.z = " + StartObjPos.z);
            Debug.Log("DEBUG Position StartObjPos.position.x = " + StartObjPos.x);

            Debug.Log("DEBUG Position NewObjPos.position.z = " + NewObjPos.z);    // y = 388. x = 263.9999
            Debug.Log("DEBUG Position NewObjPos.position.x = " + NewObjPos.x);

            Quaternion NewQuaternion = Quaternion.Euler(new Vector3(GameObject.Find("SpeedHand").transform.rotation.x,
            GameObject.Find("SpeedHand").transform.rotation.y,
            110 - ObjectSpeed));
            GameObject.Find("SpeedHand").transform.rotation = NewQuaternion;
        }
        else
        {
            Text txt = GameObject.Find("SpeedText").GetComponent<Text>();
            txt.text = 0 + "Km/h";

            GameObject.Find("SpeedHand").transform.rotation = SpeedHandRolate;
            // = SpeedHandRolate;
        }
        StartObjPos = transform.position;
    }

    void Move()
    {
        //********************************** 이동
        int speed = 5;

        x = (touch.position.y - StartPos.y);
        y = (touch.position.x - StartPos.x);
        if (transform.position.y > 2) // 점프사용중 회전 금지
        {
            speed = 2;
        }

        if (transform.position.y > 2 && x < 0) // 점프후진일 경우 움직이지 않음
        {

        }
        else 
            rb.AddRelativeForce(new Vector3(0, 0, x) * speed, ForceMode.Impulse);
        //***************************************
    }

}

