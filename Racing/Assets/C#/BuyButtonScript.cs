using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyButtonScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData test)
    {
        GameObject ShopMngObj = GameObject.Find("ShopManager");
        dbAccess db = ShopMngObj.GetComponent<dbAccess>();
        Debug.Log("DEBUG: GetComponent");
        db.OpenDB("race_db.sqlite");
        /*
        db.BasicQuery("CREATE TABLE my_car_tb " +
                    "(is_select BOOL," +
                    "car_type INT NOT NULL," +
                    "speed TEXT," +
                    "aceleration INT," +
                    "handling INT," +
                    "energy INT)");
                    */


        db.BasicQuery("INSERT INTO my_car_tb(is_select, car_type, speed,aceleration,handling,energy) VALUES('" + 0.ToString() + 
                                                                                                "', '" + ShopMngObj.GetComponent<ShopManagerScript>().m_Position + 
                                                                                                "', '" + 100+
                                                                                                "', '" + 100 +
                                                                                                "', '" + 100 +
                                                                                                "', '" + 100 +
                                                                                                "')");
        


        db.CloseDB();


        SceneManager.LoadScene(2);
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
