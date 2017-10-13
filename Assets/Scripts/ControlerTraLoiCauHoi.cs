using UnityEngine;
using System.Data;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class ControlerTraLoiCauHoi : MonoBehaviour
{

    public GameObject panelChucMung;
    public GameObject panelXacNhan;
    public GameObject panelTotal;
    public GameObject panelComplete;
    public Text TextNoidungXacNhan;
    public Text txtScoreDung;
    public Text txtScoreTotal;
    public Text txtScoreCurrent;
    public Text txtNumQues;
    public Text txtNameTopic;
    public Image imResult;
    public Image monkey;
    public Sprite imWrong;
    public Sprite imRight;


    int score = 0;

    public GameObject BonDapAn;
    public GameObject HaiDapAn;
    public GameObject TanThanh;
    public GameObject ChonHinh;
    //4 đáp án
    public Text txtNoiDung4DA;
    //Chon Hinh
    public Text NoiDungCauHoiTaChonHinh;
    public Button btA;
    public Button btB;
    //2 Dap án.
    public Text NoiDungCauHoiDungSai;
    public Text txtDapAn1;
    public Text txtDapAn2;

    ConnectDatabase cnn = new ConnectDatabase();
    //Tan Thanh
    public Text NoiDungCauHoiTanThanh;

    List<string> chude = new List<string>();


    //get question 
    string curTopic;
    public static List<CauHoi> allQues = new List<CauHoi>();
    ConnectDatabase cdb;
    int numQues;
    public void getquestions(string topic)
    {
        curTopic = topic;
        allQues = new List<CauHoi>();
        cdb = new ConnectDatabase();
        allQues = cdb.GetAllQuestionOfaTopic(topic);
        numQues = allQues.Count;
        txtNumQues.text = "Câu: " + 1 + "/" + numQues;
    }

    CauHoi curQues;

    string loaiCauHoi;
    int vt;
    Random rd = new Random();

    // Use this for initialization
    void Start()
    {
        getquestions(GetQuestions.topic);
        panelChucMung.SetActive(false);
        panelTotal.SetActive(false);
        //Set question
        vt = Random.Range(0, allQues.Count - 1);
        curQues = allQues[vt];
        loaiCauHoi = curQues.Loai;
        SetContent();
        chude = cnn.GetTopic(GetQuestions.lop);
        txtNameTopic.text = cnn.getNameTopic(GetQuestions.topic);
    }
    // Update is called once per frame

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                TroVeChonChuDe();
                return;
            }
        }
    }

    void chuyenCauHoi(string loaiCuaHoi)
    {
        if (loaiCauHoi == "TracNghiem4Cau")
            BonDapAn.SetActive(false);
        else
        if (loaiCauHoi == "DungSai")
            HaiDapAn.SetActive(false);
        else
        if (loaiCauHoi == "TanThanh")
            TanThanh.SetActive(false);
        else
        if (loaiCauHoi == "ChonHinh")
            ChonHinh.SetActive(false);
    }

    void SetContent()
    {
        if (curQues.Loai == "TracNghiem4Cau")
        {
            BonDapAn.SetActive(true);
            txtNoiDung4DA.text = curQues.Noidung+ "\n--------------------------------------------" 
                + "\nA. " + curQues.A + "\nB. " + curQues.B + "\nC. " + curQues.C + "\nD. " + curQues.D;
            txtNoiDung4DA.transform.Translate((float)-0.0005188, - 200, 0);
        }
        else
        if (curQues.Loai == "DungSai")
        {
            HaiDapAn.SetActive(true);
            NoiDungCauHoiDungSai.text = curQues.Noidung+ "\n----------------------------------------"
                + ":\nA. " + curQues.A + "\nB. " + curQues.B;
            NoiDungCauHoiDungSai.transform.Translate((float)-0.0005188, -200, 0);
        }
        else
        if (curQues.Loai == "TanThanh")
        {
            TanThanh.SetActive(true);
            NoiDungCauHoiTanThanh.text = curQues.Noidung + "\n-------------------------------------------"
                + "\nA. Tán thành.\nB. Phân vân.\nC. Không tán thành";
            NoiDungCauHoiTanThanh.transform.Translate((float)-0.0005188, -200, 0);
        }
        else
        if (curQues.Loai == "ChonHinh")
        {
            //setHinh
            btA.GetComponent<SpriteRenderer>().sprite = Sprite.Create(LoadPNG(curQues.A), new Rect(-182, -57, 330, 241), new Vector2(0.5f, 0.5f));
            btB.GetComponent<SpriteRenderer>().sprite = Sprite.Create(LoadPNG(curQues.B), new Rect(182, -57, 330, 241), new Vector2(0.5f, 0.5f));
            ChonHinh.SetActive(true);
        }
    }

    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    int soCauDung = 0;
    int soCauSai = 0;
    void Check(string answwer)
    {
        if (curQues.Dapan.ToLower() == answwer.ToLower())
        {
            score = score + allQues[vt].diem;
            txtScoreDung.text = "+ " + allQues[vt].diem;
            txtScoreCurrent.text = "Điểm: " + score;
            allQues.Remove(allQues[vt]);
            panelChucMung.SetActive(true);
            monkey.GetComponent<Animator>().SetBool("isCry",false);
            imResult.GetComponent<Image>().sprite = imRight;
            soCauDung++;
            txtNumQues.text = "Câu: " + (soCauDung + soCauSai + 1) + "/" + numQues;
        }
        else
        {
            panelChucMung.SetActive(true);
            monkey.GetComponent<Animator>().SetBool("isCry", true);
            imResult.GetComponent<Image>().sprite = imWrong;
            txtScoreDung.text = "+ 0";
            allQues.Remove(allQues[vt]);
            soCauSai++;
            txtNumQues.text = "Câu: " + (soCauDung + soCauSai + 1) + "/" + numQues;
        }
    }

    public void SelectedA()
    {
        Check("A");
    }

    public void SelectedB()
    {
        Check("B");
    }

    public void SelectedC()
    {
        Check("C");
    }

    public void SelectedD()
    {
        Check("D");
    }

    public void TiepTuc()
    {
        if (allQues.Count != 0)
        {
            vt = Random.Range(0, allQues.Count - 1);
            chuyenCauHoi(loaiCauHoi);
            curQues = allQues[vt];
            loaiCauHoi = curQues.Loai;
            SetContent();
            panelChucMung.SetActive(false);
        }
        else
        {
            panelChucMung.SetActive(false);
            txtScoreTotal.text = score + "";
            Profile.user.score = Profile.user.score + score;
            Profile.user.UpdateScoreAndLevelOfUser();
            string contentNK ="Bạn đã hoàn thành chủ đề " + cdb.getNameTopic(curTopic) + " với kết quả là: \n" + " - Sai: " + soCauSai + ".\n - Đúng: " + soCauDung + ".\n - Số điểm là: " + score + ".";
            if (!cdb.UpdateCommon("insert into NhatKy values ('" + System.DateTime.Now.ToString("dd/MM/yyyy") + "','" + contentNK + "')"))
                Debug.Log("insert into NhatKy values ('" + System.DateTime.Now.ToString("dd/MM/yyyy") + "','" + contentNK + "')");
            soCauSai = soCauDung = 0;
            panelTotal.SetActive(true);
        }
    }

    void ChuyenScene(string tenSceneToi)
    {
        panelChucMung.SetActive(false);
        panelXacNhan.SetActive(false);
        //        Scene s = EditorSceneManager.GetSceneByName(loaiCauHoi);
        //#pragma warning disable CS0618 // Type or member is obsolete
        //        SceneManager.UnloadScene(s);
        //#pragma warning restore CS0618 // Type or member is obsolete
        //        //EditorSceneManager.CloseScene(s, true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(tenSceneToi);
    }

    void XacNhan(string noidung)
    {
        TextNoidungXacNhan.text = noidung;
        panelXacNhan.SetActive(true);
    }

    public void DongYThoat()
    {
        panelXacNhan.SetActive(false);
        ChuyenScene("2_TheGioiThanTien");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("3_TraLoiCauhoi");
    }

    public void Huy()
    {
        
        panelXacNhan.SetActive(false);
        if(thoat == 0)
            panelChucMung.SetActive(true);
    }

    public void Thoat2()
    {
        //open scence select topic
        thoat = 1;
        XacNhan("Bạn có thật sự muốn thoát không?");
    }

    int thoat = 0;

    public void TroVeChonChuDe()
    {
        XacNhan("Bạn có thật sự muốn trở lại không?");
    }

    string nxtTopic = "";

    public void nextTopic()
    {
        bool complete = false;
        int numTopic = chude.Count;
        panelChucMung.SetActive(false);
        panelTotal.SetActive(false);
        for (int i = 0; i < numTopic; i++)
        {
            if(i == numTopic - 1)
            {
                complete = true;
            }
            if(chude[i] == GetQuestions.topic)
            {
                nxtTopic = chude[i + 1];
                break;
            }
        }
        if(complete == true)
        {
            panelComplete.SetActive(true);
            return;
        }
        getquestions(nxtTopic);
        curTopic = nxtTopic;
        //Set question
        vt = Random.Range(0, allQues.Count - 1);
        curQues = allQues[vt];
        loaiCauHoi = curQues.Loai;
        SetContent();
        txtNumQues.text = "Câu: " + 1 + "/" + numQues;
        txtNameTopic.text = cnn.getNameTopic(nxtTopic);
    }

    public void Thoat()
    {
        //open scence select topic
        thoat = 0;
        XacNhan("Bạn có thật sự muốn thoát không?");
    }
}
