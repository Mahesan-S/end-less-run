using UnityEngine;

public class Coins : MonoBehaviour{

    static int coins;
    void Start(){
    }

    private void OnTriggerEnter(Collider other){

        if (other.gameObject.CompareTag("Player")) {
            
            GameObject.Find("Canvas").GetComponent<CanvasManger>().UpdateScore(++coins);
            Destroy(this.gameObject);
        }
    }
}
