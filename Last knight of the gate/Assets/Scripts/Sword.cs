using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    BoxCollider bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManeger.Instance.getAtacando())
        {
            bc.isTrigger = false;
        }
        else
        {
            bc.isTrigger = true;
        }
        
    }
}
