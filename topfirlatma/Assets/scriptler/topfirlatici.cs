using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class topfirlatici : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging;
    public float delaysüresi = 1f;
    public float respawnsüresi = 1f;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    
    private Rigidbody2D currentBallRigidBody;
    private SpringJoint2D currentBallSprintJoint;
    void Start()
    {
         mainCamera = Camera.main;
         SpawnNewBall();
    }

    void Update()
    {   

        if(currentBallRigidBody == null) {return;}
        if (! Touchscreen.current.primaryTouch.press.isPressed)

        {   
            if(isDragging){

                LaunchBall();
            }

            isDragging = false;

            currentBallRigidBody.isKinematic = false;

            return;
        }

        isDragging = true;
        
        currentBallRigidBody.isKinematic = true;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidBody.position = worldPosition;

        
    }

    private void SpawnNewBall()

    {
        GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);

        currentBallRigidBody = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSprintJoint = ballInstance.GetComponent<SpringJoint2D>();
        currentBallSprintJoint.connectedBody = pivot;


    }
    private void LaunchBall() {

        currentBallRigidBody.isKinematic = false;
        currentBallRigidBody = null;

        Invoke(nameof(DetachBall), delaysüresi);
        
        
    }

    private void DetachBall(){

        currentBallSprintJoint.enabled = false;
        currentBallSprintJoint = null;

        Invoke(nameof(SpawnNewBall), respawnsüresi);
    }
}
