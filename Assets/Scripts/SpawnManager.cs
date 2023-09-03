using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        goal.transform.position = new Vector3(-boxL / 2f + 0.5f + Random.Range(1, boxL), 0.01f, -boxW / 2f + 0.5f + Random.Range(1, boxW));
        // Generate walls
        GenerateWalls(wallPrefab, boxL, boxW, transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateWalls(GameObject wall, int L, int W, Transform parent)
    {
        float unit = wall.GetComponent<BoxCollider>().size.x;
        float height = wall.GetComponent<BoxCollider>().size.y / 4;
        // Generate full walls
        List<GameObject> walls = new(L * (W - 1) + (L - 1) * W);
        for (int i = 1; i <= L; i++)
        {
            for(int j = 1; j < W; j++)
            {
                walls.Add(Instantiate(wall, new Vector3(-(L + unit) / 2f + i * unit, height, -W / 2f + j * unit), new Quaternion(0f, 0f, 0f, 1f), parent));
            }
        }
        for (int i = 1; i < L; i++)
        {
            for (int j = 1; j <= W; j++)
            {
                walls.Add(Instantiate(wall, new Vector3(-L / 2f + i * unit, height, -(W + unit) / 2f + j * unit), new Quaternion(0f, 0.5f, 0f, 0.5f), parent));
            }
        }
        // Choose a random wall, join the neighboring cells if not yet joined. Repeat until all cells are joined.
        UnionFind cells = new(L * W);
        int cell1 = -1, cell2 = -1, joined = 1;
        while(joined < L * W)
        {
            int wallIndex = Random.Range(0, L * (W - 1) + (L - 1) * W - 1);
            if (!walls[wallIndex].IsDestroyed())
            {
                if (wallIndex < L * (W - 1))
                {
                    cell1 = wallIndex + wallIndex / (W - 1);
                    cell2 = cell1 + 1;
                }
                else
                {
                    cell1 = wallIndex - L * (W - 1);
                    cell2 = cell1 + W;
                }
                if (cells.Find(cell1) != cells.Find(cell2))
                {
                    Destroy(walls[wallIndex]);
                    cells.Union(cell1, cell2);
                    joined++;
                }
            }
        }
    }

    Vector3 RandomBeadPos()
    {
        return new Vector3(Random.Range(-boxL / 2 + beadRadius, boxL / 2 - beadRadius), beadRadius, Random.Range(-boxW / 2 + beadRadius, boxW / 2 - beadRadius));
    }
}
