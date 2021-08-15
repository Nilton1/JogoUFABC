using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public float vida = 100;
    public float MaxVida = 100;
    public float dano = 35;
    public float defesa = 5;
    public int recompensa = 1;
    float cooldownTimeReceberDano;
    float cooldownAvancar;

    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        cooldownTimeReceberDano = 1;
        cooldownAvancar = 1;
        
        MaxVida = 100 + (GameManeger.Instance.level-1)*40;
        vida = MaxVida;
        dano = 35 + (GameManeger.Instance.level-1) * 5;
        defesa = 5 + (GameManeger.Instance.level - 1);
        recompensa =(GameManeger.Instance.level)*2 ;
    }

    public float getDano()
    {
        return dano;
    }
    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player.transform, Vector3.down);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        cooldownTimeReceberDano = Mathf.Clamp(cooldownTimeReceberDano - Time.deltaTime, 0, Mathf.Infinity);

        if (cooldownAvancar != 0 && !isGrounded)
        {
            rb.AddForce(Vector3.zero);
        }
        else
        {
            Vector3 offset = transform.position - player.transform.position;
            offset.Normalize();
            offset.x = 0;
            offset.y *= 0;
            offset.z = 0;
            Vector3 force = new Vector3(0, 9, 0);
            rb.AddForce(force * 500);
            rb.AddForce(transform.forward * 6000);
            cooldownAvancar = 1.5f;
        }
        cooldownAvancar = Mathf.Clamp(cooldownAvancar - Time.deltaTime, 0, Mathf.Infinity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PlayerWepon") && cooldownTimeReceberDano == 0 && GameManeger.Instance.getAtacando())
        {
            cooldownTimeReceberDano = 1;
            //Debug.Log("Dano recebido");
            float PlayerDano = GameManeger.Instance.getDano();
            if (defesa > PlayerDano)
            {
                //Debug.Log("1 de Dano");
                vida -= 1;
            }
            else
            {
                //Debug.Log("Dano Causado no inimigo " + (PlayerDano - defesa));
                vida -= PlayerDano - defesa;
            }

            vida = Mathf.Clamp(vida, 0, MaxVida);
            //Debug.Log("Vida Atual do inimigo " + vida);



            Vector3 offset = transform.position - collision.gameObject.transform.position;
            offset.Normalize();
            // offset.y /= 4;
            rb.AddForce(offset * 40 * 1000);
            if (vida == 0)
            {
                GameManeger.Instance.coinCount += recompensa;
                GameManeger.Instance.recebeEXP(3);
                Destroy(this.gameObject);
            }

        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
