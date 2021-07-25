using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = GameManeger.Instance.vida / GameManeger.Instance.MaxVida;
    }
}
