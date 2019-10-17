using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager UIGM;

    public Image imgRuna;
    public Sprite[] runas = new Sprite[5];

    public GameObject[] armIndio = new GameObject[5];
    public GameObject arm;

    public bool RuneInRock = false, usouPedraCerta = false, runaMao = false;

    private void Awake()
    {
        UIGM = this;
        
    }

    public void ChangeRunesImage(string runaName)
    {
        //imgRuna.transform.gameObject.SetActive(true);

        if (runaName == "Runa Azul")
        {
            runaMao = true;
            imgRuna.sprite = runas[0];
            if (RuneInRock)
            {
                for (int i = 0; i < armIndio.Length; i++)
                {
                    if (i == 0)
                    {
                        arm.SetActive(true);
                        armIndio[i].SetActive(true);

                        GameManager.gManager.runaErrada = true;
                    }
                    else
                    {

                        armIndio[i].SetActive(false);
                    }
                }
            }
        }
        else if (runaName == "Runa verde")
        {
            runaMao = true;
            imgRuna.sprite = runas[1];
            if (RuneInRock)
            {
                for (int i = 0; i < armIndio.Length; i++)
                {
                    if (i == 1)
                    {
                        arm.SetActive(true);
                        armIndio[i].SetActive(true);

                        GameManager.gManager.runaErrada = true;
                    }
                    else
                    {
                        armIndio[i].SetActive(false);
                    }
                }
            }
        }
        else if (runaName == "Runa Amarela")
        {
            runaMao = true;
            imgRuna.sprite = runas[2];
            if (RuneInRock)
            {
                for (int i = 0; i < armIndio.Length; i++)
                {
                    if (i == 2)
                    {
                        arm.SetActive(true);
                        armIndio[i].SetActive(true);

                        GameManager.gManager.currentState = GameManager.GameState.Minigame;
                        imgRuna.transform.gameObject.SetActive(false);
                    }
                    else
                    {
                        arm.SetActive(true);
                        armIndio[i].SetActive(false);
                    }
                }
            }
        }
        else if (runaName == "Runa Rosa")
        {
            runaMao = true;
            imgRuna.sprite = runas[3];
            if (RuneInRock)
            {
                for (int i = 0; i < armIndio.Length; i++)
                {
                    if (i == 3)
                    {
                        arm.SetActive(true);
                        armIndio[i].SetActive(true);

                        GameManager.gManager.runaErrada = true;
                    }
                    else
                    {
                        armIndio[i].SetActive(false);
                    }
                }
            }
        }
        else if (runaName == "Runa Roxa")
        {
            runaMao = true;
            imgRuna.sprite = runas[4];
            if (RuneInRock)
            {
                for (int i = 0; i < armIndio.Length; i++)
                {
                    if (i == 4)
                    {
                        arm.SetActive(true);
                        armIndio[i].SetActive(true);

                        GameManager.gManager.runaErrada = true;
                    }
                    else
                    {
                        armIndio[i].SetActive(false);
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (!RuneInRock)
        {
            if (arm != null)
            {
                arm.SetActive(false);
            }
        }
    }

    public void TerminouAnimacaoDoIndio()
    {
        RuneInRock = false;
    }
}
