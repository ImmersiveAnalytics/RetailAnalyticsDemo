using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf1Script : MonoBehaviour {


    public GameObject myBarchart;
    public Material myChartMaterial;
    bool ShowBarChart = false;

    // Use this for initialization
    void Start ()
    {
        myBarchart.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SUCCESS COLLLIDED " + collision.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (ShowBarChart == false)
        {
            ShowBarChart = true;
            myBarchart.SetActive(true);
        }

        Vuforia.RetailItemScript productinfo = other.gameObject.transform.parent.GetComponent<Vuforia.RetailItemScript>();
        if (productinfo.GetProductName() == "")
        {
            productinfo.filtProd();
            waitandaddrequest(other);
        }
        else
        {
            addDataToBar(productinfo.GetProductName(), productinfo.GetSales());
        }
    }

    IEnumerator waitandaddrequest(Collider other)
    {
        Vuforia.RetailItemScript productinfo = other.gameObject.transform.parent.GetComponent<Vuforia.RetailItemScript>();
        yield return new WaitForSeconds(1);
        addDataToBar(productinfo.GetProductName(), productinfo.GetSales());
    }

    void OnTriggerExit(Collider other)
    {
        AddBarData myBarScript = myBarchart.GetComponent<AddBarData>();
        Vuforia.RetailItemScript productinfo = other.gameObject.transform.parent.GetComponent<Vuforia.RetailItemScript>();
        myBarScript.RemoveCategory(productinfo.GetProductName());
        if(myBarScript.getAmountOfCategories() == 0)
        {
            ShowBarChart = false;
            myBarchart.SetActive(false);
        }
        Debug.Log("SUCCESS Removed");
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
