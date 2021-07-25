
using UnityEngine;

public class PlayerAnimations: MonoBehaviour
{
    Animator animator;
    int andando;
    int correndo;
    int atacando;
    float cooldownTime;
    // Start is called before the first frame update
    void Start()
    {
        cooldownTime = 0;
        animator = GetComponent<Animator>();
        andando = Animator.StringToHash("movimentando");
        correndo = Animator.StringToHash("correndo");
        atacando = Animator.StringToHash("atacar");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float correr = Input.GetAxis("Fire3");
        float atacar = Input.GetAxis("Fire1");
        if ((x > 0.25 || x < -0.25) || (y > 0.25 || y < -0.25))
        {
            animator.SetBool(andando, true);
        }
        else { 
            animator.SetBool(andando, false);
        }

        if (correr != 0 && cooldownTime==0)
        {
            if (animator.GetBool(correndo)) {
                animator.SetBool(correndo, false);
            }
            else {
                animator.SetBool(correndo, true);
            }
            cooldownTime = 1;

        }

        if (atacar != 0 )
        {
            animator.SetBool(atacando, true);
        }
        else{
            animator.SetBool(atacando, false);
        }
        cooldownTime = Mathf.Clamp(cooldownTime - Time.deltaTime, 0, Mathf.Infinity);

    }
}
