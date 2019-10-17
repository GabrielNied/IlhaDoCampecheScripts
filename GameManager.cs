using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gManager = null;

    [HideInInspector]
    public enum GameState { Animacoes, Gameplay, Minigame, Historia, Transicao }
    public GameState currentState = GameState.Gameplay;

    public TextManager tManager;
    private AnimationManager aManager;
    private FirstPersonController pController;
    private KeyManager kManager;
    private FadeManager fManager;
    private TrocaCor tCor;

    public bool chamaHistoria = false;
    public bool chamaAnimacao = false;
    public bool chamaMinigame = false;
    public bool podeFalar = true;
    public bool furacao = false, podeOlharTempo = false, podeComecar = false, runaErrada = false;

    private GameObject aCamera, mCamera;
    public GameObject boitataEixo, pedra1, pedra2;

    public Vector3 posicaoObjeto;
    public Transform posicaoFuracao, posicaoPedra, posicaoBoitata;
    public AudioClip boitata, musica, mar, musicaMuseu, furacaoSom;
    public AudioClip[] indio;

    public int parteHistoriaIlha = 0, parteFala = 0, parte = 0;

    private float volumeMusica;
    public float tempoOlhando = 3;

    public GameObject inscricaoLigada, inscricaoDesligada, luzInscricao, mouse;

    private void Awake()
    {
        if (gManager == null) gManager = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        /*
        if (SceneManager.GetActiveScene().name == "Museu")
        {
            SoundManager.instance.efxSource.clip = porta;
            SoundManager.instance.efxSource.Play();
        }
        */
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Creditos")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Destroy(this.gameObject);
        }
        else if (SceneManager.GetActiveScene().name == "MuseuFinal")
        {
            currentState = GameState.Animacoes;
            chamaHistoria = false;

            SoundManager.instance.musicSource.clip = musicaMuseu;
            SoundManager.instance.musicSource.Play();
            SoundManager.instance.FadeIn(SoundManager.instance.musicSource, volumeMusica, 0.1f);
        }
        else if (SceneManager.GetActiveScene().name == "Ilha")
        {
            mouse = GameObject.FindGameObjectWithTag("Mouse");
            inscricaoLigada = GameObject.FindGameObjectWithTag("PedraIlhaLigada");
            inscricaoDesligada = GameObject.FindGameObjectWithTag("PedraIlha");
            inscricaoLigada.SetActive(false);
            luzInscricao = GameObject.FindGameObjectWithTag("LuzInscricao");
            luzInscricao.SetActive(false);

            boitataEixo = GameObject.FindGameObjectWithTag("BoitataEixo");


            pedra1 = GameObject.FindGameObjectWithTag("MagicRockFake");
            pedra2 = GameObject.FindGameObjectWithTag("MagicRock");
            pedra2.SetActive(false);
            currentState = GameState.Historia;
            StartCoroutine(Espera());
            posicaoFuracao = GameObject.FindGameObjectWithTag("Furacao").GetComponent<Transform>();
            posicaoPedra = GameObject.FindGameObjectWithTag("PedraIlha").GetComponent<Transform>();
            posicaoBoitata = GameObject.FindGameObjectWithTag("Boitata").GetComponent<Transform>();
            boitataEixo.SetActive(false);
            volumeMusica = SoundManager.instance.musicSource.volume;
            SoundManager.instance.FadeOut(SoundManager.instance.musicSource, 0.0f, 0.5f);
        }
        else
        {
            currentState = GameState.Gameplay;
        }



        if (GameObject.FindGameObjectWithTag("TextManager") != null)
        {
            tManager = GameObject.FindGameObjectWithTag("TextManager").GetComponent<TextManager>();
        }

        if (GameObject.FindGameObjectWithTag("MainCamera") != null)
        {
            aManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AnimationManager>();
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            pController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }

        if (GameObject.FindGameObjectWithTag("KeyManager") != null)
        {
            kManager = GameObject.FindGameObjectWithTag("KeyManager").GetComponent<KeyManager>();
        }

        if (GameObject.FindGameObjectWithTag("FadeManager") != null)
        {
            fManager = GameObject.FindGameObjectWithTag("FadeManager").GetComponent<FadeManager>();
        }

        if (GameObject.FindGameObjectWithTag("TrocaCor") != null)
        {
            tCor = GameObject.FindGameObjectWithTag("TrocaCor").GetComponent<TrocaCor>();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Ilha")
        {

            if (SoundManager.instance.musicSource.volume <= 0.0f)
            {
                SoundManager.instance.musicSource.clip = musica;
                SoundManager.instance.ambienteSource.clip = mar;
                SoundManager.instance.musicSource.Play();
                SoundManager.instance.ambienteSource.Play();
                SoundManager.instance.FadeIn(SoundManager.instance.musicSource, volumeMusica, 0.05f);
                SoundManager.instance.FadeIn(SoundManager.instance.ambienteSource, 0.15f, 0.05f);
            }
            /*
            if (boitataGrita)
            {
                tempoGrito -= Time.deltaTime;
                if (boitataEixo != null)
                {
                    boitataEixo.transform.Rotate(0, -1 * 6 * Time.deltaTime, 0, 0);
                }

                if (tempoGrito <= 0)
                {
                    tCor.trocaCor = true;
                    SoundManager.instance.efxSource.clip = boitata;
                    SoundManager.instance.efxSource.Play();
                    tempoGrito = 30f;
                }

                if (tempoGrito <= 27f && tempoGrito >= 26f)
                {
                    tCor.trocaCor = false;
                }              
            }
            */
        }

        if (currentState == GameState.Animacoes)
        {
            if (SceneManager.GetActiveScene().name == "MuseuFinal")
            {
                if (!chamaAnimacao)
                {
                    aManager.ChamaAnimacao("Final");
                    chamaAnimacao = true;
                }

                if (aManager.animacaoCompleta && !tManager.historiaCompleta)
                {
                    chamaAnimacao = false;
                    HistoriaFinal(0);
                }
            }
            else
            {
                if (!chamaAnimacao)
                {
                    aManager.ChamaAnimacao(tManager.numeroAnim);
                    chamaAnimacao = true;
                }

                if (aManager.animacaoCompleta)
                {
                    chamaAnimacao = false;
                    if (tManager.sceneToLoad != null)
                    {
                        currentState = GameState.Transicao;
                    }
                    else
                    {
                        currentState = GameState.Gameplay;
                    }
                }
            }
        }


        if (currentState == GameState.Gameplay)
        {
            if (SceneManager.GetActiveScene().name == "Ilha")
            {
                if (UIManager.UIGM.runaMao)
                {
                    pedra1.SetActive(false);
                    pedra2.SetActive(true);
                    UIManager.UIGM.runaMao = false;
                }

                if (runaErrada)
                {
                    parteHistoriaIlha = 3;
                    currentState = GameState.Historia;
                }
            }

            if (pController != null)
            {
                pController.podeAndar = true;
            }
        }
        else
        {
            if (pController != null)
            {
                pController.podeAndar = false;
            }
        }


        if (currentState == GameState.Minigame)
        {
            if (!chamaMinigame)
            {
                kManager.chamaMinigame();
                chamaMinigame = true;
                pedra2.SetActive(false);
                pedra1.SetActive(true);
                mouse.SetActive(false);
            }
            if (kManager.minigameCompleto)
            {
                chamaMinigame = false;
                inscricaoLigada.SetActive(true);
                luzInscricao.SetActive(true);
                inscricaoDesligada.SetActive(false);
                parteHistoriaIlha = 4;
                currentState = GameState.Historia;
            }
        }

        if (currentState == GameState.Historia)
        {
            if (tManager != null)
            {
                if (SceneManager.GetActiveScene().name == "Ilha")
                {
                    if (podeComecar)
                    {
                        HistoriaIlha();
                        if (tManager.parteHistoria > parte)
                        {
                            podeFalar = true;
                            parte = tManager.parteHistoria;
                            parteFala = 0;
                        }
                        if (tManager.index > parteFala)
                        {
                            podeFalar = true;
                            parteFala = tManager.index;
                        }

                        if (tManager.parteHistoria == 0 && tManager.index == 0 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[0];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 0 && tManager.index == 1 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[1];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 0 && tManager.index == 2 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[2];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 1 && tManager.index == 0 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[3];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 1 && tManager.index == 1 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[4];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 1 && tManager.index == 2 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[5];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 2 && tManager.index == 0 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[6];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 2 && tManager.index == 1 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[7];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 2 && tManager.index == 2 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[8];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 3 && tManager.index == 0 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[2];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 3 && tManager.index == 1 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[3];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 4 && tManager.index == 0 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[1];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.parteHistoria == 4 && tManager.index == 1 && podeFalar)
                        {
                            SoundManager.instance.playerSound.clip = indio[0];
                            SoundManager.instance.playerSound.Play();
                            podeFalar = false;
                        }
                        if (tManager.historiaCompleta && tManager.parteHistoria == 3)
                        {
                            currentState = GameState.Gameplay;
                            runaErrada = false;
                            chamaHistoria = false;
                            tManager.historiaCompleta = false;
                        }
                        if (tManager.historiaCompleta && tManager.parteHistoria == 4)
                        {
                            boitataEixo.SetActive(true);
                            posicaoObjeto = posicaoBoitata.position;
                            SoundManager.instance.efxSource.clip = boitata;
                            SoundManager.instance.efxSource.Play();
                            //chamaHistoria = false;
                            tManager.historiaCompleta = false;
                            if (!podeOlharTempo)
                            {
                                tempoOlhando = 10f;
                            }
                            podeOlharTempo = true;
                        }
                        if (tManager.historiaCompleta && tManager.parteHistoria == 5)
                        {
                            chamaHistoria = false;
                            tManager.historiaCompleta = false;
                            parteHistoriaIlha = 6;
                        }
                        if (tManager.historiaCompleta && tManager.parteHistoria == 6)
                        {
                            podeOlharTempo = false;
                            fManager.ChamaCena(tManager.sceneToLoad);
                            SoundManager.instance.FadeOut(SoundManager.instance.musicSource, 0.1f, 0.2f);
                            SoundManager.instance.FadeOut(SoundManager.instance.ambienteSource, 0.1f, 0.1f);
                            if (SoundManager.instance.musicSource.volume <= 0.2f)
                            {
                                SoundManager.instance.musicSource.clip = null;
                            }
                            if (SoundManager.instance.ambienteSource.volume <= 0.2f)
                            {
                                SoundManager.instance.ambienteSource.clip = null;
                            }
                        }
                        if (tempoOlhando < 0 && podeOlharTempo)
                        {
                            if (parteHistoriaIlha == 4)
                            {
                                posicaoFuracao.gameObject.SetActive(false);
                                furacao = false;
                            }
                            podeOlharTempo = false;
                            parteHistoriaIlha += 1;
                            chamaHistoria = false;
                            tManager.historiaCompleta = false;
                            tempoOlhando = 3f;
                        }
                        if (tManager.historiaCompleta && tManager.parteHistoria == 0)
                        {
                            posicaoObjeto = posicaoFuracao.position;
                            //SoundManager.instance.efxSource.clip = boitata;
                            //SoundManager.instance.efxSource.Play();
                            // posicaoObjeto = posicaoBoitata.position;
                            if (!podeOlharTempo)
                            {
                                tempoOlhando = 2f;
                            }
                            podeOlharTempo = true;

                            //tCor.trocaCor = true;
                            furacao = true;
                            SoundManager.instance.ambienteSource.clip = furacaoSom;
                        }                      
                        if (tManager.historiaCompleta && tManager.parteHistoria == 1)
                        {
                            currentState = GameState.Gameplay;
                            chamaHistoria = false;
                            tManager.historiaCompleta = false;
                        }
                        if (tManager.historiaCompleta && tManager.parteHistoria == 2)
                        {
                            currentState = GameState.Gameplay;
                            chamaHistoria = false;
                            tManager.historiaCompleta = false;
                        }

                    }
                }
                else
                {
                    if (tManager.historiaCompleta)
                    {
                        tManager.historiaCompleta = false;
                        chamaHistoria = false;

                        if (Cursor.visible)
                        {
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                        }

                        if (tManager.chamaAnim)
                        {
                            if (SceneManager.GetActiveScene().name == "MuseuFinal")
                            {
                                SoundManager.instance.FadeOut(SoundManager.instance.musicSource, 0.1f, 0.2f);
                                if (SoundManager.instance.musicSource.volume <= 0.2f)
                                {
                                    SoundManager.instance.musicSource.clip = null;
                                }
                                currentState = GameState.Transicao;
                            }
                            else
                            {
                                currentState = GameState.Animacoes;
                            }
                        }
                        else
                        {
                            currentState = GameState.Gameplay;
                        }
                    }
                }
            }
        }

        if (currentState == GameState.Transicao)
        {
            fManager.ChamaCena(tManager.sceneToLoad);

            if (SceneManager.GetActiveScene().name == "MuseuFinal")
            {
                if (SoundManager.instance.musicSource.volume <= 0.2f)
                {
                    SoundManager.instance.musicSource.clip = null;
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "MuseuFinal")
        {

        }
        else if (SceneManager.GetActiveScene().name == "Ilha")
        {
            if (podeOlharTempo && tempoOlhando > 0)
            {
                if (pController != null)
                {
                    if (posicaoObjeto != null)
                    {
                        if(parteHistoriaIlha == 4)
                        {
                            posicaoObjeto = posicaoBoitata.position;
                        }
                        Debug.Log("asdasd");
                        tempoOlhando -= Time.deltaTime;
                        Vector3 targetDir = posicaoObjeto - aManager.transform.position;
                        float step = 1.5f * Time.deltaTime;
                        Vector3 newDir = Vector3.RotateTowards(aManager.transform.forward, targetDir, step, 0);
                        Debug.DrawRay(aManager.transform.position, newDir, Color.red);
                        aManager.transform.rotation = Quaternion.LookRotation(newDir);
                    }
                }
            }
        }
    }

    public void Historia(int numero, Vector3 posicao)
    {
        if (!chamaHistoria)
        {
            posicaoObjeto = posicao;

            if (!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            tManager.parteHistoria = numero;
            StartCoroutine(tManager.Type());
            chamaHistoria = true;
            currentState = GameState.Historia;
        }
    }

    public void HistoriaIlha()
    {
        if (!chamaHistoria)
        {
            if (!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            tManager.parteHistoria = parteHistoriaIlha;
            StartCoroutine(tManager.Type());
            chamaHistoria = true;
        }
    }

    public void HistoriaFinal(int numero)
    {
        if (!chamaHistoria)
        {
            if (!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            tManager.parteHistoria = numero;
            StartCoroutine(tManager.Type());
            chamaHistoria = true;
            currentState = GameState.Historia;
        }
    }

    public void Animacao(int numero)
    {
        if (!chamaAnimacao)
        {
            currentState = GameState.Animacoes;

            chamaAnimacao = true;
        }
    }
    public IEnumerator Espera()
    {
        yield return new WaitForSeconds(4f);
        podeComecar = true;
    }
}
