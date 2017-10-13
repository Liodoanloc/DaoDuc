using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlerMonkey : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	public void cry()
    {
        anim.SetBool("isCry",true);
    }

    public void smile()
    {
        anim.SetBool("isCry", false);
    }
}
