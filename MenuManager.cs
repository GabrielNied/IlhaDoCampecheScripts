using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> botoes;
    private GameObject criado, icone, canvas;
    private Transform botao;
    private FadeManager fManager;

    [Header("Qual botão vai começar selecionado")]
    public int posicao;

    private int opcoes;
    private bool mouseOver = false, clicou = false;

    [Header("Coloca os botões de modo Manual")]
    public bool manual = false;

    public AudioClip trilha, select, troca;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("FadeManager") != null)
        {
            fManager = GameObject.FindGameObjectWithTag("FadeManager").GetComponent<FadeManager>();
        }
        /*
        if (!manual)
        {
            canvas = GameObject.Find("Canvas");
            botao = canvas.transform.Find("Buttons");

            for (int i = 0; i < botao.childCount; i++)
            {
                GameObject filhoBotao = botao.transform.GetChild(i).gameObject;
                botoes.Add(filhoBotao);
            }
        }
        opcoes = botoes.Count;
        */

        if (!SoundManager.instance.musicSource.isPlaying)
        {
            SoundManager.instance.musicSource.clip = trilha;
            SoundManager.instance.musicSource.Play();
            SoundManager.instance.FadeIn(SoundManager.instance.musicSource, 0.9f, 0.25f);
        }
    }

    void Update()
    {
        //PosicoesMenu();
    }


    void PosicoesMenu()
    {
        /*
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (posicao < opcoes - 1)
            {
                posicao++;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (posicao > 0)
            {
                posicao--;
            }
        }
        

        if (Input.GetButtonDown("Interagir"))
        {
            Debug.Log("chamou");
            for (int i = 0; i < posicao; i++)
            {
                botao.transform.GetChild(posicao).GetComponent<Button>().onClick.Invoke();
            }
        }
        
        if (!mouseOver)
        {
            botoes[posicao].GetComponent<Button>().Select();
        }
        else
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
        */
    }

    public void MouseOver(int posicaoBotao)
    {
        mouseOver = true;
        //posicao = posicaoBotao;
        if (!clicou)
        {
            SoundManager.instance.RandomizeSfx(troca);
        }
    }

    public void MouseExit()
    {
        mouseOver = false;
    }

    public void NewGameBtn(string NewGameLevel)
    {
        if (!clicou)
        {
            SoundManager.instance.RandomizeSfx(select);
            fManager.ChamaCena(NewGameLevel);
            clicou = true;
        }
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }
}