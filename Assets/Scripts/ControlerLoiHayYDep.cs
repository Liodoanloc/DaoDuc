using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlerLoiHayYDep : MonoBehaviour
{

    List<LoiHayYDep> listLoiHayYDep;
    int numContent;
    int curContent = 0;
    public Text contentLHYD;
    public Toggle autoNext;
    public Text txtToggleNext;
    int timeAgo;

    public void getNoiDung()
    {
        listLoiHayYDep = new List<LoiHayYDep>();
        ConnectDatabase cdb = new ConnectDatabase();
        listLoiHayYDep = cdb.GetAllLoiHayYDep();
        numContent = listLoiHayYDep.Count;
        Debug.Log(numContent + "");
        if (numContent != 0)
        {
            timeAgo = System.DateTime.Now.Second;
            if (timeAgo >= 0 && timeAgo < 30)
                timeAgo = 0;
            else
                timeAgo = 30;
        }
    }

    // Use this for initialization
    void Start()
    {
        getNoiDung();
        setContent();
        clickToggleNext();
    }

    // Update is called once per frame
    void Update()
    {
        if(numContent != 0)
            if (autoNext.isOn)
            {
                if (System.DateTime.Now.Second == 30 && timeAgo == 0) 
                {
                    timeAgo = 30;
                    nextContent();
                }
                else if (System.DateTime.Now.Second == 0 && timeAgo == 30)
                {
                    timeAgo = 0;
                    nextContent();
                }
            }
    }

    public void nextContent()
    {
        if (numContent != 0)
        {
            curContent++;
            if (curContent == numContent)
                curContent = 0;
            setContent();
        }
    }

    public void preContent()
    {
        if (numContent != 0)
        {
            curContent--;
            if (curContent < 0)
                curContent = numContent - 1;
            setContent();
        }
    }

    void setContent()
    {
        if (numContent != 0)
        {
            contentLHYD.text = listLoiHayYDep[curContent].NoiDung;
        }
    }

    public void clickToggleNext()
    {
        if(autoNext.isOn)
        {
            txtToggleNext.text = "Tự động chuyển";
        }
        else
        {
            txtToggleNext.text = "Không chuyển";
        }
    }
}
