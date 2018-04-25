using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTogether : MonoBehaviour {

    public GameObject EthPart;
    public Transform rotator;

	//Use this for initialization
	void Start () {

        EthPart = GameObject.FindWithTag("Tetrahedron");

        rotator = transform;
	}
	
	// Update is called once per frame
	void Update () {

        rotator.rotation = Quaternion.Euler(rotator.eulerAngles.x, rotator.eulerAngles.y, EthPart.transform.eulerAngles.x);

    }
}
