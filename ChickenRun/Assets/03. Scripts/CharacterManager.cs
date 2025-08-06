
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>(); //addComponet로 스크립트 붙여주기
            }
            return _instance;
        }
    }

    public Character _character;
    public Character Character
    {
        get { return _character; }
        set { _character = value; }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
