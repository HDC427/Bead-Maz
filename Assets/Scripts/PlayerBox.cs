using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBox : MonoBehaviour
{
    [SerializeField] private GameObject origin;
    private const int numBeads = 20;
    public static List<GameObject> beadPool = new(numBeads);
    private float beadRadius;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tiltBox();
    }

    void tiltBox()
    {
        // Tilt the box by clicking and dragging mouse
        if (Input.GetMouseButton(0))
        {
            float horizontalMouseMove = Input.GetAxis("Mouse X");
            float verticalMouseMove = Input.GetAxis("Mouse Y");
            // Make sure the origin's position is (0, 0, 0)
            origin.transform.Rotate(Vector3.right, horizontalMouseMove);
            origin.transform.Rotate(Vector3.forward, verticalMouseMove);
           
        }
    }
}
