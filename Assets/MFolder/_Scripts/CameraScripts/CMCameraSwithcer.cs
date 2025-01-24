using UnityEngine;
using UnityEngine.InputSystem;

public class CMCameraSwithcer : MonoBehaviour
{
    [SerializeField] InputAction action;

    Animator animator;

    bool mainFollowCamera = true;

    void Awake(){
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        action.performed += _ => SwitchState();
    }

    void OnEnable(){
        action.Enable();
    }

    void OnDisable(){
        action.Disable();
    }

    private void SwitchState(){
        if (mainFollowCamera){
            animator.Play("GhostCamera");
        }
        else{
            animator.Play("FollowCamera");
        }
        mainFollowCamera = !mainFollowCamera;
    }
}
