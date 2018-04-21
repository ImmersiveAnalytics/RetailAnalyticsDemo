using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ClearButtonScript : MonoBehaviour, IInputClickHandler, IInputHandler
{

    public GameObject myRack;
    public GameObject myRetailTargets;

	// Use this for initialization
	void Start () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        RackScript myRackScript = myRack.GetComponent<RackScript>();
        myRackScript.clearShelfData();
        TargetsScript myTargetsScript = myRetailTargets.GetComponent<TargetsScript>();
        myTargetsScript.clearRetailItemsData();
    }
    public void OnInputDown(InputEventData eventData)
    {

    }
    public void OnInputUp(InputEventData eventData)
    {

    }


    // Update is called once per frame
    void Update () {
		
	}
}
