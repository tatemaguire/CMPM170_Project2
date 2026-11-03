using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Resets the game by reloading the current scene, which puts every tile,
// player, and score back to its starting state.
//
// WORKS WITH NO EDITOR SETUP: if the scene has no ResetGame object, one is created
// automatically at startup, showing a simple "Reset Game" button in the top-left corner
// and listening for the R key.
//
// UNITY SETUP FOR A PROPER UI BUTTON (optional, do later in the editor):
//   1. Add a UI > Button to the Canvas and label it "Reset".
//   2. Add this component to any GameObject in the scene (e.g. an empty "Game Manager").
//   3. In the Button's OnClick() list, drag in that GameObject and pick ResetGame > ResetTheGame.
//   4. Untick "Show Debug Button" so the placeholder button goes away.
public class ResetGame : MonoBehaviour
{
    [Tooltip("Draws a placeholder button with OnGUI. Turn off once a real UI button is hooked up.")]
    public bool showDebugButton = true;

    [Tooltip("Keyboard shortcut for resetting. R is not used by any player controls.")]
    public Key resetKey = Key.R;

    // Creates a ResetGame object automatically if nobody added one to the scene.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void CreateIfMissing()
    {
        if (FindAnyObjectByType<ResetGame>() == null)
        {
            GameObject auto = new GameObject("Reset Game (auto)");
            auto.AddComponent<ResetGame>();
            //this only runs once per play session, so keep the object alive across scene reloads
            DontDestroyOnLoad(auto);
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current[resetKey].wasPressedThisFrame)
        {
            ResetTheGame();
        }
    }

    void OnGUI()
    {
        if (!showDebugButton) return;

        if (GUI.Button(new Rect(10, 10, 120, 40), "Reset Game"))
        {
            ResetTheGame();
        }
    }

    // Public so a UI Button's OnClick() can call it.
    public void ResetTheGame()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
