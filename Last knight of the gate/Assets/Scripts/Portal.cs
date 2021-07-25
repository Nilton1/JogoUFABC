using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool irLevel1;
    public bool irSafeArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (irLevel1){
                SceneManager.LoadScene("Level1");
            }
            else if (irSafeArea)
            {
                SceneManager.LoadScene("SafeArea");
            }

        }
    }
}
