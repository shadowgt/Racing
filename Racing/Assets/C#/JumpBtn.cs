using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpBtn : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    private GameObject GameObj;
    private Rigidbody rb;

    private bool bPressed = false;

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
        bPressed = false;
    }
    // Use this for initialization
    void Start () {
        GameObj = GameObject.Find("sedan1");
    }

    // Update is called once per frame
    void Update()
    {
        if (bPressed == true)
        {
            GameObj = GameObject.Find("sedan1");
            Rigidbody rb = GameObj.GetComponent<Rigidbody>();
            GameObj.transform.Translate(Vector3.up * 10);


            if (GameObj.transform.rotation.x < 50)
            {
                GameObj.transform.Rotate(-1, 0, 0);
            }

            if (GameObj.transform.position.y > 100)
            {
                bPressed = false;
            }

        }
        else
        {
            if (GameObj.transform.rotation.x < 15 && GameObj.transform.position.y > 50)
            {
                GameObj.transform.Rotate(1, 0, 0);
            }
        }
    }

}
