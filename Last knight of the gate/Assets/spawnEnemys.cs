using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemys : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        int level = 0;
        level = GameManeger.Instance.level;
        for (int i = 0; i < level; i++)
        {
            Instantiate(enemy);
        }


    }

    // Update is called once per frame

}
