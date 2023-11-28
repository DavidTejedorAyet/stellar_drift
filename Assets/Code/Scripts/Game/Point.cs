using MelenitasDev.SoundsGood;
using UnityEngine;

public class Point : MonoBehaviour {
    public GameObject pointTakenEffect;
    void Start() {

    }

    void Update() {

    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            AudioManager.Instance.PlaySoundEffect(SFX.point);
            LevelManager.Instance.AddPoint();
            Instantiate(pointTakenEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
