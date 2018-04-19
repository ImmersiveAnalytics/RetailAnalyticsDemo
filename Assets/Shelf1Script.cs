using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf1Script : MonoBehaviour {


    public GameObject myBarchart;
    public Material myChartMaterial;
    //public Vuforia.PopScript popScript;
    bool ShowBarChart = false;
    private void Awake()
    {

    }
    // Use this for initialization
    void Start () {
        myBarchart.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SUCCESS COLLLIDED " + collision.gameObject.name);
        if (collision.gameObject.name == "PopcornTarget")
        {

            //EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();

            Debug.Log("SUCCESS COLLLIDED popcorn");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ShowBarChart == false)
        {
            ShowBarChart = true;
            myBarchart.SetActive(true);
        }

        Vuforia.PopScript productinfo = other.gameObject.transform.parent.GetComponent<Vuforia.PopScript>();

        //addDataToBar(other.gameObject.name, 2293802);
        addDataToBar(productinfo.GetProductName(), productinfo.GetSales());
        
        Debug.Log("SUCCESS TRIGGERD popcorn");
        
    }

    void OnTriggerExit(Collider other)
    {
        AddBarData myBarScript = myBarchart.GetComponent<AddBarData>();
        Vuforia.PopScript productinfo = other.gameObject.transform.parent.GetComponent<Vuforia.PopScript>();
        myBarScript.RemoveCategory(productinfo.GetProductName());
        if(myBarScript.getAmountOfCategories() == 0)
        {
            ShowBarChart = false;
            myBarchart.SetActive(false);
        }
        Debug.Log("SUCCESS Removed popcorn");
        
    }

    void addDataToBar(string prodName, float salesAmount)
    {
        AddBarData myBarScript = myBarchart.GetComponent<AddBarData>();
        myBarScript.AddCategory(prodName, myChartMaterial);
        myBarScript.SetValue(prodName, salesAmount);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
