using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Spawner spawner;
    private float fall = 0;
    public float fallSpeed = 1;

    private float lastFall = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindFirstObjectByType<Spawner>();
        // Default position not valid? Then it's game over
        if (!IsValidBoard())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
            return;
        }
        UpdateBoard();
    }

    // Update is called once per frame.
    // Implements all piece movements: right, left, rotate and down.
    void Update()
    {
            //Lo de caer 
            if (Time.time - fall >= fallSpeed || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MovePiece(new Vector3(0, -1, 0));
                lastFall = Time.time;
                // Mover hacia abajo
                

                if (IsValidBoard())
                {
                    UpdateBoard();
                }
                else
                {
                    // Revertir posición y bloquear pieza en el tablero
                    transform.position += new Vector3(0, 1, 0);
                    UpdateBoard();
                    Board.DeleteFullRows();
                    //spawner.SpanwNext();
                    RecyclePiece();//enabled = false;
                }
                fall = Time.time;
            }//fin
           


        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if it's valid
            if (IsValidBoard())
                // It's valid. Update grid.
                UpdateBoard();
            else
                // It's not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }
        // Implement Move Right (key RightArrow)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if it's valid
            if (IsValidBoard())
                // It's valid. Update grid.
                UpdateBoard();
            else
                // Its not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }
        // Implement Rotate, rotates the piece 90 degrees (Key UpArrow)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
            if (IsValidBoard())
                UpdateBoard();
            else
                transform.Rotate(0, 0, 90);
        }

        // Implement move Down wards and Fall (each second)
        
              
      
    }

    // TODO: Updates the board with the current position of the piece. 
    void UpdateBoard()
    {
        for (int y = 0; y < Board.h; y++) {
            for (int x = 0; x < Board.w; x++) {
                if (Board.grid[x,y] != null && Board.grid[x,y].transform.parent == transform) {
                    
                    Board.grid[x,y] = null;
                    
        }
    }
    }
        foreach (Transform child in transform) {
            Vector2 v = Board.RoundVector2(child.position);
            //Board.grid[(int)v.x, (int)v.y] = child.gameObject;
            Board.grid[(int)v.x, (int)v.y] = child.gameObject;
            Board.ActivateBlock((int)v.x,(int)v.y);
    }
    }
    // Returns if the current position of the piece makes the board valid or not
    bool IsValidBoard()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Board.RoundVector2(child.position);

            if (!Board.InsideBorder(v)|| (Board.grid[(int)v.x, (int)v.y] != null &&
                Board.grid[(int)v.x, (int)v.y].transform.parent != transform))
            return false;

            

            // No hay límite superior para la coordenada Y, pero debe estar dentro de las celdas del tablero
            // Así que solo validamos si está dentro de los límites horizontales y no se superpone con otro bloque
            if (Board.grid[(int)v.x, (int)v.y] != null &&
                Board.grid[(int)v.x, (int)v.y].transform.parent != transform)
                return false;
        }
        return true;
    }


void MovePiece(Vector3 direction){
    transform.position += direction;
    if (IsValidBoard()) {

        UpdateBoard();
    }

 else {
    transform.position -= direction;
    if (direction == new Vector3(0, -1, 0)) {
        Board.DeleteFullRows();

        //Object.FindFirstObjectByType<Spawner>().SpanwNext();
        //enabled = false;
        RecyclePiece();
    }
}
}
     // En lugar de destruir la pieza, la reciclamos
void RecyclePiece()
    {
        // Ocultar y deshabilitar la pieza
        //transform.position = new Vector3(-100, -100, 0);
        gameObject.SetActive(false);

        // Activar la siguiente pieza
        spawner.ActivateNextPiece();
    }


}
