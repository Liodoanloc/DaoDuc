using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using System;
using System.IO;

public class ConnectDatabase {

    string strCon;
    

    string p = "App_DaoDuc.sqlite";

    string convertStrCon()
    {
#if UNITY_EDITOR
        strCon = Application.dataPath + "/StreamingAssets/" + p;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        strCon = Application.persistentDataPath + "/" + p;

        if (!File.Exists(strCon))

        {

            // if it doesn't ->

            // open StreamingAssets directory and load the db ->

            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);  // this is the path to your StreamingAssets in android

            while (!loadDB.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check

            // then save to Application.persistentDataPath
            File.WriteAllBytes(strCon, loadDB.bytes);
        }
#endif
        return strCon;
    }

    public List<CauHoi> GetAllQuestionOfaTopic(string maChude)
    {
        IDbConnection con = new SqliteConnection("URI=file:" + convertStrCon());
        con.Open();
        string query = "select MaCauHoi, MaLoai, NoiDung, A, B, C, D, DapAn, Diem from CauHoi where MaChuDe = '" + maChude+"'";        
        try {
            using (con)
            {
                List<CauHoi> lstMa = new List<CauHoi>();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    IDataReader datas = cmd.ExecuteReader();
                    while (datas.Read())
                    {
                        CauHoi cauhoi = new CauHoi();
                        cauhoi.Ma = datas[0].ToString();
                        cauhoi.Loai = datas[1].ToString();
                        cauhoi.Noidung = datas[2].ToString();
                        cauhoi.A = datas[3].ToString();
                        cauhoi.B = datas[4].ToString();
                        cauhoi.C = datas[5].ToString();
                        cauhoi.D = datas[6].ToString();
                        cauhoi.Dapan = datas[7].ToString();
                        cauhoi.diem = int.Parse(datas[8].ToString());
                        lstMa.Add(cauhoi);
                    }
                    con.Close();
                    return lstMa;
                }
            }
        }
        catch
        {
            return new List<CauHoi>();
        }
        
    }

    public string getNameTopic(string maChude)
    {
        IDbConnection con = new SqliteConnection("URI=file:" + convertStrCon());
        con.Open();
        string query = "select TenChuDe from ChuDe where MaChuDe = '" + maChude + "'";
        try
        {
            using (con)
            {
                using (IDbCommand cmd = con.CreateCommand())
                {
                    string name = "";
                    cmd.CommandText = query;
                    IDataReader datas = cmd.ExecuteReader();
                    while (datas.Read())
                    {
                        name =  datas[0].ToString();
                    }
                    return name;
                }
            }
        }
        catch
        {
            return "";
        }

    }

    public List<string> GetTopic(string maLop)
    {
        IDbConnection con = new SqliteConnection("URI=file:" + convertStrCon());
        con.Open();
        string query = "select MaChuDe from ChuDe where MaLop = '" + maLop + "' and (select count(*) from CauHoi where MaChuDe = MaChuDe) > 0" ;
        try
        {
            using (con)
            {
                List<string> lstMa = new List<string>();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    IDataReader datas = cmd.ExecuteReader();
                    while (datas.Read())
                    {
                        string ma = datas.ToString();
                        lstMa.Add(ma);
                    }
                    con.Close();
                    return lstMa;
                }
            }
        }
        catch
        {
            return new List<string>();
        }

    }
   

    public List<LoiHayYDep> GetAllLoiHayYDep()
    {
        IDbConnection con = new SqliteConnection("URI=file:" + convertStrCon());
        con.Open();
        string query = "select ma, noidung from LoiHayYDep";
        try
        {
            using (con)
            {
                List<LoiHayYDep> lstMa = new List<LoiHayYDep>();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    IDataReader datas = cmd.ExecuteReader();
                    while (datas.Read())
                    {
                        Debug.Log("loihay1");
                        LoiHayYDep item = new LoiHayYDep();
                        item.Ma = datas[0].ToString();
                        item.NoiDung = datas[1].ToString();
                        lstMa.Add(item);
                        Debug.Log("loihay");
                    }
                    con.Close();
                    return lstMa;
                }
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.ToString());
            return new List<LoiHayYDep>();
        }

    }

    public List<NhatKy> GetAllNhatKy()
    {
        IDbConnection con = new SqliteConnection("URI=file:" + convertStrCon());
        con.Open();
        string query = "select * from NhatKy";
        try
        {
            using (con)
            {
                List<NhatKy> lstMa = new List<NhatKy>();
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    IDataReader datas = cmd.ExecuteReader();
                    while (datas.Read())
                    {
                        NhatKy item = new NhatKy();
                        
                        item.Ngay = datas[0].ToString();
                        Debug.Log(item.Ngay);
                        item.NoiDung = datas[1].ToString();                        
                        lstMa.Add(item);
                        Debug.Log(lstMa.Count + " nhatky ");
                    }
                    con.Close();
                    return lstMa;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return new List<NhatKy>();
        }

    }

    public bool UpdateCommon(string sql)
    {
        try
        {
            IDbConnection con = new SqliteConnection("URI=file:" + convertStrCon());
            con.Open();
            using (con)
            {
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    
                    con.Close();
                    return true;
                }
            }
        }catch(Exception ex)
        {
            Debug.Log(ex.ToString());
            return false;
        }
    }

    //cap nhạp điểm 

    public int CheckExist(string sql)
    {
        try
        {
            IDbConnection con = new SqliteConnection("URI=file:" + convertStrCon());
            con.Open();
            using (con)
            {
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    string var = cmd.ExecuteScalar().ToString();                    
                    con.Close();
                    if (var != "0")
                        return 1;
                    else return 0;
                }
            }
        }
        catch
        {
            return -1;
        }
    }

    public bool InsertUpdateUser(string sql)
    {
        try
        {
            IDbConnection con = new SqliteConnection("URI=file:" + convertStrCon());
            con.Open();
            using (con)
            {
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    Debug.Log(sql);
                    int a = cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.ToString());
            return false;
        }
    }

    public User GetInfoUser(String idUser)
    {
        User user = new User();
        string sql = "select * from NguoiDung where TenDangNhap = '" + idUser +"'";
        try
        {
            IDbConnection con = new SqliteConnection("URI=file:" + convertStrCon());
            con.Open();
            using (con)
            {
                using (IDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    IDataReader data = cmd.ExecuteReader();
                    user.idUser = idUser;
                    user.name = data[1].ToString();
                    user.level = int.Parse(data[3].ToString());
                    user.score = int.Parse(data[4].ToString());
                    Debug.Log(data[1].ToString());
                    Debug.Log(data[3].ToString());
                    Debug.Log(data[4].ToString());
                    Debug.Log("ádasdasd");

                    //while (data.Read())
                    //{

                    //}
                    con.Close();
                    return user;
                }
            }
        }
        catch
        {
            return null;
        }
    }

}
