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

    void SetRackPosition(Vector3 newPosition)
    {
        Rack.transform.position.Set(newPosition.x, newPosition.y, newPosition.z);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
