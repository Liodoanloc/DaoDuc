using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlerGame : MonoBehaviour {

    // Use this for initialization
    int[,] matran;
    string[,] content;
    public Button       bt00, bt01, bt02, bt03, bt04, bt05, bt06,
                        bt10, bt11, bt12, bt13, bt14, bt15, bt16,
                        bt20, bt21, bt22, bt23, bt24, bt25, bt26,
                        bt30, bt31, bt32, bt33, bt34, bt35, bt36;
    public Button[] bt;
    List<string> contentGame;
    void Start()
    {
        matran = new int[4, 7] {{1,4,3,9,11,9,14},{10,7,2,12,7,12,3},{13,1,5,8,10,11,6}, {8,6,14,2,4,5,13}};
        //bt00.tag = "0:0";
        //bt01.tag = "0:1";
        //bt02.tag = "0:2";
        //bt03.tag = "0:3";
        //bt04.tag = "0:4";
        //bt05.tag = "0:5";
        //bt06.tag = "0:6";
        //bt10.tag = "1:0";
        //bt11.tag = "1:1";
        //bt12.tag = "1:2";
        //bt13.tag = "1:3";
        //bt14.tag = "1:4";
        //bt15.tag = "1:5";
        //bt16.tag = "1:6";
        //bt20.tag = "2:0";
        //bt21.tag = "2:1";
        //bt22.tag = "2:2";
        //bt23.tag = "2:3";
        //bt24.tag = "2:4";
        //bt25.tag = "2:5";
        //bt26.tag = "2:6";
        //bt30.tag = "3:0";
        //bt31.tag = "3:1";
        //bt32.tag = "3:2";
        //bt33.tag = "3:3";
        //bt34.tag = "3:4";
        //bt35.tag = "3:5";
        //bt36.tag = "3:6";
    }

    public void createContent()
    {
        contentGame = new List<string>();
        contentGame.Add(" Xa mặt : cách Lòng ");
        contentGame.Add(" Yêu cho roi cho vọt : ghét cho ngọt cho bùi ");
        contentGame.Add(" Ép dầu ép mỡ : ai nỡ ép duyên ");
        contentGame.Add(" Có tật : giật mình ");
        contentGame.Add(" Gậy ông : đập lưng ông ");
        contentGame.Add(" Phéo vua : thua lệ làng ");
        contentGame.Add(" Qua cầu : rút ván ");
        contentGame.Add(" Khẩu phật : tâm xà ");
        contentGame.Add(" Trông mặt : mà bắt hình dong ");
        contentGame.Add(" Ăn quả : nhớ kẻ trồng cây ");
        contentGame.Add(" Cái nết : đánh chết cái đẹp ");
        contentGame.Add(" Chín người : mười ý ");
        contentGame.Add(" Đi hỏi già : về nhà hỏi trẻ ");
    }

    int press = 0;
    int[] lcTem = new int[2];

    List<GameObject> lsbt = new List<GameObject>();
    List<GameObject> lsbtHide = new List<GameObject>();
    public void click(GameObject bt)
    {
        string[] location = bt.GetComponent<Tag>().tag.Split(':');
        int i = int.Parse(location[0]);
        int j = int.Parse(location[1]);
        Debug.Log(i +":"+ j);
        if (matran[i,j] != 0)
        {
            lsbt.Add(bt);
            //bt.GetComponent<Renderer>().material.color = new Color(190, 241, 249, 255);
            if (press < 1)
            {
                lcTem[0] = i;
                lcTem[1] = j;
            }
            press++;
            if(press == 2)
            {
                press = 0;
                if(matran[i, j] == matran[lcTem[0], lcTem[1]])
                {
                    matran[i, j] = 0;
                    matran[lcTem[0], lcTem[1]] = 0;
                    lsbt[0].SetActive(false);
                    lsbt[1].SetActive(false);
                    lsbtHide.Add(lsbt[0]);
                    lsbtHide.Add(lsbt[1]);
                    ktThang();
                    lsbt.Clear();
                }
                else
                {
                    lsbt.Clear();
                    lsbt.Add(bt);
                    press = 1;
                    lcTem[0] = i;
                    lcTem[1] = j;
                    //lsbt[0].GetComponent<Renderer>().material.color = new Color(249, 179, 209, 255);
                    //lsbt[2].GetComponent<Renderer>().material.color = new Color(249, 179, 209, 255);
                }
                
            }
        }
    }

    public bool ktThang()
    {
        bool kt = true;
        for( int i = 0; i < 4; i ++)
        {
            for( int j = 0; j < 7; j++)
            {
                if (matran[i, j] != 0)
                    return false;
            }
        }
        return kt;
    }

    public void Refresh()
    {
        foreach (GameObject go in lsbtHide)
            go.SetActive(true);
        Start();
        lsbtHide.Clear();
    }

    public void TroVe()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("XemVideo");
    }

    
}
