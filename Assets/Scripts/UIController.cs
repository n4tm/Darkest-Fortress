using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private Transform heartContainer;

    private List<GameObject> _heartsList;
    private int _lastFull = -1;

    private void Awake()
    {
        _heartsList = new List<GameObject>();
    }
    
    public void InsertHeartIntoUI(int amount, bool full)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newHeart = Instantiate(heart, heartContainer);
            _heartsList.Add(newHeart);
        }

        if (full) AddHearts(amount);
    }

    public void ChangeHeartsInUI(int amount, bool add)
    {
        if (add) AddHearts(amount);
        else RemoveHearts(amount);
    }

    private void AddHearts(int amount)
    {
        int lastFullHeartPosition = _lastFull;

        for (int i = lastFullHeartPosition + 1; i < lastFullHeartPosition + amount + 1; i++)
        {
            if (i >= _heartsList.Count) return;

            GameObject objHeart = _heartsList[i];
            objHeart.transform.Find("Full").gameObject.SetActive(true);

            _lastFull++;
        }
    }

    private void RemoveHearts(int amount)
    {
        int lastFullHeartPosition = _lastFull;

        for (int i = lastFullHeartPosition; i > lastFullHeartPosition - amount; i--)
        {
            if (i < 0) return;

            GameObject objHeart = _heartsList[i];
            objHeart.transform.Find("Full").gameObject.SetActive(false);

            _lastFull--;
        }
    }
}
