using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class Character : MonoBehaviour
{
    public ChickenController ChickenController;

    private void Awake()
    {
        CharacterManager.Instance.Character = this;
        ChickenController= GetComponent<ChickenController>();
    }
   
}
