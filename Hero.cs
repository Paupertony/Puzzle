using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    private Vector3 moveDirection;
    private bool onConv = false;
    private AudioSource audioS;
    private Rigidbody body;
    public AudioClip hitClip;
    public AudioClip rubberClip;
    public AudioClip otherClip;
    private SphereCollider ball;
    private GameObject[] powerplus;
    public Material[] faces;
    public GameObject face;
    private bool power = false;
    public GameObject siren;
    public GameObject effect;
    public Material[] powersMaterial;
    private GameObject chat;

    private Quaternion face_target;

    private Vector2[] dotes = new Vector2[10];
    private GameObject[] lines = new GameObject[10];

    public int lang = 1; //1 eng 2 rus


    private string[] eng_hit =  {"Ouch!", "It hurts!", "Robots have feelings too", ":-(", "THANK YOU", "I'm not iron. Joke"};
    private string[] ru_hit =  {"Ауч!", "Больно же!", "У роботов тоже есть чувства", ":-(", "СПАСИБО", "Я же не железный. Шутка"};

        // Start is called before the first frame update
    void Start()
    {

    transform.localEulerAngles = new Vector3(- 187.69f,   -48.12799f,    29.259f);    
    face_target = face.transform.rotation; // - 187.69   -48.12799    29.259

    chat = GameObject.Find("Chat");
    body = GetComponent<Rigidbody>();
    audioS = GetComponent<AudioSource>();
    ball = GetComponent<SphereCollider>();
    siren = GameObject.Find("Siren");
    //face_target = GameObject.Find("face_target");
    effect = GameObject.Find("ElectricalSparksEffect");
    siren.SetActive(false);
    }


    private void playHit(int type, float pitch, float vol)
    {   
       switch (type)
        {
            case 1:
            audioS.pitch = pitch;
            audioS.PlayOneShot(hitClip, vol);
            break;
            
            case 2:
            audioS.pitch = pitch;
            audioS.PlayOneShot(rubberClip, vol);
            break;

            case 3:
            audioS.pitch = pitch;
            audioS.PlayOneShot(otherClip, vol);
            break;


        }
    }

    private void SizeSet(float size)
    {
        ball.radius = size;
    }


    private void ChageMaterial(GameObject[] list, int num)
    {
        for (int i=0; i<list.Length; i++)
        {
            list[i].GetComponent<Renderer>().material = powersMaterial[num];

            switch(num)
            {
                case 1:
                list[i].GetComponent<AudioSource>().Play();
                break;

                case 0:
                list[i].GetComponent<AudioSource>().Stop();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // SizeSet(1.2f);
            power = true;

            GameObject[] pw = GameObject.FindGameObjectsWithTag("Power");
            ChageMaterial(pw,1);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
           // SizeSet(0.5f);
            power = false;
            GameObject[] pw = GameObject.FindGameObjectsWithTag("Power");
            ChageMaterial(pw,0);
             for (int i=0; i<lines.Length; i++)
                {
                    if (lines[i])
                    {
                        Destroy(lines[i]);
                    }
                }
        }


        //Vector3 relativePos =  face_target.transform.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        face.transform.rotation = face_target;
         //face.transform.localEulerAngles = new (0,0,0);
        
    }


    private void FixedUpdate()
    {
        if (onConv)
        {   
            float mx = Mathf.Cos(moveDirection.y/Mathf.Rad2Deg);
            float my = Mathf.Sin(moveDirection.y/Mathf.Rad2Deg);
            body.AddForce(new Vector3(my,0,mx));
        }

        //power section

        if (power)
        {
            float mindist =99999f;
            int num = 0;
            powerplus = GameObject.FindGameObjectsWithTag("Power");
            for (int i=0; i<powerplus.Length; i++)
            {
                float dist = Vector3.Distance(transform.position, powerplus[i].transform.position);
                if (dist<=mindist)
                {
                    mindist = dist;
                    num = i;
                }
            }
            Curves(powerplus[num]);
            Vector3 force = powerplus[num].transform.position - transform.position;
            body.AddForce(force/mindist*30);

        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, int k, float duration = 0.01f)
         {
             lines[k] = new GameObject();
             lines[k].transform.position = start;
             lines[k].AddComponent<LineRenderer>();
             LineRenderer lr = lines[k].GetComponent<LineRenderer>();
             lr.material = powersMaterial[1];
             lr.SetColors(color, color);
             lr.SetWidth(0.1f, 0.1f);
             lr.SetPosition(0, start);
             lr.SetPosition(1, end);
             //GameObject.Destroy(myLine, duration);
         }





    private void Curves(GameObject obj)
    {
        Vector2 dot1 = new Vector2(transform.position.x, transform.position.y);
        Vector2 dot3 = new Vector2(obj.transform.position.x, obj.transform.position.y);
        Vector2 dot2 = new Vector2((dot1.x+dot3.x)/2, dot3.y+2f); 
        if (dot1.y>dot3.y)
        {
                dot2 = new Vector2((dot1.x+dot3.x)/2, dot1.y+2f);
        }

           
        
        int kD= 0;

        for (int i=0; i<lines.Length; i++)
        {
            if (lines[i])
            {
                Destroy(lines[i]);
            }
        }
        float deltaZ = (obj.transform.position.z - transform.position.z)/10;



        for (float i=0; i<1; i+=0.1f)
        {   
            float tempX = (1-i)*(1-i)*dot1.x+2*(1-i)*i*dot2.x+i*i*dot3.x;
            float tempY = (1-i)*(1-i)*dot1.y+2*(1-i)*i*dot2.y+i*i*dot3.y;
            dotes[kD] = new Vector2(tempX, tempY);
            kD++;

        }

        for (int i=0; i<9; i++ )
        {   
            Vector3 startD = new Vector3(dotes[i].x, dotes[i].y, transform.position.z+deltaZ*i);
            Vector3 endD = new Vector3(dotes[i+1].x, dotes[i+1].y, transform.position.z+deltaZ*(i+1));

            DrawLine(startD, endD, new Vector4(0,0,1,1), i);
        }



    }


    //Нахождение на конвейере


    private void EffectStop()
    {
        effect.GetComponent<ParticleSystem>().Stop();
    }

    private void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.tag =="Move")
        {
            playHit(2, 1f,col.impulse.magnitude/25f);
        }
        else if (col.gameObject.tag =="Metal" || col.gameObject.tag =="Power")
        {
            playHit(1, Random.Range(0.9f, 1.1f),col.impulse.magnitude/20f);
            ContactPoint contact = col.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            effect.transform.position = pos;
            effect.transform.rotation = rot;

       




            effect.GetComponent<ParticleSystem>().Play();
            Invoke("EffectStop", 0.2f);
        }

        else if (col.gameObject.tag =="Untagged" || col.gameObject.tag =="Respawn")
        {
            playHit(3, Random.Range(0.7f, 1.1f),col.impulse.magnitude/20f);
        }
        Debug.Log("Collision " + col.impulse.magnitude);

        if (col.impulse.magnitude>10){
            face.GetComponent<Renderer>().material = faces[2];
            Invoke("NormalFace", 3f);
        }


        if (col.impulse.magnitude>12){
                switch (lang)
                {
                    case 1:
                
                    chat.GetComponent<Msg>().NewMSG("ROBOT: " + eng_hit[Random.Range(0,eng_hit.Length)]);
                    break;

                    case 2:
                    chat.GetComponent<Msg>().NewMSG("РОБОТ: " + ru_hit[Random.Range(0,ru_hit.Length)]);
                    break;
                }
        } 
    }


    private void NormalFace()
    {
        face.GetComponent<Renderer>().material = faces[0];
    }

    private void OnTriggerStay(Collider col)
    {
        
        if (col.gameObject.tag == "Move")
        {
            onConv = true;
            moveDirection = col.gameObject.transform.localEulerAngles;
        }

        if (col.gameObject.tag == "Up")
        {
            body.velocity = new Vector3(0,10,0);
            //body.AddForce(new Vector3(0,20,0));

        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Move")
        {
            moveDirection = col.gameObject.transform.localEulerAngles;
            face.GetComponent<Renderer>().material = faces[0];
        }

        if (col.gameObject.tag == "Up")
        {   
            body.velocity = new Vector3(0,0,0);
            transform.position = new Vector3(col.gameObject.transform.position.x, transform.position.y, col.gameObject.transform.position.z);
            //body.AddForce(new Vector3(0,400,0));
            face.GetComponent<Renderer>().material = faces[1];

        }

        if (col.gameObject.tag == "Respawn")
        {
            siren.SetActive(true);
            siren.GetComponent<AudioSource>().Play();
            face.GetComponent<Renderer>().material = faces[3];
        }

    }

    private void OnTriggerExit(Collider col)
    {
        onConv = false;

        if (col.gameObject.tag == "Respawn")
        {
            siren.SetActive(false);
        }
    }
}
