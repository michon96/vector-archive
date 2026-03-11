using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectHandler : MonoBehaviour
{
    [SerializeField] ToggleGroup toggleGroup;
    [SerializeField] int selectedLevel;
    [SerializeField] int[][] gameLevels = new int[][]
    {
        new int[] {2,2},//4
        new int[] {2,3},//6
        new int[] {3,4},//12
        new int[] {4,4},//16
        new int[] {6,6},//20
    };

    void Start()
    {
        RegisterAllToggles(); 
        SetSelectedToggle(selectedLevel);
    }

    void OnLevelSelected(int levelID)
    {
        selectedLevel = levelID;
        
        Debug.Log("Level selected: " + selectedLevel);
    }

    public int[] GetSelectedLevel()
    {

        var toReturn = gameLevels[selectedLevel];
        return toReturn;
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
                    OnLevelSelected(levelIndex-1);
                }
            });
        }
    }
    public void SetSelectedToggle(int p_index)
    {

        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            int levelIndex = i + 1;
            Toggle currentToggle = toggles[i];
            currentToggle.isOn =false;
          
        }
        toggles[0].isOn = true;
    }
}