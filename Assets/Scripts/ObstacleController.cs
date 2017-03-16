using UnityEngine;

public class ObstacleController : MonoBehaviour {

    private float speed;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        // speed = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {

        this.transform.position -= (Vector3.forward * speed * Time.deltaTime);

        if (this.transform.position.z < -10) {
            Destroy(this.gameObject);
        }

    }
    
    public void SetSpeed(float _speed) {
        
        speed = _speed;
        
    }

}