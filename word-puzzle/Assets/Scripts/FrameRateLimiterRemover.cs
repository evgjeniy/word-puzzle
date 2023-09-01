using UnityEngine;

public class FrameRateLimiterRemover : MonoBehaviour
{
    [SerializeField] private int newFpsLimit = 120;

    private void Start()
    {
        Application.targetFrameRate = newFpsLimit;
        QualitySettings.vSyncCount = 0;
        
        Destroy(this);
    }
}