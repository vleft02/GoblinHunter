using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }


    void Update()
    {
        //transform.rotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }
}
