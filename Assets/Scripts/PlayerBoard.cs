using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoard : MonoBehaviour
{
    private int boxL, boxW;
    [SerializeField] private GameObject origin;
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
        transform.localScale = new Vector3(transform.localScale.x * boxL, transform.localScale.y, transform.localScale.z * boxW);
        // Generate beads
        beadRadius = beadPrefab.GetComponent<SphereCollider>().radius;
        for (int i = 0; i < numBeads; i++)
        {
            GameObject tempBead = Instantiate(beadPrefab, RandomBeadPos(), beadPrefab.transform.rotation);
            //tempBead.SetActive(false);
            //tempBead.transform.localScale = new Vector3(tempBead.transform.localScale.x / boxL, tempBead.transform.localScale.y, tempBead.transform.localScale.z / boxW);
            tempBead.transform.parent = origin.transform;
            beadPool.Add(tempBead);
        }
    }

    // Update is called once per frame
    void Update()
    {
        tiltBox();
    }

    Vector3 RandomBeadPos()
    {
        return new Vector3(Random.Range(-boxL / 2 + beadRadius, boxL / 2 + beadRadius), beadRadius * 2, Random.Range(-boxW / 2 + beadRadius, boxW / 2 + beadRadius));
    }

    void tiltBox()
    {
        // Tilt the box by clicking and dragging mouse
        if (Input.GetMouseButton(0))
        {
            float horizontalMouseMove = Input.GetAxis("Mouse X");
            float verticalMouseMove = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.right, horizontalMouseMove);
            transform.Rotate(Vector3.forward, verticalMouseMove);
            origin.transform.Rotate(Vector3.right, horizontalMouseMove);
            origin.transform.Rotate(Vector3.forward, verticalMouseMove);
           
        }
    }
}
