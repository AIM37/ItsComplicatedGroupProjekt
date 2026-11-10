using UnityEngine;
public class WallManager : MonoBehaviour{
    private void Awake(){
        int obstacleLayer = LayerMask.NameToLayer("Obstacle");
        foreach (Transform child in transform){
            if (child.CompareTag("NoCollision")) continue;
            if (child.GetComponent<BoxCollider2D>() == null){
                child.gameObject.AddComponent<BoxCollider2D>();
            }
            child.gameObject.layer = obstacleLayer;
        }
    }
}