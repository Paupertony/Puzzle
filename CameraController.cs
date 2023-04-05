using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject hero;
    public bool followHero = false;
    private int type = 1;
    private float speed = 0.5f;
    private Quaternion StartRot;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        followHero = false;
        transform.position = new Vector3(-30, 50, -30);
    }


    void FixedUpdate()
    {
        if (!followHero){
            transform.position = new Vector3(-30, transform.position.y-speed, -30);
            if (speed>0.15f)
            {
                speed-=0.05f;
            }
            if (transform.position.y<25){
                followHero=true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.C)||Input.GetKeyDown(KeyCode.V))&&followHero)
        {
            type++;
            if (type>3)
            {
                type = 1;
            }

            switch (type)
            {
                case 1:
                transform.localEulerAngles = new Vector3(45, 45, 0);
                break;

                case 2:
                transform.localEulerAngles = new Vector3(90, 0, 0);
                break;

                case 3:
                transform.localEulerAngles = new Vector3(90, 0, 0);
                break;

            }
        }

        if (followHero){

            switch (type)
            {
                case 1:
                transform.position = new Vector3 (hero.transform.position.x-10f, 19f, hero.transform.position.z-10f);
                break;

                case 2:
                transform.position = new Vector3 (hero.transform.position.x, 10f, hero.transform.position.z);
                break;

                case 3:
                transform.position = new Vector3 (hero.transform.position.x, 30f, hero.transform.position.z);
                break;



            }
        }
    }
}
