using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    CharacterController PlayerController;
    Vector3 CurrentPosition;
    Animator PlayerAnimation;

    [SerializeField] float PlayerSpeed;
    [SerializeField] float JumpForce;
    float PlayerSpeedTimer;
    float gravity = -9.8f;
    int Lane;
    float PlayerLaneChangeSpeed;
    bool IsMoving;
    
    void Start(){        
        PlayerController = GetComponent<CharacterController>();
        PlayerAnimation = GetComponent<Animator>();
        Time.timeScale = 0f;
    }
    void Awake(){
        Lane = 2;
        PlayerSpeedTimer = 180f;
        PlayerLaneChangeSpeed = 3.5f;
        IsMoving = false;
    }

    void Update(){
        
        CurrentPosition.z = PlayerSpeed;
        MoveLeftAndRight();
        
    }
    void FixedUpdate(){

        if(IsMoving == true) { 
            AutoMoveForward();
            updateSpeed();
        }
    }
    void updateSpeed() {
        if (PlayerSpeedTimer < 0) {
            PlayerSpeed = PlayerSpeed + 1f + Time.deltaTime;
        }
        PlayerSpeedTimer -= Time.deltaTime;
    }
    void MoveLeftAndRight() {
        Vector3 targetPostion = transform.position.z * Vector3.forward + transform.position.y * Vector3.up;

        if (Input.GetKeyDown(KeyCode.DownArrow)){
            StartCoroutine(Slide());
        }

        if (PlayerController.isGrounded){
            CurrentPosition.y = -1f;
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                StartCoroutine(JumpPlayer());
            }
        }
        else
        {
            CurrentPosition.y += gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Lane ++;
            if(Lane > 3) { 
                Lane = 3; 
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Lane --;
            if (Lane < 1) {  
                Lane = 1; 
            }
        }
        
        if(Lane == 1) { targetPostion += Vector3.left * PlayerLaneChangeSpeed; }
        else if(Lane == 2){ targetPostion += Vector3.left * 0; }
        else { targetPostion += Vector3.right * PlayerLaneChangeSpeed; }

        //transform.position = Vector3.Lerp(transform.position, targetPostion, 60 * Time.deltaTime);
        if (transform.position == targetPostion)
            return;
        Vector3 differrent = targetPostion - transform.position;
        Vector3 moveDirection = differrent.normalized * 25 * Time.deltaTime;
        if (moveDirection.sqrMagnitude < differrent.sqrMagnitude)
        {
            PlayerController.Move(moveDirection);
        }
        else
            PlayerController.Move(differrent);
    }
    void AutoMoveForward(){
        PlayerController.Move(CurrentPosition * Time.fixedDeltaTime);
        //transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }
    public void StartRun() {
        StartCoroutine(Run());
    }
    IEnumerator Run() {
        PlayerAnimation.SetInteger("States", 1);
        yield return new WaitForSeconds(0.5f);
        IsMoving = true;
    }
    IEnumerator  Slide() {
        
        PlayerAnimation.SetInteger("States", 3);
        yield return new WaitForSeconds(1f);
        PlayerAnimation.SetInteger("States", 1);
        
    }
    IEnumerator JumpPlayer() {
       PlayerAnimation.SetInteger("States", 2);
        CurrentPosition.y = JumpForce;
        yield return new WaitForSeconds(1f);
       PlayerAnimation.SetInteger("States", 1);
    }

   

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if (hit.gameObject.CompareTag("Obticals")) {
            Time.timeScale = 0f;
            GameObject.Find("Canvas").GetComponent<CanvasManger>().ResartPanelOpen();
        }
    }
}
