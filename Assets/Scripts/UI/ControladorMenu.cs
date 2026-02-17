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
    
    [Header("Nuevas Secciones")]
    public GameObject panelMenuPrincipal; // El objeto que contiene el Fondo, Cometas, etc.
    public GameObject panelSeleccionPersonaje;     // El objeto Panel_SeleccionPersonaje que acabas de crear

    //Herrameintas de desvanecimiento
    public CanvasGroup grupoMenuPrincipalCG;
    public CanvasGroup grupoSeleccionPersonajeCG;
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


    public void IrASeleccionPersonaje()
    {
        StartCoroutine(TransicionEntrePaneles(grupoMenuPrincipalCG, grupoSeleccionPersonajeCG, true));
    }

    public void RegresarAlMenuPrincipalDesdeSeleccionPersonaje()
    {
        StartCoroutine(TransicionEntrePaneles(grupoSeleccionPersonajeCG, grupoMenuPrincipalCG, false));
    }

    // Esta función hace el cambio suave
IEnumerator TransicionEntrePaneles(CanvasGroup saliente, CanvasGroup entrante, bool esHaciaSeleccion)
{
    float t = 0;
    float duracionTransicion = 0.5f;

    // 1. El panel que está visible empieza a desvanecerse
    while (t < duracionTransicion)
    {
        t += Time.deltaTime;
        saliente.alpha = 1 - (t / duracionTransicion);
        yield return null;
    }
    saliente.alpha = 0;
    saliente.gameObject.SetActive(false);

    // 2. Activamos el nuevo panel y lo desvanecemos hacia adentro
    entrante.gameObject.SetActive(true);
    t = 0;
    while (t < duracionTransicion)
    {
        t += Time.deltaTime;
        entrante.alpha = t / duracionTransicion;
        yield return null;
    }
    entrante.alpha = 1;
}




}
