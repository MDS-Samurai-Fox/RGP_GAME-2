using UnityEngine;

public class PlayerController : MonoBehaviour {

    private GameManager gm;
    public Transform shield;

    private float speed = 1.5f;

    // Power ups    
    private bool isAlive = true;
    private float speedPowerUpTimer = 0;
    private float speedDuration = 2.5f;
    private bool hasReceivedSpeedBoost = false;
    private bool isSpeedBosted = false;
    private int speedBoostType = 0;

    private float shieldPowerUpTimer = 0;
    private float shieldDuration = 4.5f;
    private bool isShielded = false;
    private bool hasActivatedShield = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        gm = FindObjectOfType<GameManager> ();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        FindObjectOfType<CameraFollow> ().SetPlayer(this.transform);

    }

    // Update is called once per frame
    void Update() {

        if (!isAlive)
            return;

        if (Input.GetButtonDown("Horizontal") && (Input.GetAxisRaw("Horizontal") > 0)) {

            gm.soundManager.Play(gm.soundManager.moveRight);

        } else if (Input.GetButtonDown("Horizontal") && (Input.GetAxisRaw("Horizontal") < 0)) {

            gm.soundManager.Play(gm.soundManager.moveLeft);

        } else { }

        this.transform.RotateAround(new Vector3(0, -0.75f, 0), Vector3.back, Input.GetAxis("Horizontal") * (speed * 100.0f) * Time.deltaTime);

        // Shield
        if (hasActivatedShield) {

            shield.gameObject.SetActive(true);
            if (isShielded) {
                shieldPowerUpTimer = 0;
            } else {
                isShielded = true;
            }
            hasActivatedShield = false;

        }

        if (isShielded) {

            shieldPowerUpTimer += Time.deltaTime;
            if (shieldPowerUpTimer > shieldDuration) {

                shieldPowerUpTimer = 0;
                isShielded = false;
                shield.gameObject.SetActive(false);

            }

        }

        // Speed boosts
        if (hasReceivedSpeedBoost) {

            if (isSpeedBosted) {
                speedPowerUpTimer = 0;
            } else {
                isSpeedBosted = true;
            }
            switch (speedBoostType) {
                case 1:
                    speed = 1.7f;
                    break;
                case 2:
                    speed = 1.3f;
                    break;
                case 3:
                    speed = 1.8f;
                    break;
                default:
                    break;
            }
            hasReceivedSpeedBoost = false;

        }

        if (isSpeedBosted) {

            speedPowerUpTimer += Time.deltaTime;
            if (speedPowerUpTimer > speedDuration) {

                speedPowerUpTimer = 0;
                isSpeedBosted = false;
                speed = 1.5f;

            }

        }

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {

        print("-- Collided with " + other.name);

        gm.cf.Shake();

        if (other.CompareTag("AntiMatter")) {

            if (isShielded) {

                ToggleShield();

            } else {

                isAlive = false;
                StartCoroutine(gm.GameOver());

            }

        } else if (other.CompareTag("Diamond")) {
            
            gm.score += 20;

            gm.ChangeColor(new Color(175, 51, 255), 0.75f);

            hasActivatedShield = true;

            gm.soundManager.Play(gm.soundManager.shield);

        } else if (other.CompareTag("Hydrogen")) {
            
            gm.score += 15;

            gm.ChangeColor(new Color(51, 204, 255), 0.75f);

            gm.soundManager.Play(gm.soundManager.hydrogen);

            hasReceivedSpeedBoost = true;

            speedBoostType = 1;

        } else if (other.CompareTag("Nitrogen")) {
            
            gm.score += 35;

            gm.ChangeColor(new Color(102, 255, 51), 0.75f);

            gm.soundManager.Play(gm.soundManager.nitrogen);

            hasReceivedSpeedBoost = true;

            speedBoostType = 2;

        } else if (other.CompareTag("Plutonium")) {
            
            gm.score += 25;

            gm.ChangeColor(new Color(255, 114, 51), 0.75f);

            gm.soundManager.Play(gm.soundManager.plutonium);

            hasReceivedSpeedBoost = true;

            speedBoostType = 3;

        } else {

        }

        Destroy(other.gameObject);

    }

    void ToggleShield() {

        shieldPowerUpTimer = 0;
        hasActivatedShield = false;
        isShielded = !isShielded;
        shield.gameObject.SetActive(!shield.gameObject.activeSelf);

    }

}