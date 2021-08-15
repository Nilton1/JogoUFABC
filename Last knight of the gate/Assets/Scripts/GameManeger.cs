using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManeger : MonoBehaviour
{
    private static GameManeger instance = null;
    public float vida;
    public float MaxVida;
    public float dano;
    public float defesa;
    public bool atacando;
    public float regenVida;
    public int coinCount = 1;
    public int level = 1;
    public int nextLevel = 10;
    public int expAtual = 0;
    private float fatorLevel = 1.0f;
    public int experience = 0;
    // Start is called before the first frame update
    public static GameManeger Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("GM").AddComponent<GameManeger>();
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        atacando = false;
        ResetPlayerStats();
    }

    public void ResetPlayerStats()
    {
        vida = 600;
        MaxVida = 600;
        dano = 20;
        defesa = 4;
        regenVida = 1;
        coinCount = 1;
        level = 1;
        nextLevel = 10;
        expAtual = 0;
        fatorLevel = 1.0f;
        experience = 0;
    }
    public float getDano()
    {
        return dano;
    }

    public bool getAtacando()
    {
        return atacando;
    }

    // Update is called once per frame
    void Update()
    {
        float atacar = Input.GetAxis("Fire1");
        if (atacar != 0)
        {
            atacando = true;
        }
        else
        {
            atacando = false;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            //apenas compilado
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public void CalculaDanoRecebido(float danoMonstro)
    {
        if (defesa > dano)
        {
            Debug.Log("1 de Dano");
            vida -= 1;
        }
        else
        {
            //Debug.Log("Dano Causado " + (danoMonstro - defesa));
            vida -= danoMonstro - defesa;
        }
        vida = Mathf.Clamp(vida, 0, MaxVida);
        if (isGameOver())
        {
            SceneManager.LoadScene("Menu");
        }
        //Debug.Log("Vida Atual " + vida);
    }

    public void recebeEXP(int exp)
    {
        expAtual += exp;

        if (expAtual >= nextLevel)
        {
            MaxVida += 30;
            vida = MaxVida;
            dano += 4;
            defesa += 2;
            if (expAtual > nextLevel)
            {
                int diferenca = 0;
                diferenca = expAtual - nextLevel;
                level += 1;
                expAtual = diferenca;
            }
            else if (expAtual == nextLevel)
            {
                level += 1;
                expAtual = 0;
            }
            nextLevel = (int)Math.Pow(10, fatorLevel);
            fatorLevel += 0.2f;
        }
    }
    private bool isGameOver()
    {
        if (vida <= 0)
        {
            return true;
        }
        return false;
    }
}
