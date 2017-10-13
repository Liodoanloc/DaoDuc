using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FbManager : MonoBehaviour {

    // Use this for initialization

    public Text _name, _idFb;
    public GameObject canvasDangKyByIDFB;
    public GameObject canvasDangNhap;
    public GameObject canvasTBLoi;
    public Text txtTenDN, txtNDLoi, nameBT;
    public InputField txtMK;
    public GameObject cvDangKy;

    public static User user;

    void Start () {
        //canvasDangNhap.SetActive(true);
        //canvasTBLoi.SetActive(false);
        //cvDangKy.SetActive(false);
        //canvasDangKyByIDFB.SetActive(false);
        //FB.Init(onInitComplete,onHideUnity);
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                canvasDangNhap.SetActive(true);
                cvDangKy.SetActive(false);
                canvasTBLoi.SetActive(false);
                canvasDangKyByIDFB.SetActive(false);
                return;
            }
        }
    }
    ConnectDatabase cnn = new ConnectDatabase();

    private void onHideUnity(bool isUnityShown)
    {
        if (isUnityShown) //ứng dụng được show.
            Time.timeScale = 1;//cho phép dúng dụng chạy
        else
            Time.timeScale = 0;// tạm dừng úng dụng
    }

    private void onInitComplete()
    {
        print("FB on init complete");
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            Debug.Log(aToken.UserId);
            foreach (string perm in aToken.Permissions)
            {
                //lưu vào csdl
                Debug.Log(perm);
            }
        }
    }
    

    public void FBLogin()
    {
        if (FB.IsLoggedIn)                         
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            Debug.Log(aToken.UserId);
            foreach (string perm in aToken.Permissions)
            {
                //lưu vào csdl
                Debug.Log(perm);
            }
        }
        else
        {
            var permission = new List<string>() { "public_profile", "email" };//something you need to get info user            
            FB.LogInWithReadPermissions(permission, AuthLoginCallBack);

        }
    }

    private void AuthLoginCallBack(ILoginResult result)
    {
        if (result.Error != null)
        {
            print(result.Error);
            return;
        }
        FB.API("/me", HttpMethod.GET, GetUserInfoCallBack);

    }

    private void GetUserInfoCallBack(IGraphResult result)
    {
        if(result.Error != null)
        {
            print(result.Error);
            return;
        }
        string userName = result.ResultDictionary["name"].ToString();
        string idFb = result.ResultDictionary["id"].ToString();
        int chk = cnn.CheckExist("select count(*) from NguoiDung where idFaceBook='" + idFb + "'");
        if(chk == 1)
        {
            canvasDangKyByIDFB.SetActive(false);
            UnityEngine.SceneManagement.SceneManager.LoadScene("1_Menu");
        }
        else if( chk == 0)
        {
            _name.text = "Xin Chào: " + userName;
            _idFb.text = "ID Facebook: " + idFb;
            ControlerDangNhapDangKy.userName = userName;
            ControlerDangNhapDangKy.idFB = idFb;
            canvasDangKyByIDFB.SetActive(true);
            canvasDangNhap.SetActive(false);
        }
    }


    string checkTT()
    {
        if (txtTenDN.text == "")
            return "Xin hãy nhập tên đang nhập";
        if (txtMK.text == "")
            return "Xin hãy nhập mật khẩu";

        if (cnn.CheckExist("select count(*) from NguoiDung where TenDangNhap='" + txtTenDN.text + "' and  MatKhau = '" + txtMK.text + "'") == 0)
            return "Tên đang nhập hoặc mật khẩu không đúng...";
        return "";
    }
    public void DangNhap()
    {
        string tb = checkTT();
       
        if (tb == "")
        {
            user = new User();
            user.getInfo(txtTenDN.text);
            UnityEngine.SceneManagement.SceneManager.LoadScene("1_Menu");
        }
        else
        {
            txtNDLoi.text = tb;
            nameBT.text = "Quay lại";
            canvasTBLoi.SetActive(true);
        }
            
    }

    public void ShowDangKy()
    {
        canvasDangNhap.SetActive(false);
        canvasTBLoi.SetActive(false);
        cvDangKy.SetActive(true);
        canvasDangKyByIDFB.SetActive(false);
    }

    public void TroVeCVDangNhap()
    {
        canvasDangNhap.SetActive(true);
        canvasTBLoi.SetActive(false);
        cvDangKy.SetActive(false);
        canvasDangKyByIDFB.SetActive(false);
    }
}
