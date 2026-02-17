using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorSimple : MonoBehaviour
{
   public float velocidad = 15f; // Puedes cambiar esto en el Inspector

    void Update()
    {
        // Rota sobre el eje Z (hacia adelante/atrás en 2D)
        transform.Rotate(0, 0, velocidad * Time.deltaTime);
    }



}
