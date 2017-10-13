using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetProfile : MonoBehaviour {

    public Text txtName, txtScore, txtLevel;

    public AudioSource music;
    
    // Use this for initialization
    void Start () {
        txtLevel.text = Profile.user.level + "";
        txtName.text = Profile.user.name;
        txtScore.text = Profile.user.score + "";
        if(Profile.OnMusic)
        {
            music.mute = false;
        }
        else
        {
            music.mute = true;
        }
    }
	
    void Update()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("1_Menu");
                return;
            }
        }
    }

    
	
}
