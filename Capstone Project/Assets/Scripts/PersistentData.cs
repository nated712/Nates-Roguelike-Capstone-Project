using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance;
    public float score;
    public float highScore = 0f;
    public string timer;

    /// <summary>
    /// Ensures that there is only one instance of PersistentData across all scenes.
    /// If there is already an instance, this script will destroy itself.
    /// Otherwise, it sets itself as the singleton instance and marks itself to not be destroyed on scene load.
    /// </summary>
    private void Awake(){

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
