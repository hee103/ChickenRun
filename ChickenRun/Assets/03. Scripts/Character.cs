
using UnityEngine;


public class Character : MonoBehaviour
{
    public CharacterController CharacterController { get; private set; }
    public ChickenController ChickenController { get; private set; }
    public FarmerController FarmerController { get; private set; }

    private void Awake()
    {
        CharacterManager.Instance.Character = this;

        CharacterController = GetComponent<CharacterController>();
        ChickenController = GetComponent<ChickenController>();
        FarmerController = GetComponent<FarmerController>();

        if (ChickenController != null)
        {
            Debug.Log("´ß Ä³¸¯ÅÍ");
        }
        else if (FarmerController != null)
        {
            Debug.Log("³óºÎ Ä³¸¯ÅÍ");
        }
    }
}



