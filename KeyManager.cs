using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    public int randomMinigame;

    public Image[] setasImage;

    public bool chamouMinigame = false, acertou = false, errou = false, minigameCompleto = false;

    public GameObject particula;

    void Start()
    {
        
    }


    void Update()
    {
        if (chamouMinigame && !acertou && !errou)
        {
            if (randomMinigame == 0)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        acertou = true;
                    }
                    else
                    {
                        errou = true;
                    }
                }
            }
            else if (randomMinigame == 1)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        acertou = true;
                    }
                    else
                    {
                        errou = true;
                    }
                }
            }
            else if (randomMinigame == 2)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        acertou = true;
                    }
                    else
                    {
                        errou = true;
                    }
                }
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        acertou = true;
                    }
                    else
                    {
                        errou = true;
                    }
                }
            }
        }
        else if (chamouMinigame && acertou && !errou)
        {
            foreach (Image seta in setasImage)
            {
                seta.gameObject.SetActive(false);
            }
            particula.SetActive(true);
            minigameCompleto = true;
            chamouMinigame = false;
        }
        else if (chamouMinigame && !acertou && errou)
        {
            foreach (Image seta in setasImage)
            {
                seta.gameObject.SetActive(false);
            }
            chamaMinigame();
            errou = false;
        }
    }

    public void chamaMinigame()
    {
        randomMinigame = Random.Range(0, 4);

        if(randomMinigame == 0)
        {
            setasImage[randomMinigame].gameObject.SetActive(true);
            chamouMinigame = true;
        }
        else if(randomMinigame == 1)
        {
            setasImage[randomMinigame].gameObject.SetActive(true);
            chamouMinigame = true;
        }
        else if(randomMinigame == 2)
        {
            setasImage[randomMinigame].gameObject.SetActive(true);
            chamouMinigame = true;
        }
        else
        {
            setasImage[randomMinigame].gameObject.SetActive(true);
            chamouMinigame = true;
        }

    }
}
