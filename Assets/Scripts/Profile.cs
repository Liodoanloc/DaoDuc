using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour {

    // Use this for initialization
    public static User user;
    // Use this for initialization
    public Toggle tgMusic;
    public AudioSource music;
    public static bool OnMusic = true;
    public Text txtName, txtScore, txtLevel, tbDoiTen, txtThongBao;
    public GameObject cvSetting, cvProfile;
    void Start()
    {
        user = new User();
        user.getInfo("1");
        txtLevel.text = user.level + "";
        txtName.text = user.name;
        tbDoiTen.text = user.name;
        txtScore.text = user.score + "";
    }

    ConnectDatabase cnn = new ConnectDatabase();
    public void UpdateName()
    {
        if (tbDoiTen.text == string.Empty)
        {
            txtThongBao.text = "Xin vui lòng nhập tên.";
            return;
        }

        if (cnn.InsertUpdateUser("update NguoiDung set TenNguoiDung = '" + tbDoiTen.text + "'"))
        {
            txtThongBao.text = "Đổi tên thành công.";
        }
        else
            txtThongBao.text = "Đã xãy ra lỗi xin vui lòng thử lại.";
    }

    public void closeSetting()
    {
        cvSetting.SetActive(false);
        cvProfile.SetActive(true);
    }

    public void ShowSetting()
    {
        cvSetting.SetActive(true);
        cvProfile.SetActive(false);
    }

    public void Music()
    {
        if (tgMusic.isOn)
        {
            music.mute = false;
            OnMusic = true;
        }
        else
        {
            music.mute = true;
            OnMusic = false;
        }
    }


}
