using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadBehavior : MonoBehaviour
{
    private const int numBeads = 20;
    public static List<GameObject> beadPool = new(numBeads);
    [SerializeField] private GameObject playerBoard, beadPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numBeads; i++)
        {
            GameObject tempBead = Instantiate(beadPrefab);
            tempBead.SetActive(false);
            tempBead.transform.SetParent(playerBoard.transform);
            beadPool.Add(tempBead);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
