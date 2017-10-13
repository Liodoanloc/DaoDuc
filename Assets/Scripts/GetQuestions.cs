using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetQuestions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public static string topic = "";
    public static string lop = "";


    public void ChuDe(Text TextTopic)
    {
        topic = TextTopic.text;
    }
    public void Lop(Text TextLop)
    {
        lop = TextLop.text;
        UnityEngine.SceneManagement.SceneManager.LoadScene("3_TraLoi");
    }


    public void ChuDeStr(string chude)
    {
        topic = chude;
    }

    public void LopStr( string l)
    {
        lop = l;
        UnityEngine.SceneManagement.SceneManager.LoadScene("3_TraLoi");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
