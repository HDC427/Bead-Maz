using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int boxL, boxW;
    [SerializeField] private GameObject playerBox, goal;
    [SerializeField] private GameObject wallPrefab, beadPrefab;
    private const int numBeadsBuffer = 20;
    public static int numBeadsTotal, numBeadsRemaining;
    public static List<GameObject> beadPool = new(numBeadsBuffer);
    private float beadRadius;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the box with random length and width
        boxL = Random.Range(7, 13);
        boxW = Random.Range(7, 13);
        playerBox.transform.parent = transform;
        playerBox.transform.localScale = new Vector3(boxL, playerBox.transform.localScale.y, boxW);
        // Generate beads
        beadRadius = beadPrefab.GetComponent<SphereCollider>().radius;
        numBeadsTotal = Random.Range(1, numBeadsBuffer + 1);
        numBeadsRemaining = numBeadsTotal;
        GameManager.theGM.UpdateCount(0);
        for (int i = 0; i < numBeadsBuffer; i++)
        {
            GameObject tempBead = Instantiate(beadPrefab, RandomBeadPos(), beadPrefab.transform.rotation);
            if (i >= numBeadsTotal)
            {
                tempBead.SetActive(false);
            }
            tempBead.transform.parent = transform;
            beadPool.Add(tempBead);
        }
        // Generate goal
        goal.transform.parent = transform;
        goal.transform.position = new Vector3((Random.Range(-boxL+1, boxL)) / 2f, 0.01f, (Random.Range(-boxW+1, boxW)) / 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 RandomBeadPos()
    {
        return new Vector3(Random.Range(-boxL / 2 + beadRadius, boxL / 2 - beadRadius), beadRadius, Random.Range(-boxW / 2 + beadRadius, boxW / 2 - beadRadius));
    }
}
