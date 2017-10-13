using System.Collections;
using UnityEngine;

public class ChuyenManHinh : MonoBehaviour { 



    public void StartApp ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("1_Menu");
    }



    public void TheGioiThanTien()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("2_TheGioiThanTien");//cách mới
        
        //Application.LoadLevel("2_TheGioiThanTien");//cách cũ
    }

    public void TraLoiCauHoi ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("3_TraLoiCauhoi");
    }

    public void xemNhatKy()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("NhatKy");
    }

    public void xemVideo()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("XemVideo");
    }
    public void GioiThieu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GioiThieuTacGia");
    }
    public void Thoat()
    {
        Application.Quit();
    }

}
