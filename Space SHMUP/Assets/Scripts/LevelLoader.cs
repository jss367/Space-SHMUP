using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public string LevelName = "Menu";

    protected void Start()
    {
        Application.LoadLevelAsync(LevelName);
    }
}
