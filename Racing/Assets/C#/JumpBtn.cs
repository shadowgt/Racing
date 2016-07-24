using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class JumpBtn : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    private GameObject GameObj;
    private Rigidbody rb;
    private Touch touch;
    private float JumpLimit = 40.0f;
    private float JumpLimitRotateX = 15.0f;

    private bool bPressed = false;
    private bool bBesthigh = false;

    // 인터페이스 트리거 관련
    public void OnPointerDown(PointerEventData data)
    {
        if (GameObj.transform.position.y < 2)
        {
            bPressed = true;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        //bPressed = false;
    }
    // Use this for initialization
    void Start () {
        GameObj = GameObject.Find("sedan1");
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (bPressed == true && Input.touchCount > 0)
            {
                GameObj = GameObject.Find("sedan1");
                Rigidbody rb = GameObj.GetComponent<Rigidbody>();
                //y = 388.x = 263.9999
                touch = Input.GetTouch(0);

                if (bBesthigh == true) // // 하강중 기울기 조절
                {
                    if (GameObj.transform.position.y > 20)
                    {
                        if (touch.position.y >= 388)
                        {
                            if (GameObj.transform.rotation.x < JumpLimitRotateX)
                            {
                                GameObj.transform.Rotate(30 * Time.deltaTime, 0, 0);
                            }
                        }
                        else
                        {
                            if (GameObj.transform.rotation.x > -JumpLimitRotateX)
                            {
                                GameObj.transform.Rotate(-30 * Time.deltaTime, 0, 0);
                            }
                        }
                    }
                    else
                    {
                        bBesthigh = false;
                        bPressed = false;
                    }

                }
                else // 상승중 기울기 조절
                {
                    GameObj.transform.Translate(Vector3.up * 10);

                    if (touch.position.y >= 388)
                    {
                        if (GameObj.transform.rotation.x > -JumpLimit)
                        {
                            GameObj.transform.Rotate(-1, 0, 0);
                        }
                    }
                    else
                    {
                        if (GameObj.transform.rotation.x < JumpLimit)
                        {
                            GameObj.transform.Rotate(1, 0, 0);
                        }
                    }

                }

                if (GameObj.transform.position.y > 100)
                {
                    bBesthigh = true;
                }
            }
            else
            {

            }

        }
        catch (Exception e)
        {
            Debug.Log("DEBUG: " + e);

        }
    }
}
