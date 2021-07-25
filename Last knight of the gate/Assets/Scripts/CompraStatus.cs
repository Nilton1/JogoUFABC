using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompraStatus : MonoBehaviour
{
    public bool regenVida;
    public bool dano;
    public bool defesa;
    public TextMeshPro mensagem;
    // Start is called before the first frame update
    void Start()
    {
        if (dano)
        {

            mensagem.text = "Comprar força!";

        }
        else if (regenVida)
        {

            mensagem.text = "Comprar regeneração de vida!";

        }
        else if (defesa)
        {
            mensagem.text = "Comprar Defesa!";
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerWepon") && GameManeger.Instance.getAtacando())
        {
            if (GameManeger.Instance.coinCount > 0)
            {
                if (dano)
                {

                    GameManeger.Instance.dano += 1;
                    GameManeger.Instance.coinCount -= 1;

                }
                else if (regenVida)
                {

                    GameManeger.Instance.regenVida += 1;
                    GameManeger.Instance.coinCount -= 1;

                }
                else if (defesa)
                {

                    GameManeger.Instance.defesa += 1;
                    GameManeger.Instance.coinCount -= 1;

                }

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PlayerWepon") && GameManeger.Instance.getAtacando())
        {
            if (GameManeger.Instance.coinCount > 0)
            {
                if (dano)
                {

                    GameManeger.Instance.dano += 1;
                    GameManeger.Instance.coinCount -= 1;

                }
                else if (regenVida)
                {

                    GameManeger.Instance.regenVida += 1;
                    GameManeger.Instance.coinCount -= 1;

                }
                else if (defesa)
                {

                    GameManeger.Instance.defesa += 1;
                    GameManeger.Instance.coinCount -= 1;

                }

            }
        }

    }
}
