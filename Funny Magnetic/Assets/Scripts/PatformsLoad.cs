using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PatformsLoad : MonoBehaviour{

    GameObject FirstGameObject;
    List<GameObject> PlatformPrefab = new List<GameObject>();

    List<GameObject> PlatformList = new List<GameObject>();
    float Timer = 5, DestoryTimer = 17;
    
    void Start(){
        PlatformPrefab.Add(Resources.Load<GameObject>("Road"));
        PlatformPrefab.Add(Resources.Load<GameObject>("Road_0"));
        PlatformPrefab.Add(Resources.Load<GameObject>("Road_1"));
        PlatformPrefab.Add(Resources.Load<GameObject>("Road_2"));
        PlatformPrefab.Add(Resources.Load<GameObject>("Road_3"));


        PlatformList.Add(Instantiate(PlatformPrefab[0],
                        new Vector3(transform.position.x, 1f, transform.position.z + 5),
                        Quaternion.identity));

        SpawnNextPlane();
        SpawnNextPlane();
        SpawnNextPlane();
    }

    void FixedUpdate(){
        if(Timer < 0) {
            SpawnNextPlane();
            Timer = 5f;
        }
        Timer -= Time.deltaTime;
        RemovePlane();

    }
    
    void SpawnNextPlane() {
        int range = Random.Range(0, PlatformPrefab.Count);

        Transform previousLocation = PlatformList[PlatformList.Count - 1].GetComponent<Transform>();
        
        PlatformList.Add(Instantiate(PlatformPrefab[range],
                        new Vector3(previousLocation.position.x, previousLocation.position.y, previousLocation.position.z + 28),
                        Quaternion.identity));
    }

    void RemovePlane() {
        if(DestoryTimer < 0) {
            Destroy(PlatformList[0]);
            PlatformList.RemoveAt(0);
            DestoryTimer = 17f;
            
        }
        DestoryTimer -= Time.deltaTime;
        
    }

}
