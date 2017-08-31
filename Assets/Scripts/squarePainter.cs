using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squarePainter : MonoBehaviour {

    public Sprite availablePath;
    public Sprite TargetSquare;
    public Sprite simpleSquare;
    public Sprite extraMove;



	public void PaintSquares(List<Square_cell_Operator> squaresToPaint, int textureID)
    {
        switch(textureID)
        {
            case 1:
                foreach(Square_cell_Operator sqr in squaresToPaint)
                {
                    sqr.SetSprite(availablePath);
                }
                break;
            case 2:
                foreach (Square_cell_Operator sqr in squaresToPaint)
                {
                    sqr.SetSprite(TargetSquare);
                }
                break;
            case 3:
                foreach (Square_cell_Operator sqr in squaresToPaint)
                {
                    sqr.SetSprite(extraMove);
                }
                break;
            case 4:
                foreach (Square_cell_Operator sqr in squaresToPaint)
                {
                    sqr.SetSprite(simpleSquare);
                }
                break;


        }
    }
}
