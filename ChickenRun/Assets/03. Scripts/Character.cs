
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
