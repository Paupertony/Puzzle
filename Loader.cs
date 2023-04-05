using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public AudioClip upclip;
    private AudioSource audioS;
    public float pitch = 1;
    private bool canPlay = true;
    // Start is called before the first frame update
    void Start()
    {

    audioS = GetComponent<AudioSource>();
    audioS.pitch = pitch;
    }

    private void CanAgain()
    {
        canPlay = true;
    }


    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == "Player" && canPlay){
            audioS.PlayOneShot(upclip, 0.7F);
            canPlay = false;
            Invoke("CanAgain", 1f);
        }
    }


}
