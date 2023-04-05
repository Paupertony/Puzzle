using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{ 
    public string nextLvl;
    // Start is called before the first frame update


    private void ToLvl()
    {
        SceneManager.LoadScene(nextLvl, LoadSceneMode.Single);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
        {
            Invoke("ToLvl", 0.1f);
        }
    }

}
