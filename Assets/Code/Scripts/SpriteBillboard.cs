using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    private Transform target;
    [SerializeField] public bool rotateYAxis;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (!rotateYAxis)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
        else
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            //transform.rotation = Camera.main.transform.rotation;
            //transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }
}
