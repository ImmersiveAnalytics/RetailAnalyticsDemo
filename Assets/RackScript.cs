using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackScript : MonoBehaviour {

    public GameObject Rack;
    // Use this for initialization
    void Awake() {
        

    }

    void Start () {
		
	}

    public void clearShelfData()
    {
        ShelfScript[] shelflist = GetComponentsInChildren<ShelfScript>();

        foreach (ShelfScript shelf in shelflist)
        {
            shelf.clearShelfData();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
