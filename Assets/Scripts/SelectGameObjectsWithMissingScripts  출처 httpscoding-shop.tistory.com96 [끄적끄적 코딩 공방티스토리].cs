using UnityEditor;
using UnityEngine;

public class SelectGameObjectsWithMissingScripts : Editor
{
    [MenuItem("Utility/Remove Missing Script")]
    private static void RemoveAllMissingScriptComponents()
    {
        var selectedGameObjects = Selection.gameObjects;
        int totalComponentCount = 0;
        int totalGameObjectCount = 0;

        foreach (var gameObject in selectedGameObjects)
        {
            int removedComponents = RemoveMissingScriptsRecursively(gameObject);
            if (removedComponents > 0)
            {
                totalComponentCount += removedComponents;
                totalGameObjectCount++;
            }
        }

        Debug.Log($"Removed {totalComponentCount} missing script component(s) from {totalGameObjectCount} game object(s).");
    }

    private static int RemoveMissingScriptsRecursively(GameObject gameObject)
    {
        int missingScriptCount = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject);

        if (missingScriptCount > 0)
        {
            Undo.RegisterCompleteObjectUndo(gameObject, "Remove Missing Scripts");
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
        }

        foreach (Transform child in gameObject.transform)
        {
            missingScriptCount += RemoveMissingScriptsRecursively(child.gameObject);
        }

        return missingScriptCount;
    }
}