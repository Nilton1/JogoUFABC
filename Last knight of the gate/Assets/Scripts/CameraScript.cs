using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    Vector3 offset;
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0,9,-10);
        transform.rotation = Quaternion.Euler(45, 0, 0);
        transform.position = player.transform.position + offset;
    }
}
