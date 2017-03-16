using UnityEngine;

public class SoundManager : MonoBehaviour {
	
	public AudioSource music;
	public AudioSource effects;
	
	[SpaceAttribute]
	public AudioClip moveRight, moveLeft, shield, hydrogen, nitrogen, plutonium, death;
	
	public void Play(AudioClip _clip) {
		
		effects.PlayOneShot(_clip);
		
	}
	
	public void GameOverMusic() {
		
		music.Stop();
		effects.PlayOneShot(death);
		
	}
	
}