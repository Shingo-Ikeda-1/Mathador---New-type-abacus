using System.Collections.Generic;
using UnityEngine;

public class ChildSpawnable : MonoBehaviour, IChildSpawnable
{
    [SerializeField] private GameObject _childPrefab;
    public GameObject ChildPrefab
    {
        get => _childPrefab;
    }

    private int _childCount = 0;
    public int ChildCount
    {
        get { return _childCount; }
        set { SetChildCount(value); }
    }

    public virtual void OnEnable()
    {
        ChildCount = 6;
    }
    public virtual List<GameObject> AddChild(int numToAdd = 1)
    {
        // Prevents overflowable digit from being generated
        if (ChildCount >= 9) return null;

        // Adds a ChildPrefab
        GameObject newSlider = Instantiate(ChildPrefab, transform, false);
        _childCount++;
        newSlider.name = $"{ChildPrefab.name}_{ChildCount - 1}";
        List<GameObject> sliders = new() { newSlider };
        AlignChildren();

        numToAdd--;
        if (numToAdd > 0) { sliders.AddRange(AddChild(numToAdd)); }
        return sliders;
    }

    public virtual void RemoveChild(int numToRemove = 1)
    {
        if (ChildCount == 0)
        {
            //No more ChildPrefab left to be removed.
            return;
        }
        Transform child = transform.GetChild(ChildCount - 1);
        Destroy(child.gameObject); // will be executed after the current Update loop
        _childCount--;
        AlignChildren();

        numToRemove--;
        if (numToRemove > 0) { RemoveChild(numToRemove); }
    }

    public List<GameObject> SetChildCount(int targetNum)
    {
        int gap = targetNum - ChildCount;
        if (gap > 0) { return AddChild(gap); }
        if (gap < 0) { RemoveChild(-gap); }
        return null;
    }

    public virtual void AlignChildren()
    {
        float width = (transform as RectTransform).rect.width;
        float gap = width / (_childCount + 1);
        int nth = 1;
        float distanceToRightEdge = width / 2;
        Vector2 nextChildPosition;
        foreach (Transform child in transform)
        {
            nextChildPosition = new Vector2((distanceToRightEdge - (gap * nth)) * (transform as RectTransform).lossyScale.x, 0f);
            child.localPosition = nextChildPosition;
            nth++;
        }
    }
}
