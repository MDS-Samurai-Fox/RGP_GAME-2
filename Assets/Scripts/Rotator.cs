using System;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public bool rotateX, rotateY, rotateZ;

    private Vector3 rotation;
    // Update is called once per frame
    void Update() {

        if (Input.GetAxisRaw("Horizontal") >= 0) {
            this.transform.Rotate(new Vector3(Convert.ToInt16(rotateX), Convert.ToInt16(rotateY), Convert.ToInt16(rotateZ)) * 50 * Time.deltaTime);
        } else if (Input.GetAxisRaw("Horizontal") < 0) {
            this.transform.Rotate(new Vector3(-Convert.ToInt16(rotateX), -Convert.ToInt16(rotateY), -Convert.ToInt16(rotateZ)) * 50 * Time.deltaTime);
        }

    }

}