using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public bool hasSet = true;
    public AudioClip upclip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {   
        if (hasSet)
        {
        Debug.Log("Beep");
        transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y+180, transform.localEulerAngles.z);
        }
    }
}
