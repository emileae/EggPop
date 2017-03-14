using UnityEngine;
using System.Collections;

public class EggSpawner : MonoBehaviour {

	public float spawnWaitTime = 0.5f;

	private bool droppingEggs = false;
	public int eggsOnScreen = 0;
	public int unPoppedEggs = 0;
	public int numberOfEggsDropped = 10;
	public int minEggsBeforeNextSpawn = 3;

	public GameObject smallEggPrefab;
	public GameObject largeEggPrefab;

	// counting popped eggs per drop
	private int eggsSpawned = 0;

	// Use this for initialization
	void Start () {
		SpawnEggs();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (eggsOnScreen <= minEggsBeforeNextSpawn && !droppingEggs) {
			droppingEggs = true;
			SpawnEggs ();
		}
	}

	void SpawnEggs ()
	{
		StartCoroutine(SpawnAnEgg());
	}

	IEnumerator SpawnAnEgg ()
	{
		yield return new WaitForSeconds (spawnWaitTime);
		Vector3 eggSpawnPosition = new Vector3(transform.position.x + Random.Range(-30, 30), transform.position.y, 0);
		Instantiate (smallEggPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(-90, 90)));

		eggsSpawned += 1;
		eggsOnScreen += 1;

		// keep spawning eggs until the entire batch is done
		if (eggsSpawned < numberOfEggsDropped) {
			SpawnEggs ();
		} else {
			droppingEggs = false;
			eggsSpawned = 0;
		}
	}

	public void DestroyEgg (GameObject egg){
		Destroy(egg);
	}

}
