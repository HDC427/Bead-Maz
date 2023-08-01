using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int boxL, boxW;
    [SerializeField] private GameObject playerBox;
    [SerializeField] private GameObject wallPrefab, beadPrefab;
    private const int numBeads = 20;
    public static List<GameObject> beadPool = new(numBeads);
    private float beadRadius;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the box with random length and width
        boxL = Random.Range(7, 13);
        boxW = Random.Range(7, 13);
        Vector3 scale = playerBox.transform.localScale;
        playerBox.transform.localScale = new Vector3(scale.x * boxL, scale.y, scale.z * boxW);
        // Generate beads
        beadRadius = beadPrefab.GetComponent<SphereCollider>().radius;
        for (int i = 0; i < numBeads; i++)
        {
            GameObject tempBead = Instantiate(beadPrefab, RandomBeadPos(), beadPrefab.transform.rotation);
            //tempBead.SetActive(false);
            tempBead.transform.parent = transform;
            beadPool.Add(tempBead);
        }
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
