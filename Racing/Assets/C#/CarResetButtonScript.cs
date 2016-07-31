using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;

public class CarResetButtonScript : MonoBehaviour , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData test)
    {
        GameObject GarageMngObj = GameObject.Find("GarageManager");
        dbAccess db = GarageMngObj.GetComponent<dbAccess>();
        Debug.Log("DEBUG: GetComponent");
        db.OpenDB("race_db.sqlite");

        db.BasicQuery("DELETE FROM my_car_tb");

        db.CloseDB();
        SceneManager.LoadScene(2);
        //GarageMngObj.GetComponent<GarageManagerScript>().Start();
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
