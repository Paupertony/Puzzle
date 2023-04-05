using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Helper : MonoBehaviour
{   
    private GameObject hero;
    private NavMeshAgent myNavMeshAgent;
    private bool hasHero = false;
    public GameObject startTarget;
    public GameObject hand;
    public GameObject loader;
    public AudioClip run;
    private AudioSource audioS;
    private bool canPick = true;
    // Start is called before the first frame update
    void Start()
    {   
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        hero = GameObject.Find("Hero");
        audioS = GetComponent<AudioSource>();

        NewTarget();
    }

    private void CanAgain()
    {
        canPick = true;
    }

    private void Drop()
    {

        hero.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        hero.GetComponent<Rigidbody>().detectCollisions = true;
        hasHero = false;
        Invoke("NewTarget", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHero)
        {   
            hero.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            hero.transform.position = hand.transform.position;
            hero.transform.rotation = hand.transform.rotation;


            float dist = Vector3.Distance(startTarget.transform.position, transform.position);
            Debug.Log(dist);
            if (dist<0.5f)
            {   
                myNavMeshAgent.ResetPath();
                //canPick = false;
                Invoke("CanAgain", 2f);
                Vector3 relativePos = loader.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = rotation;
                Invoke("Drop", 0.3f);
                
            }
        }



    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player"){

            if (!hasHero && canPick)
            {
                hasHero = true;
                canPick =false;
                hero.GetComponent<Rigidbody>().detectCollisions = false;
            }
        }

    }


    private void NewTarget()
    {   
        if (!hasHero)
        {
            myNavMeshAgent.SetDestination(hero.transform.position);
            Invoke("NewTarget", 5f);
        }
        else
        {
            myNavMeshAgent.SetDestination(startTarget.transform.position);
        }

        audioS.PlayOneShot(run, 0.5f);
        
    }
}
