using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPoint : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, maxCheckDistance,layerMask))
        {
            if(hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
                
            }
        }
        else
        {

        }
    }
}
