using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController),typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    private CharacterController characterController;
    private Camera cam;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
        if( input != Vector3.zero)
        {
            agent.enabled = false;
            var transformedInput = Matrix4x4.Rotate(Quaternion.Euler(0,45,0)).MultiplyPoint3x4(input);
            transform.rotation = Quaternion.LookRotation(transformedInput,Vector3.up);
            characterController.Move(transform.forward * Time.deltaTime * speed);
        }
        else
        {
            agent.enabled = true;
            if( Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
}
