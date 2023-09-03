using System.Collections;
using System.Collections.Generic;

public class UnionFind
{
    private int[] parent;
    private int[] rank;

    public UnionFind(int size)
    {
        parent = new int[size];
        rank = new int[size];

        // Initialize each element as its own parent and set rank to 0.
        for (int i = 0; i < size; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }
    }

    // Find the representative (root) of the set containing element x.
    public int Find(int x)
    {
        if (parent[x] != x)
        {
            // Path compression: Set the parent of x to the root.
            parent[x] = Find(parent[x]);
        }
        return parent[x];
    }

    // Union two sets by rank.
    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);

        if (rootX != rootY)
        {
            // Union by rank: Attach the shorter tree to the root of the taller tree.
            if (rank[rootX] < rank[rootY])
            {
                parent[rootX] = rootY;
            }
            else if (rank[rootX] > rank[rootY])
            {
                parent[rootY] = rootX;
            }
            else
            {
                // If ranks are equal, choose one as the root and increment its rank.
                parent[rootY] = rootX;
                rank[rootX]++;
            }
        }
    }
}