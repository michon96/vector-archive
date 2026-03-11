using UnityEngine;
using UnityEngine.UI;

public class LevelSelectHandler : MonoBehaviour
{
    [SerializeField] ToggleGroup toggleGroup;
    [SerializeField] int selectedLevel;

    void Start()
    {
        RegisterAllToggles(); 
    }

    void OnLevelSelected(int levelID)
    {
        selectedLevel = levelID;
        Debug.Log("Level selected: " + selectedLevel);
    }

    public void RegisterAllToggles()
    {
        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            int levelIndex = i + 1;
            Toggle currentToggle = toggles[i];

            currentToggle.onValueChanged.AddListener((bool isOn) => {
                if (isOn)
                {
                    OnLevelSelected(levelIndex);
                }
            });
        }
    }
}