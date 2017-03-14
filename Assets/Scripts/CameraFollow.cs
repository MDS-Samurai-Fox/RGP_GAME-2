using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	public Transform player;
	
	public Ease easeType;
	
	public float followDelayTime = 0.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.DOMove(new Vector3(player.position.x, player.position.y + 0.1f, this.transform.position.z), followDelayTime).SetEase(easeType);
		
	}

}