using UnityEngine;
using System.Collections;

public class HandleBtn : MonoBehaviour
{
    private Vector2 HandleBtnStartPos, HandleBtnAltPos;
    // Use this for initialization
    void Start()
    {
        HandleBtnStartPos = HandleBtnAltPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int nCount = Input.touchCount;

        ///////////////////////////
        Touch touch;
        if (nCount > 0)
        {
            for (int i = 0; i <= nCount; i++)
            {
                if (i == 0) // 핸들 터치관련
                {
                    touch = Input.GetTouch(0);

                    Debug.Log("DEBUG Position touch.position.y = " + touch.position.y);
                    Debug.Log("DEBUG Position touch.position.x = " + touch.position.x);


                    if (HandleBtnStartPos != touch.position)
                    {
                        this.transform.position = Vector2.Lerp(HandleBtnAltPos, touch.position ,10);
                    }
                    HandleBtnAltPos = this.transform.position;


                    if (TouchPhase.Ended == touch.phase)
                    {
                        this.transform.position = Vector2.Lerp(HandleBtnAltPos, HandleBtnStartPos, 20);
                    }
                    
                }
            }
        }
    }
}