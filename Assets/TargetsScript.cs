using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void clearRetailItemsData()
    {
        Vuforia.RetailItemScript[] retaillist = GetComponentsInChildren<Vuforia.RetailItemScript>();

        foreach (Vuforia.RetailItemScript retailitem in retaillist)
        {
            retailitem.clearText();
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
