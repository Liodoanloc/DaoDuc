using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlerDangNhapDangKy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (cnn.CheckExist("select count(*) from NguoiDung where TenDangNhap='1' and  MatKhau = '1'") == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("1_Menu");
            tbTen.SetActive(false);
            btHoanThanh.SetActive(false);
        }
        else
        {
            tbTen.SetActive(true);
            btHoanThanh.SetActive(true);
        }
    }


    public Text txtMk, txtXNMk, txtTenDN, txtTen,   tbNhapTen;
    public Toggle lv1, lv2, lv3, lv4, lv5, lvKhac;
    int lv = 1;
    public static string idFB, userName;
    public GameObject cvTBLoi, tbTen, btHoanThanh;
    public Text txtNDLoi, nameBT;
    

    ConnectDatabase cnn = new ConnectDatabase();

    public void checklv1()
    {
        lv2.isOn = lv3.isOn = lv4.isOn = lv5.isOn = lvKhac.isOn = false;
        lv = 1;
    }
    public void checklv2()
    {
        lv1.isOn = lv3.isOn = lv4.isOn = lv5.isOn = lvKhac.isOn = false;
        lv = 2;
    }
    public void checklv3()
    {
        lv2.isOn = lv1.isOn = lv4.isOn = lv5.isOn = lvKhac.isOn = false;
        lv = 3;
    }
    public void checklv4()
    {
        lv2.isOn = lv3.isOn = lv1.isOn = lv5.isOn = lvKhac.isOn = false;
        lv = 4;
    }
    public void checklv5()
    {
        lv2.isOn = lv3.isOn = lv4.isOn = lv1.isOn = lvKhac.isOn = false;
        lv = 5;
    }
    public void checklvKhac()
    {
        lv2.isOn = lv3.isOn = lv4.isOn = lv5.isOn = lv1.isOn = false;
        lv = 5;
    }

    string checkTT()
    {
        if (txtTenDN.text == "")
            return "Xin hãy nhập tên đang nhập";
        if (cnn.CheckExist("select count(*) from NguoiDung where TenDangNhap='" + txtTenDN.text + "'") != 0)
            return "Tên này đã tồn tại, xin hãy chọn tên đang nhập khác";
        if (txtMk.text == "")
            return "Xin hãy nhập mật khẩu";
        if (txtXNMk.text == "")
            return "Xin hãy nhập mật khẩu xác nhận";
        if (txtMk.text != txtXNMk.text)
            return "Mật khẩu không trùng khớp, vui lòng thử lại.";
        if (lv1.isOn == false && lv2.isOn == false && lv3.isOn == false && lv4.isOn == false && lv5.isOn == false && lvKhac.isOn == false)
            return "Vui lòng chọn cấp độ của bạn.";
        return "";
    }

    string checkTTDKThuong()
    {
        if (txtTen.text == "")
            return "Xin hãy nhập tên của bạn.";
        if (txtTenDN.text == "")
            return "Xin hãy nhập tên đang nhập.";
        if (cnn.CheckExist("select count(*) from NguoiDung where TenDangNhap='" + txtTenDN.text + "'") != 0)
            return "Tên này đã tồn tại, xin hãy chọn tên đang nhập khác";
        if (txtMk.text == "")
            return "Xin hãy nhập mật khẩu";
        if (txtXNMk.text == "")
            return "Xin hãy nhập mật khẩu xác nhận";
        if (txtMk.text != txtXNMk.text)
            return "Mật khẩu không trùng khớp, vui lòng thử lại.";
        if (lv1.isOn == false && lv2.isOn == false && lv3.isOn == false && lv4.isOn == false && lv5.isOn == false && lvKhac.isOn == false)
            return "Vui lòng chọn cấp độ của bạn.";
        return "";
    }

    public void DangKy()
    {
        string tb = checkTT();
        if (tb == "")
        {
            if (cnn.InsertUpdateUser("insert into NguoiDung values ('" + txtTenDN.text + "','" + userName + "'," + 1 + "," + lv + "," + 0 + ",'" + txtMk.text + "','" + idFB + "')"))
            {
                //set cấp độ
                FbManager.user = new User();
                FbManager.user.getInfo(txtTenDN.text);
                txtNDLoi.text = "Chào mừng "+userName+" đến với thế giới thần tiên..";
                nameBT.text = "Tiếp tục";
                cvTBLoi.SetActive(true);
            }
            else
            {
                txtNDLoi.text = "Đã xãy ra xự cố, xin vui lòng thử lại..";
                nameBT.text = "Quay lại";
                cvTBLoi.SetActive(true);
            }
        }
        else
        {
            txtNDLoi.text = tb;
            nameBT.text = "Quay lại";
            cvTBLoi.SetActive(true);
        }
    }

    public void DangKyThuong()
    {
        string tb = checkTTDKThuong();
        if (tb == "")
        {
            if (cnn.InsertUpdateUser("insert into NguoiDung values ('" + txtTenDN.text + "','" + txtTen.text + "'," + 1 + "," + lv + "," + 0 + ",'" + txtMk.text + "','NULL')"))
            {
                //set cấp độ
                FbManager.user = new User();
                FbManager.user.getInfo(txtTenDN.text);
                txtNDLoi.text = "Chào mừng " + txtTen.text + " đến với thế giới thần tiên..";
                nameBT.text = "Tiếp tục";
                cvTBLoi.SetActive(true);
            }
            else
            {
                txtNDLoi.text = "Đã xãy ra xự cố, xin vui lòng thử lại..";
                nameBT.text = "Quay lại";
                cvTBLoi.SetActive(true);
            }
        }
        else
        {
            txtNDLoi.text = tb;
            nameBT.text = "Quay lại";
            cvTBLoi.SetActive(true);
        }
    }

    public void QuayLai()
    {
        if(nameBT.text == "Tiếp tục")
            UnityEngine.SceneManagement.SceneManager.LoadScene("1_Menu");
        else
            cvTBLoi.SetActive(false);

    }

    public void NhapTen()
    {
        if(tbNhapTen.text == "")
        {
            txtNDLoi.text = "Vui lòng nhập tên";
            nameBT.text = "Quay lại";
            cvTBLoi.SetActive(true);
            return;
        }
        if (cnn.InsertUpdateUser("insert into NguoiDung values ('1','" + tbNhapTen.text + "', 1 , 0 , 0 ,'1','NULL')"))
        {
            txtNDLoi.text = "Chào mừng " + tbNhapTen.text + " đến với thế giới thần tiên..";
            nameBT.text = "Tiếp tục";
            cvTBLoi.SetActive(true);
        }
        else
        {
            txtNDLoi.text = "Đã xãy ra xự cố, xin vui lòng thử lại..";
            nameBT.text = "Quay lại";
            cvTBLoi.SetActive(true);
        }
    }
}
