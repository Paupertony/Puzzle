using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public string[] msg_eng = {"hey!"};

    public string[] msg_ru = {"Эй!"};


    private int lang = 1; 

    private int k=0;

    private GameObject chat;


    // Start is called before the first frame update
    void Start()
    {
        Invoke ("StartLesson", 1f);
        chat = GameObject.Find("Chat");
        k=0;
    }


    private void SendMSG()
    {
        if (lang==1)
        {
            chat.GetComponent<Msg>().NewMSG("ROBOT: "+msg_eng[k]);
            chat.GetComponent<Msg>().NewMSG("ROBOT: press [Y]");
        }
        else
        {
            chat.GetComponent<Msg>().NewMSG("РОБОТ: "+msg_eng[k]);
            chat.GetComponent<Msg>().NewMSG("РОБОТ: нажми [Y]");
        }
    }

    private void StartLesson()
    {
        Time.timeScale = 0f;
        SendMSG();
    }

    private void StopLesson()
    {
        Time.timeScale = 1f;
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (k<msg_eng.Length-1){
                k++;
                SendMSG();
            }
            else{
                StopLesson();
            }
        }
    }
}
