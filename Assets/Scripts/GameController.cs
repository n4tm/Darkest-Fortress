using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private UIController _uiController;

    private void Awake()
    {
        Instance = this;
        _uiController = FindObjectOfType<UIController>();
    }
    
    public void InsertNewHeart(int difference, bool full)
    {
        _uiController.InsertHeartIntoUI(difference, full);
    }

    public void UpdateHeartContainers(int difference, bool add)
    {
        _uiController.ChangeHeartsInUI(difference, add);
    }
}
