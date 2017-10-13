using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerGiaiTri : MonoBehaviour {

	public void NgheNhac()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=Uq-jX0xmVN4&list=RDUq-jX0xmVN4&t=1");
    }

    public void KeChuyen()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=5zpVOCtuR_c&list=PLVOZ_45NZhu4MzzwyKcIQjeJCZ0wRqTY0");
    }

    public void XemVideo()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=7e82Wwg-7vw&list=PLLHdq4EIIq8Hw-YTNkJOSggr_tbCDTiuk");
    }

    public void ChoiTroChoi()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TroChoi");
    }

    public void back()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        Application.LoadLevel("1_Menu");
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
