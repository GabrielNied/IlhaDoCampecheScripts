using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologoManager2 : MonoBehaviour
{
    private TextManager tManager;
    private FadeManager fManager;

    public bool chamaHistoria = true;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("TextManager") != null)
        {
            tManager = GameObject.FindGameObjectWithTag("TextManager").GetComponent<TextManager>();
        }
        if (GameObject.FindGameObjectWithTag("FadeManager") != null)
        {
            fManager = GameObject.FindGameObjectWithTag("FadeManager").GetComponent<FadeManager>();
        }
        tManager.parteHistoria = 0;            
    }

    void Update()
    {       
        if (chamaHistoria)
        {
            if (!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            StartCoroutine(tManager.Type());
            chamaHistoria = false;
        }

        if (tManager.historiaCompleta)
        {
            fManager.ChamaCena(tManager.sceneToLoad);
        }
    }
}
