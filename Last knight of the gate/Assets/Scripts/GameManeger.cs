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
    public int coinCount=1;
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
        vida = 600;
        MaxVida = 600;
        dano = 20;
        defesa = 4;
        regenVida = 1;
        coinCount = 1;
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
            atacando= true;
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
        //Debug.Log("Vida Atual " + vida);
    }

    private bool isGameOver()
    {
        if(vida <= 0){
            return true;
        }
        return false;
    }
}
