using UnityEngine;
using System.Collections;

public class SmallEgg : MonoBehaviour {

	public EggSpawner eggSpawner;

	public bool popEgg = false;

	private bool eggPopped = false;

	public SpriteRenderer shell;
	public SpriteRenderer prize;
	public float prizeLifespan = 1.0f;

	public ParticleSystem confetti1;

	private bool confettiburst = false;

	// audio
	AudioSource audio;

	// Use this for initialization
	void Start ()
	{
		if (eggSpawner == null) {
			eggSpawner = GameObject.Find("SpawnPoint").GetComponent<EggSpawner>();
		}
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (popEgg && !eggPopped) {
			PopEgg();
		}
	}

	public void PopEgg ()
	{
		eggPopped = true;
		eggSpawner.eggsOnScreen -= 1;

		// audio
		audio.Play();

		// sprites
		shell.enabled = false;
		prize.enabled = true;

		// particles
		if (!confettiburst) {
			confettiburst = true;

			// play all particle systems
			confetti1.Play ();
		}

		// begin destruction countdown
		StartCoroutine(DestroyEgg());

	}

	IEnumerator DestroyEgg (){
		yield return new WaitForSeconds(prizeLifespan);
		eggSpawner.DestroyEgg(gameObject);
	}

}
