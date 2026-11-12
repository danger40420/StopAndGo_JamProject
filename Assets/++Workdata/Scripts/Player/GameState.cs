using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    public static bool IsDraggingBox = false;

    public static HashSet<BoxType> UnlockedBoxes = new HashSet<BoxType>();

    public static event Action<BoxType> OnBoxUnlocked;
    
    public enum BoxType
    {
        Red,
        Green,
        Blue
    }

    public static void UnlockBox(BoxType type)
    {
        UnlockedBoxes.Add(type);
        OnBoxUnlocked?.Invoke(type);
    }

    // Reset Keys before loading GameScene
    public static void ResetState()
    {
        IsDraggingBox = false;
        UnlockedBoxes.Clear();
        OnBoxUnlocked = null;
    }
    
}
