using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChamaHistoria : MonoBehaviour
{
    private TextManager tManager;
    [Header("Número da história no TextManager")]
    public int numeroHistoria;
    [Header("Opções")]
    public bool repetivel = false;
    public bool interagir = false;

    //[HideInInspector]
    public bool espera = false, tocou = false;

    public Transform posicaoObjeto;

    private bool colidiu = false;
    private bool mouseOver = false;
    // mudar vel pra cada texto
    // chamar só se tiver olhando pro objeto

    void Start()
    {

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Ilha")
        {
            if (colidiu)
            {
                if (!interagir)
                {
                    if (!tocou && !espera)
                    {
                        GameManager.gManager.Historia(numeroHistoria, posicaoObjeto.position);
                        espera = true;

                        if (!repetivel)
                        {
                            tocou = true;
                        }
                        Destroy(this.gameObject);
                    }
                }                
            }
        }
        else
        {
            if (colidiu && mouseOver)
            {
                if (!interagir)
                {
                    if (!tocou && !espera)
                    {
                        Debug.Log("chamou");
                        GameManager.gManager.Historia(numeroHistoria, posicaoObjeto.position);
                        espera = true;

                        if (!repetivel)
                        {
                            tocou = true;
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Interagir"))
                    {
                        if (!tocou && !espera)
                        {
                            Debug.Log("chamou1");
                            GameManager.gManager.Historia(numeroHistoria, posicaoObjeto.position);
                            espera = true;

                            if (!repetivel)
                            {
                                tocou = true;
                            }
                        }
                    }
                }
            }
        }
        
    }

    public void OnMouseEnter()
    {
        mouseOver = true;
    }
    public void OnMouseOver()
    {
        mouseOver = true;
    }
    public void OnMouseExit()
    {
        mouseOver = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            colidiu = true;                  
        }
    }    

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            colidiu = false;
            espera = false;
        }
    }
}
