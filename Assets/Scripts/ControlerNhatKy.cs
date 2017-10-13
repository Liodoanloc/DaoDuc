using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlerNhatKy : MonoBehaviour {

    public Text txtContent;

    List<NhatKy> nhatKy;

    public void getNoiDung()
    {
        nhatKy = new List<NhatKy>();
        ConnectDatabase cdb = new ConnectDatabase();
        nhatKy = cdb.GetAllNhatKy();
    }
    
    public void setNoiDung()
    {
        string content;
        if( nhatKy.Count != 0)
            txtContent.text = "";
        foreach (NhatKy nk in nhatKy)
        {
            content = nk.Ngay + " : " + nk.NoiDung + ".";
            txtContent.text += content + "\n";
        }
    }
    // Use this for initialization
    void Start () {
        getNoiDung();
        setNoiDung();
	}

    public void back()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        Application.LoadLevel("1_Menu");
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
