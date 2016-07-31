using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarSelectBtnScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData test)
    {
        GameObject GarageMngObj = GameObject.Find("GarageManager");
        dbAccess db = GarageMngObj.GetComponent<dbAccess>();
        Debug.Log("DEBUG: GetComponent");
        db.OpenDB("race_db.sqlite");

        db.BasicQuery("UPDATE my_car_tb SET is_select=0");
        int nTmp = GarageMngObj.GetComponent<GarageManagerScript>().m_Position+1;
        Debug.Log("DEBUG: rowid  =" + nTmp);
        
        db.BasicQuery("UPDATE my_car_tb SET is_select=1 WHERE rowid="+ nTmp );
        db.CloseDB();
        SceneManager.LoadScene(0);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
