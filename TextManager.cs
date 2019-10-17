using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    [Header("History Settings")]
    public TextMeshProUGUI textDisplay;
    public float typingSpeed, modifier;
    public Historia[] historia;

    [Header("Object References")]
    public GameObject textBox;
    public GameObject continueButton;
    public Animator animator;

    //[HideInInspector]
    public int parteHistoria = 0;
    public bool historiaCompleta = false, chamaAnim = false;
    public string numeroAnim, sceneToLoad;
    private bool typing = true;
    public int index;
    private float actualTypingSpeed;

    void Start()
    {
    
    }

    void Update()
    {
        if (!typing) continueButton.SetActive(true);
        if (!typing && Input.GetButtonDown("Interagir")) this.NextSentence();
    }

    public IEnumerator Type()
    {
        if (textBox != null)
        {
            textBox.SetActive(true);
        }
        if (historia[parteHistoria].special.Length > 0)
        {
            if (historia[parteHistoria].special[index])
            {
                actualTypingSpeed = typingSpeed * modifier;
                textDisplay.alignment = TMPro.TextAlignmentOptions.Center;
            }
            else
            {
                actualTypingSpeed = typingSpeed;
                textDisplay.alignment = TMPro.TextAlignmentOptions.Left;
            }
        }
        else
        {
            actualTypingSpeed = typingSpeed;
            textDisplay.alignment = TMPro.TextAlignmentOptions.Left;
        }

        textDisplay.text += "<#C3C3C3>";
        foreach (char letter in historia[parteHistoria].talker[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(actualTypingSpeed);
        }

        textDisplay.text += "</color><color=white>";
        foreach (char letter in historia[parteHistoria].sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(actualTypingSpeed);
        }

        textDisplay.text += "</color>";
        typing = false;
    }

    public void NextSentence()
    {
        typing = true;
        animator.SetTrigger("Change");
        continueButton.SetActive(false);

        if (index < historia[parteHistoria].sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            if (historia[parteHistoria].animation.Length > 0)
            {
                if (historia[parteHistoria].animation[index])
                {
                    chamaAnim = true;
                    numeroAnim = parteHistoria.ToString();
                    if (historia[parteHistoria].loadScene[index])
                    {
                        sceneToLoad = historia[parteHistoria].sceneName[index];
                    }
                }
            }
            index = 0;
            textDisplay.text = "";
            historiaCompleta = true;
            if (textBox != null)
            {
                textBox.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class Historia
    {
        public string[] talker;
        public string[] sentences;
        public bool[] special;
        public bool[] animation;
        public bool[] loadScene;
        public string[] sceneName;
    }

}
