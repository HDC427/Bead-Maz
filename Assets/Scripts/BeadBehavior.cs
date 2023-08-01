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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

    }
}
