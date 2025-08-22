using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractPoint : MonoBehaviour
{
    public float maxCheckDistance = 3f; // 상호작용 거리
    public LayerMask layerMask;         // Item 레이어만 체크

    [SerializeField]private Camera camera;
    private GameObject curInteractGameObject;
    public ChickenController chickenController;

    void Start()
    {
        //camera = Camera.main;
        
    }

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );

        Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            if (hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
            }
        }
        else
        {
            curInteractGameObject = null;
        }
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractGameObject != null)
        {
            Destroy(curInteractGameObject);
            chickenController.Heal();
            curInteractGameObject = null;


        }
    }
}
