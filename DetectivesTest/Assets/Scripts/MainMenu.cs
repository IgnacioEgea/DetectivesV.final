using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance = null;

    void Awake() //Llamar a este método inmediatamente para que ponga la instancia en nulo.
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    void Update() // Volver al menú principal llamando al método "ToMainMenu()" al pulsar la tecla ESC.
    {
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    ToMainMenu();
        //}
    }
    public void ToMainMenu() // Carga de escena para volver al menú principal.
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void LoadScene() // Método que carga escenas.
    {
        SceneManager.LoadSceneAsync(2);
      
    }
    public void StartGame() // Método para empezar el juego.
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void GoBack()
    {
        SceneManager.LoadSceneAsync(0);

    }

    public void FinishGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Quit() // Método para salir del juego.
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            NextLevel();
        }
    }
}


