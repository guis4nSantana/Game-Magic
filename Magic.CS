using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Principal : MonoBehaviour
{
    public GameObject jogadorFelpudo;
    public GameObject FelpudoIdie;
    public GameObject FelpudoBate;

    public GameObject barril;
    public GameObject inimEsq;
    public GameObject inimDir;

    public Text pontuacao;
    public GameObject barra;

    public AudioClip somBate;
    public AudioClip somPerde;


    //Novas Variaveis

    private int record = 0;
    [SerializeField] private GameObject gameOverPanel, gamePanel, barGame, newRecord, CharactersPanel;
    [SerializeField] private Text scoreTxt, recordTxt;

    //Novas Variaveis

    float escalaJogadorHorizontal;
    private List<GameObject> listaBlocos;
    bool ladoPersonagem;
    int score;

    bool comecou;
    bool acabou;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteKey("Record");

        if (PlayerPrefs.HasKey("Record"))
        {
            record = PlayerPrefs.GetInt("Record");
        }
        else record = 0;


        escalaJogadorHorizontal = transform.localScale.x;


        FelpudoBate.SetActive(false);
        listaBlocos = new List<GameObject>();
        CriaBarrisInicio();

        pontuacao.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        pontuacao.text = "START";
        pontuacao.fontSize = 25;


        barGame.SetActive(true);//modificado
        gamePanel.SetActive(true);//modificado
        gameOverPanel.SetActive(false);//modificado
        newRecord.SetActive(false);//modificado
        CharactersPanel.SetActive(false);


    }


    // Update is called once per frame
    void Update()
    {
        if (!acabou)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!comecou)
                {
                    comecou = true;
                    barra.SendMessage("Comecou");

                }

                GetComponent<AudioSource>().PlayOneShot(somBate);


                if (Input.mousePosition.x > Screen.width / 2)
                {

                    bateDireita();
                }
                else
                {
                    bateEsquerda();


                }
                listaBlocos.RemoveAt(0);
                ReposicionaBlocos();
                confereJogada();
            }
        }

    }

    void bateDireita()
    {
        ladoPersonagem = true;
        FelpudoIdie.SetActive(false);
        FelpudoBate.SetActive(true);
        jogadorFelpudo.transform.position = new Vector2(1.1f, jogadorFelpudo.transform.position.y);
        jogadorFelpudo.transform.localScale = new Vector2(-escalaJogadorHorizontal, jogadorFelpudo.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
        listaBlocos[0].SendMessage("BateDireita");
    }

    void bateEsquerda()
    {
        ladoPersonagem = false;
        FelpudoIdie.SetActive(false);
        FelpudoBate.SetActive(true);
        jogadorFelpudo.transform.position = new Vector2(-1.1f, jogadorFelpudo.transform.position.y);

        jogadorFelpudo.transform.localScale = new Vector2(escalaJogadorHorizontal, jogadorFelpudo.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
        listaBlocos[0].SendMessage("BateEsquerda");
    }








    void VoltaAnimacao()
    {
        FelpudoIdie.SetActive(true);
        FelpudoBate.SetActive(false);
    }


    GameObject CriaNovoBarril(Vector2 posicao)
    {
        GameObject novoBarril;

        if (Random.value > 0.5f || (listaBlocos.Count < 2))
        {
            novoBarril = Instantiate(barril);



        }
        else
        {
            if (Random.value > 0.5f)
            {
                novoBarril = Instantiate(inimDir);

            }
            else
            {
                novoBarril = Instantiate(inimEsq);

            }




        }

        novoBarril.transform.position = posicao;

        return novoBarril;

    }


    void CriaBarrisInicio()
    {
        for (int i = 0; i <= 7; i++)
        {

            GameObject objetoBarril = CriaNovoBarril(new Vector2(0, -4.1f + (i * 2.8f)));
            listaBlocos.Add(objetoBarril);
        }
    }

    void ReposicionaBlocos()
    {

        GameObject objetoBarril = CriaNovoBarril(new Vector2(0, -4.1f + (8 * 2.8f)));
        listaBlocos.Add(objetoBarril);

        for (int i = 0; i <= 7; i++)
        {
            listaBlocos[i].transform.position = new Vector2(listaBlocos[i].transform.position.x, listaBlocos[i].transform.position.y - 2.8f);
        }
    }



    void confereJogada()
    {
        if (listaBlocos[0].gameObject.CompareTag("Inimigo"))
        {
            if ((listaBlocos[0].name == "inimigoEsq(Clone)" && ladoPersonagem) || (listaBlocos[0].name == "inimigoDir(Clone)" && !ladoPersonagem))
            {
                FimDeJogo();
            }
            else
            {
                MarcaPonto();

            }
        }
        else
        {
            MarcaPonto();
        }



    }

    void MarcaPonto()
    {
        score++;
        pontuacao.text = score.ToString();
        pontuacao.fontSize = 30;
        pontuacao.color = new Color(0.95f, 9.0f, 0.35f);
        pontuacao.transform.position = new Vector2(Screen.width / 2, Screen.height / 1.4f);
        barra.SendMessage("AumentaBarra");

    }
    void FimDeJogo()//modificado
    {
        acabou = true;
        FelpudoBate.GetComponent<SpriteRenderer>().color = new Color(1f, 0.25f, 0.25f, 1.0f);
        FelpudoIdie.GetComponent<SpriteRenderer>().color = new Color(1f, 0.25f, 0.25f, 1.0f);

        jogadorFelpudo.GetComponent<Rigidbody2D>().isKinematic = false;


        if (ladoPersonagem)
        {
            jogadorFelpudo.GetComponent<Rigidbody2D>().AddTorque(-100.0f);
            jogadorFelpudo.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, 3.0f);
        }
        else
        {
            jogadorFelpudo.GetComponent<Rigidbody2D>().AddTorque(100.0f);
            jogadorFelpudo.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 3.0f);
        }
        GetComponent<AudioSource>().PlayOneShot(somPerde);

        //Coisas Novas:


        scoreTxt.text = score.ToString();
        if (score > record)
        {
            record = score;
            PlayerPrefs.SetInt("Record", record);
            newRecord.SetActive(true);
        }

        recordTxt.text = record.ToString();

        barGame.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void CharacterMenu()
    {
        CharactersPanel.SetActive(true);
        barGame.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        newRecord.SetActive(false);

    }

    public void RecarregaCena()
    {
        SceneManager.LoadScene("MinhaCena");
    }
}