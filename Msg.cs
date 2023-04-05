using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Msg : MonoBehaviour
{   
    public TMP_Text[] txt;
    public string welcome_text_eng;
    public string welcome_text_ru;
    public int lang = 1; //1 eng 2 rus

    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {   
        audioS = GetComponent<AudioSource>();
        for (int i=0; i < txt.Length; i++)
        {
            txt[i].text = " ";
        }
        if (lang==1)
        {
            NewMSG("SYSTEM: "+welcome_text_eng);
        }
        else
        {
            NewMSG("СИСТЕМА: "+welcome_text_ru);
        }
    }


    public void NewMSG(string text)
    {
        txt[2].text = txt[1].text;
        txt[1].text = txt[0].text;
        txt[0].text = text;
        audioS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
