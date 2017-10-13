using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User  {

    public string idUser { get; set; }
    public string name{ get; set; }
    public int level { get; set; }
    public int score { get; set; }

    public User() {
        idUser = "";
        name = "";
        level = 1;
        score = 0;
    }

    public User(User user)
    {
        this.idUser = user.idUser;
        this.name = user.name;
        this.level = user.level;
        this.score = user.score;
    }

    public void UpdateScoreAndLevelOfUser()
    {
        ConnectDatabase cnn = new ConnectDatabase();
        cnn.InsertUpdateUser("update NguoiDung set Diem = " + score + ", CapBac = " + level + " where TenDangNhap = '" +idUser + "'");
    }

    public void getInfo(string id)
    {
        ConnectDatabase cnn = new ConnectDatabase();
        User n = cnn.GetInfoUser(id);
        level = n.level;
        name = n.name;
        score = n.score;
        idUser = n.idUser;
    }
}
