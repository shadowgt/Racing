using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
    
    bool TriggerChk = false;
    Vector3 StartPos;

    public GameManager gameManager {get; set;}

    // Use this for initialization
    void Start () {
        StartPos = gameObject.transform.position;
    }

    void OnTriggerEnter(Collider coll)
    {

        TriggerChk = true;
        //AudioSource.PlayClipAtPoint(snd, transform.position);
        //Destroy(coll.gameObject);
        //Destroy(gameObject);
    }

    void OnTriggerExit(Collider coll)
    {
        gameManager.signale(this.gameObject);
    }

    // Update is called once per frame
    void Update () {

        if (TriggerChk == true)
        {
            gameObject.transform.Translate(new Vector3(0, 1, 0));
            if ((gameObject.transform.position.y - StartPos.y) > 50)
            {
                TriggerChk = false;
                gameObject.transform.position = StartPos;
            }
        }

        transform.Rotate(new Vector3(0, 1, 0));
    }
}
