using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    int ChickenSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
        //CharacterController.Awake();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Chickenmove();
    }
    private void Chickenmove()
    {
        CharacterController.Instance.Move(ChickenSpeed);
    }
}
