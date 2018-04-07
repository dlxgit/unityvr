using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPoint : MonoBehaviour {

    public static MainPoint instance;
    public GameObject canv;
    public GameObject textWin;
    public GameObject textLose;
    public GameObject panel;

    public AudioSource winSource;
    public AudioSource loseSource;

    int status = 0;
    bool lost = false;

	// Use this for initialization
	void Start () {
        //audio = GetComponents<AudioSource>();
        instance = this;
        
	}
	
	// Update is called once per frame
	void Update () {
		if (status == 1)
        {
            if (!loseSource.isPlaying)
            {
                Time.timeScale = 1;
                Application.LoadLevel(0);
                
            }
        }
        else if (status == 2)
        {

        }
	}

    public void lose()
    {
        if (!lost)
        {
            panel.active = true;
            textLose.active = true;
            loseSource.Play();
            status = 1;
            lost = true;
            Time.timeScale = 0;
        }
    }

    public void win()
    {
        panel.active = true;
        textWin.active = true;
        winSource.Play();
        status = 2;
        Time.timeScale = 0;
    }
}
