using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour{

    [SerializeField] Transform playerPostion;
    [SerializeField] Animation cameraAniation;

    private void Start(){
        cameraAniation = GetComponent<Animation>();
        EnableCamera();
    }
    public void EnableCamera() {
        cameraAniation.Play();
    }

    private void FixedUpdate(){
        this.transform.position = new Vector3(transform.position.x, playerPostion.position.y + 5f, playerPostion.position.z - 10f);
    }
}
