using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform cylinder;

    [RangeAttribute(1, 2)]
    public float speed = 1;

    private Rigidbody rb;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {

        rb = GetComponentInChildren<Rigidbody> ();

    }

    // Update is called once per frame
    void Update() {

        this.transform.RotateAround(cylinder.position, Vector3.back, Input.GetAxis("Horizontal") * (speed * 100.0f) * Time.deltaTime);

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Good")) {
			
			

        } else if (other.CompareTag("Bad")) {
			
			

        } else {
			
			

        }

    }

}