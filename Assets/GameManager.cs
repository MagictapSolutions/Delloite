using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject countrypage;
    public GameObject details;
    public GameObject india;
    public GameObject Navigatorstate;
    public GameObject Navigatorcentral;
    public GameObject navigatornew;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButton()
    {
        countrypage.SetActive(true);
        details.SetActive(false);
    }
    public void India()
    {
        india.SetActive(true);
        countrypage.SetActive(false);
        details.SetActive(false);
    }
    public void navigatorState()
    {
        india.SetActive(false);
        Navigatorstate.SetActive(true);
        navigatornew.SetActive(false);
        Navigatorcentral.SetActive(false);
       
     
        countrypage.SetActive(false);
        details.SetActive(false);
    }
    public void navigatorNew()
    {
        india.SetActive(false);
        navigatornew.SetActive(true);
        Navigatorstate.SetActive(false);
        Navigatorcentral.SetActive(false);

       
        countrypage.SetActive(false);
        details.SetActive(false);
    }

    public void navigatorCentral()
    {
        india.SetActive(false);
        Navigatorcentral.SetActive(true);
        navigatornew.SetActive(false);
        Navigatorstate.SetActive(false);
      
        countrypage.SetActive(false);
        details.SetActive(false);
    }

    public void onHome()
    {
        countrypage.SetActive(true);
        navigatornew.SetActive(false);
        Navigatorcentral.SetActive(false);
        Navigatorstate.SetActive(false);
        india.SetActive(false);
       
        details.SetActive(false);
    }
}
