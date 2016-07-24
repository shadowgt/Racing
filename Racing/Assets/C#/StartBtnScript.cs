using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartBtnScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData test)
    {
        SceneManager.LoadScene(1);
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
