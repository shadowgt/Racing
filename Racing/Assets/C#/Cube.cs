using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

    bool Chk = false;
    Vector3 StartPos;
	// Use this for initialization
	void Start () {
        StartPos = gameObject.transform.position;
    }

    void OnTriggerEnter(Collider coll)
    {

        Chk = true;
        //AudioSource.PlayClipAtPoint(snd, transform.position);
        //Destroy(coll.gameObject);
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {

        if (Chk == true)
        {
            gameObject.transform.Translate(new Vector3(0, 1, 0));
            if ((gameObject.transform.position.y - StartPos.y) > 50)
            {
                Chk = false;
                gameObject.transform.position = StartPos;
            }
        }
        else
        {
            transform.Rotate(new Vector3 (0,1,0));
        }
    }
}
