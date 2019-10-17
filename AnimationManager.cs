using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
    private GameManager gManager;

    private Camera aCamera;
    private Animator animator;

    //[HideInInspector]
    public bool animacaoCompleta = false;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameManager") != null)
        {
            gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        aCamera = GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void ChamaAnimacao(string nome)
    {
        animator.SetTrigger(nome);        
    }

    public void FimAnimation()
    {
        animacaoCompleta = true;
    }
}
