using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ControladorMenu : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject subMenuModos;   // El contenedor de los botones 1v1 y Un Jugador
    public TextMeshProUGUI textoBotonJugar; // El texto que dice "JUGAR"
    public CanvasGroup grupoAlfa; // Arrastra aquí el SubMenu_Modos
    private bool enSubMenu = false; // Para saber si estamos viendo la flecha "<-" o el texto "Jugar"
    

    public void AlDarClickEnJugar()
    {
        StopAllCoroutines(); // Detiene transiciones a medias
        if (!enSubMenu)
        {
            StartCoroutine(TransicionSubMenu(1f, "<", true));
            enSubMenu = true;
        }
        else
        {
            StartCoroutine(TransicionSubMenu(0f, "Jugar", false));
            enSubMenu = false;
        }
    }

    // Esta función hace la magia del efecto suave
    IEnumerator TransicionSubMenu(float objetivoAlfa, string nuevoTexto, bool activar)
    {
        if (activar) subMenuModos.SetActive(true);
        
        textoBotonJugar.text = nuevoTexto;
        float tiempo = 0;
        float duracion = 0.3f; // Duración del efecto en segundos
        float alfaInicial = grupoAlfa.alpha;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            // Cambia la transparencia gradualmente
            grupoAlfa.alpha = Mathf.Lerp(alfaInicial, objetivoAlfa, tiempo / duracion);
            yield return null;
        }

        grupoAlfa.alpha = objetivoAlfa;
        if (!activar) subMenuModos.SetActive(false);
    }

}
