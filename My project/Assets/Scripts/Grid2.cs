using UnityEngine;

public class Nodo2
{
    public Vector2 posicion;
    public Nodo2 arriba;
    public Nodo2 abajo;
    public Nodo2 izquierda;
    public Nodo2 derecha;

    public Nodo2(Vector2 pos)
    {
        posicion = pos;
    }
}
