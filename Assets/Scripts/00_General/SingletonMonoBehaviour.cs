using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T Instance;

    public static T instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = (T)FindObjectOfType(typeof(T));

                if (Instance == null)
                {
                    Debug.LogError($"[{typeof(T)}] does not exist in the scene.");
                }
            }

            return Instance;
        }
    }
}
