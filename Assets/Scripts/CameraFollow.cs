using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameManager gm;

    private Transform player;

    Vector3 position;

    public Ease easeType;

    public float followDelayTime = 0.3f;

    // Update is called once per frame
    void Update() {

        if (!gm.isPlaying || (player == null))
            return;

        this.transform.DOMove(new Vector3(player.position.x, player.position.y, this.transform.position.z), followDelayTime).SetEase(easeType);

    }

    public void SetPlayer(Transform _player) {

        player = _player;

    }

    public void Shake() {

        Quaternion rot = this.transform.localRotation;
        this.transform.DOShakeRotation(0.15f, 55);
        this.transform.DOLocalRotateQuaternion(Quaternion.identity, 0).SetDelay(0.15f);

    }

}