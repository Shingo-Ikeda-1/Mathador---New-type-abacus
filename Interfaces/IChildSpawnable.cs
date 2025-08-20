using System.Collections.Generic;
using UnityEngine;

public interface IChildSpawnable
{
    GameObject ChildPrefab { get; }

    int ChildCount { get; set; }


    List<GameObject> AddChild(int numToAdd = 1);

    void RemoveChild(int numToRemove = 1);

    List<GameObject> SetChildCount(int targetNum);

    void AlignChildren();
}