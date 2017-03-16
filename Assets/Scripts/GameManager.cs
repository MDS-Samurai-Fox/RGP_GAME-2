using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Transform player, spawner;
    [HideInInspector]
    public CameraFollow cf;
    private Spawner spawnManager;
    private PlayerController playerController;
    [HideInInspector]
    public SoundManager soundManager;

    public Material skybox;

    [HideInInspector]
    public bool isPlaying = false;
    
    public Text scoreText;
    public RectTransform resetButton;

    public int score;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {

        cf = FindObjectOfType<CameraFollow> ();
        soundManager = FindObjectOfType<SoundManager> ();

    }

    // Use this for initialization
    void Start() {

        StartCoroutine(Initialize(false));
        scoreText.DOFade(0, 0);

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        
        scoreText.text = score.ToString();

    }

    public IEnumerator Initialize(bool playTutorial) {

        // yield return null;
        scoreText.DOFade(1, 3).SetDelay(1);

        ChangeColor(new Color(192, 175, 52), 0);

        Transform p = Instantiate(player, player.position, Quaternion.identity);
        playerController = p.GetComponent<PlayerController> ();

        if (playTutorial) {
            yield return new WaitForSeconds(10);
        }

        soundManager.music.Play();
        soundManager.music.DOFade(0.3f, 10);
        
        

        Transform s = Instantiate(spawner, spawner.position, Quaternion.identity);
        spawnManager = s.GetComponent<Spawner> ();

        isPlaying = true;

    }

    public void ChangeColor(Color c, float duration) {

        float r = c.r / 255;
        float g = c.g / 255;
        float b = c.b / 255;
        // skybox.SetColor("_SunColor", new Color(r, g, b, 1));
        skybox.DOColor(new Color(r, g, b), "_SunColor", duration);

    }

    public void Reset() {

        score = 0;
        StartCoroutine(Initialize(false));
        Camera.main.DOFieldOfView(60, 0.5f);
        scoreText.rectTransform.DOScale(0.2f, 0.5f);
        scoreText.rectTransform.DOLocalMove(new Vector3(-360, -190, 0), 0.5f).SetEase(Ease.OutSine);
        resetButton.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        resetButton.GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public IEnumerator GameOver() {
        
        yield return null;
        
        Destroy(GameObject.Find("Player(Clone)").gameObject);
        Destroy(GameObject.Find("Spawn Manager(Clone)").gameObject);

        print(">>>> Game Over");
        isPlaying = false;
        ChangeColor(new Color(0, 0, 0), 2);
        Camera.main.DOFieldOfView(140, 4);
        soundManager.GameOverMusic();

        // Find all current atoms and stop them
        ObstacleController[] atoms = FindObjectsOfType<ObstacleController> ();
        foreach(ObstacleController atom in atoms) {

            atom.SetSpeed(0);

        }
        
        scoreText.rectTransform.DOScale(0.5f, 2).SetDelay(2.3f);
        scoreText.rectTransform.DOLocalMove(Vector3.zero, 2).SetDelay(2.3f).SetEase(Ease.OutSine);
        resetButton.GetComponent<CanvasGroup>().DOFade(1, 2).SetDelay(3.5f);
        resetButton.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

}