
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoviment : MonoBehaviour
{
    public Text coinCountText;
    public Text level;
    public Text exp;
    Rigidbody rb;
    Vector3 Vec;
    float rotacao;
    float rotacaox;
    float rotacaoFinal;
    float rotacaoy;
    public float speed = 5.0f;
    bool correndo;
    bool andando;
    float cooldownTime;
    float cooldownTimeJump;
    float cooldownTimeRegen;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        correndo = false;
        andando = false;
        cooldownTime = 0;
        cooldownTimeJump = 0;
        cooldownTimeRegen = 1;
    }

    private void FixedUpdate()
    {
        coinCountText.text = GameManeger.Instance.coinCount.ToString();
        level.text = "Level: " + GameManeger.Instance.level.ToString();
        exp.text = "Exp: " + GameManeger.Instance.expAtual.ToString() + "/" + GameManeger.Instance.nextLevel.ToString();
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float correr = Input.GetAxis("Fire3");
        if ((x > 0.25 || x < -0.25) || (y > 0.25 || y < -0.25))
        {
            andando= true;
        }
        else
        {
            andando=false;
        }

        if (correr != 0 && cooldownTime == 0)
        {
            if (correndo)
            {
                correndo= false;
            }
            else
            {
                correndo= true;
            }
            cooldownTime = 1;

        }

        if (cooldownTimeRegen == 0 && GameManeger.Instance.vida>0)
        {
            
            GameManeger.Instance.vida = Mathf.Clamp(GameManeger.Instance.vida + GameManeger.Instance.regenVida, 0, GameManeger.Instance.MaxVida);
            
            cooldownTimeRegen = 1;

        }

        cooldownTimeRegen = Mathf.Clamp(cooldownTimeRegen - Time.deltaTime, 0, Mathf.Infinity);
        cooldownTime = Mathf.Clamp(cooldownTime - Time.deltaTime, 0, Mathf.Infinity);
        cooldownTimeJump = Mathf.Clamp(cooldownTimeJump - Time.deltaTime, 0, Mathf.Infinity);
        if (andando && correndo)
        {
            speed = 9.5f;
        }
        else
        {
            speed = 5f;
        }
        Vec = transform.localPosition;

        if (cooldownTimeJump != 0 || Input.GetAxis("Jump")==0)
        {
            rb.AddForce(Vector3.zero);
        }
        else
        {
            Vector3 force = new Vector3(0, 15, 0);
            rb.AddForce(force * 700);

            cooldownTimeJump = 1.0f;
        }

        
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.localPosition = Vec;




        //transform.Rotate(rotacao);
        
        if (y > 0.25)
        {
            rotacaoy = 180f;
        }
        else if (y < -0.25)
        {
            rotacaoy = 0f;
        }

        
        if (x > 0.25)
        {
            rotacaox = 90f;
        }
        else if (x < -0.25)
        {
            rotacaox = 270f;
        }

        
        if ((x > 0.25 || x < -0.25) && (y > 0.25 || y < -0.25))
        {
            if (y < -0.25 && x < -0.25)
            {
                rotacaoy = 360f;
            }
            rotacaoFinal = (rotacaoy + rotacaox) / 2;
            rotacao = rotacaoFinal;

        }
        else if(x > 0.25 || x < -0.25)
        {
            rotacaoFinal = rotacaox;
            rotacao = rotacaoFinal;

        }
        else if (y > 0.25 || y < -0.25)
        {
            rotacaoFinal = rotacaoy;
            rotacao = rotacaoFinal;
        }
        else
        {
            rotacaoFinal = rotacao;
        }

        if (transform.rotation.y != rotacaoFinal && ((x > 0.25 || x < -0.25) || (y > 0.25 || y < -0.25)))
        {
            transform.rotation = Quaternion.Euler(0, -rotacaoFinal, 0);

            //transform.Rotate(0, -ratacaox * Time.deltaTime, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -rotacao, 0);
        }



    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            var inimigo = collision.gameObject.GetComponent<Enemy>();
            if (inimigo !=null) {
                GameManeger.Instance.CalculaDanoRecebido(inimigo.getDano());
            }
            Vector3 offset = transform.position - collision.gameObject.transform.position;
            offset.Normalize();
            offset.y /= 4;
            rb.AddForce(offset * 40 * 500);

        }
    }

}
