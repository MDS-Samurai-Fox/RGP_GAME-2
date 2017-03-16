using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private GameManager gm;

    public Transform antiMatter;
    public Transform diamond;
    public Transform hydrogen;
    public Transform nitrogen;
    public Transform plutonium;

    private List<Transform> atoms = new List<Transform> ();
    private Vector3 spawnPosition;

    private float gameTime = 0;
    private float spawnTimer = 0;
    private float timeToSpawn = 1f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        gm = FindObjectOfType<GameManager> ();
    }

    // Use this for initialization
    void Start() {

        atoms.Add(antiMatter);
        atoms.Add(diamond);
        atoms.Add(hydrogen);
        atoms.Add(nitrogen);
        atoms.Add(plutonium);

        Random.InitState((int) System.DateTime.Now.Ticks);
        
        spawnPosition = GameObject.Find("Center").transform.position;

        print(">> Spawner " + spawnPosition);

    }

    // Update is called once per frame
    void Update() {

        if (!gm.isPlaying) {
            print("-- STOPPING ALL COROUTINES");
            StopAllCoroutines();
            return;
        }

        gameTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (spawnTimer > timeToSpawn) {
            StartCoroutine(SpawnObjectAt(atoms[Random.Range(0, atoms.Count)], 1));
            spawnTimer = 0;
            timeToSpawn = Random.Range(0.7f, 2.4f);
            print("-- Spawning next item in " + timeToSpawn + " seconds");
        }

    }

    IEnumerator SpawnObjectAt(Transform _transform, float _delay) {

        yield return new WaitForSeconds(_delay);

        Vector3 position = new Vector3(spawnPosition.x, 0.15f, spawnPosition.z - Mathf.Clamp(4 - gameTime, -4, 12));

        _transform = Instantiate(_transform, position, Quaternion.identity);
        
        _transform.RotateAround(spawnPosition, Vector3.back, Random.Range(0, 360));

        _transform.gameObject.AddComponent<ObstacleController> ();
        
        _transform.gameObject.GetComponent<ObstacleController>().SetSpeed(2 + (gameTime * 0.075f));
        
        _transform.SetParent(this.transform);

    }

}