using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public Nodo2 primerNodo;
    public int columnas;
    public int filas;
    public GameObject spritePrefab;

    public Dictionary<Vector2, Nodo2> nodos = new Dictionary<Vector2, Nodo2>();

    private void Start()
    {
        CrearGrid();
        Renderizar();
    }

    private void CrearGrid()
    {
        float desplazamientoX = (columnas - 1) / 2.0f;
        float desplazamientoY = (filas - 1) / 2.0f;

        primerNodo = new Nodo2(new Vector2(-desplazamientoX, -desplazamientoY));
        nodos.Add(primerNodo.posicion, primerNodo);

        Nodo2 nodoFila = primerNodo;

        for (int i = 0; i < filas; i++)
        {
            Nodo2 nodoActual = nodoFila;

            for (int j = 1; j < columnas; j++)
            {
                Nodo2 nuevoNodo = new Nodo2(new Vector2(nodoActual.posicion.x + 1, nodoActual.posicion.y));
                nodoActual.derecha = nuevoNodo;
                nuevoNodo.izquierda = nodoActual;
                nodoActual = nuevoNodo;
                nodos.Add(nuevoNodo.posicion, nuevoNodo);
            }

            if (i < filas - 1)
            {
                Nodo2 nuevoNodoAbajo = new Nodo2(new Vector2(primerNodo.posicion.x, nodoFila.posicion.y + 1));
                nodoFila.abajo = nuevoNodoAbajo;
                nuevoNodoAbajo.arriba = nodoFila;
                nodoFila = nuevoNodoAbajo;
                nodos.Add(nuevoNodoAbajo.posicion, nuevoNodoAbajo);
            }
        }
    }

    private void Renderizar()
    {
        foreach (var nodo in nodos.Values)
        {
            Instantiate(spritePrefab, nodo.posicion, Quaternion.identity);
        }
    }

    public Nodo2 GetNodoEn(Vector2 posicion)
    {
        if (nodos.TryGetValue(posicion, out Nodo2 nodo))
        {
            return nodo;
        }
        return null;
    }
}