using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private static Spawner spawner;
    public static int w = 10;
    public static int h = 20;
    public static GameObject[,] grid = new GameObject[w, h];

    // Prefab para el bloque (sería algo como una celda vacía o fondo)
    public GameObject blockPrefab;
    void Start(){
        spawner = Object.FindFirstObjectByType<Spawner>();
        InitializeGrid(blockPrefab);
    }

 // Inicializa la cuadrícula con bloques ocultos
    public static void InitializeGrid(GameObject blockPrefab)
    {
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                GameObject block = Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.identity);
                block.SetActive(false); // Se ocultan los bloques inicialmente
                grid[x, y] = block;
            }
        }
    }

     // Activa un bloque en la cuadrícula en lugar de crearlo
    public static void ActivateBlock(int x, int y)
    {
        if (grid[x, y] != null)
        {
            grid[x, y].SetActive(true);
        }
    }

    // Rounds Vector2 so does not have decimal values
    // Used to force Integer coordinates (without decimals) when moving pieces
    public static Vector2 RoundVector2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x * 2) / 2, Mathf.Round(v.y * 2) / 2);
    }

    // TODO: Returns true if pos (x,y) is inside the grid, false otherwise
    public static bool InsideBorder(Vector2 pos)
    {
        return pos.x >= 0.5f && pos.x <= w - 0.5f && pos.y >= 0.5f && pos.y <= h - 0.5f;
    }

    // TODO: Deletes all GameObjects in the row Y and set the row cells to null.
    // You can use Destroy function to delete the GameObjects.
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
             if (grid[x, y] != null)
        {
            //Destroy(grid[x, y]);
            grid[x, y].SetActive(false);
            grid[x, y] = null;
        }
        }
    }

    // TODO: Moves all gameobject on row Y to row Y-1
    // 2 thing change:
    //  - All GameObjects on row Y go from cell (X,Y) to cell (X,Y-1)
    //  - Changes the GameObject transform position Gameobject.transform.position += new Vector3(0, -1, 0).
    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y].transform.position += new Vector3(0, -1, 0);
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;//limpiar la posicion original

            }
        }


    }

    // TODO: Decreases all rows above Y
    public static void DecreaseRowsAbove(int y)
    {
        /*for (int x = y; x < h; x++){
            Board.DecreaseRow(x);
        }*/
        for (int i = y; i < h; i++)
        {
            DecreaseRow(i);
        }
    }

    // TODO: Return true if all cells in a row have a GameObject (are not null), false otherwise
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] == null || !grid[x, y].activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    // Deletes full rows
    public static void DeleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

}
