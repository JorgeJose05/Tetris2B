using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject[] places;
    private List<GameObject> piecesPool = new List<GameObject>(); // Pool de piezas

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        foreach (GameObject prefab in piecePrefabs)
        {
            GameObject piece = Instantiate(prefab, transform.position, Quaternion.identity);
            piece.SetActive(false);
            piecesPool.Add(piece);
        }

        // Activar la primera pieza
        ActivateNextPiece();
    */
        InitializePiecePool();
        ActivateNextPiece();

    }

    //Inicializa el pool de piezas desactivadas
    void InitializePiecePool()
    {
        foreach (GameObject prefab in places)
        {
            GameObject piece = Instantiate(prefab);
            piece.SetActive(false);
            piecesPool.Add(piece);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
        public void SpanwNext()
        {
            int i = Random.Range(0, places.Length);
            Instantiate(places[i], places[i].transform.position, Quaternion.identity);
        }
    */
    public void ActivateNextPiece()
    {
        /*
        int randomIndex;
        GameObject piece;

        // Buscar una pieza inactiva en el pool
        do
        {
            randomIndex = Random.Range(0, piecesPool.Count);
            piece = piecesPool[randomIndex];
        } while (piece.activeInHierarchy);

        // Activar la pieza seleccionada
        piece.transform.position = new Vector3(5, 10, 0);
        piece.SetActive(true);
        piece.GetComponent<Piece>().enabled = true;
        */
        GameObject piece = GetInactivePiece();


        if (piece != null)
        {
            piece.transform.position = new Vector3(Board.w / 2 - 0.5f, Board.h - 1, 0);

            // Verifica si la nueva pieza puede caer en la parte superior sin colisionar con otras piezas
            foreach (Transform child in piece.transform)
            {
                Vector2 v = Board.RoundVector2(child.position);
                if (Board.grid[(int)v.x, (int)v.y] != null)
                {
                    // Si está ocupada, significa que la pieza toca una bloqueada y el juego termina
                    Debug.Log("GAME OVER - No se puede generar pieza.");
                    Destroy(piece);  // O puedes parar el juego aquí
                    return;
                }
            }

            piece.SetActive(true);
            piece.GetComponent<Piece>().enabled = true;
        }
        else
        {
            Debug.LogWarning("No hay piezas disponibles en el pool.");
        }
    }

    //Obtiene una pieza inactiva del pool
    GameObject GetInactivePiece()
    {
        /* Esto devolvera simpre la misma primera pieza
        foreach (GameObject piece in piecesPool)
        {
            if (!piece.activeInHierarchy)
            {
                return piece;
            }
        }
        return null;*/
        int indice = ((int)Random.Range(0, places.Length));

        if (!piecesPool[indice].activeInHierarchy)
        {
            return piecesPool[indice];
        }
        return null;
    }

    public void SpanwNext()
    {
        /*
      int i = Random.Range(0, places.Length);
    GameObject piece = Instantiate(places[i]);

    // Centrar la pieza en el medio superior del tablero
    piece.transform.position = new Vector3(Board.w / 2 - 0.5f, Board.h - 1 - 0.5f, 0); // Ajusta para que no se desplace fuera

    // Validar que la pieza no esté fuera de los límites
    foreach (Transform child in piece.transform)
    {
        Vector2 pos = Board.RoundVector2(child.position);
        if (!Board.InsideBorder(pos))
        {
            Debug.LogWarning("Pieza generada fuera del tablero.");
            Destroy(piece);
            return;
        }
    }
    */
        ActivateNextPiece();
    }


}
