using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject cv;

    public void goi()
    {
        cv.SetActive(true);
    }
    public void tat()
    {
        cv.SetActive(false);
    }
}
