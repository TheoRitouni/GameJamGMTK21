using UnityEngine;

/// Screen shakes data class
[CreateAssetMenu(fileName = "ShakeData_0", menuName = "JamTool/ShakeData", order = 0)]
public class ShakeData : ScriptableObject
{
    [SerializeField] private float _magnitude = 1, _duration = 1;
    [SerializeField] private AnimationCurve _curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));
    [SerializeField] private bool _isTimeScaled = true;

    /// ScreenShake's force 
    public float magnitude => _magnitude;

    /// ScreenShake's total duration 
    public float duration => _duration;

    /// ScreenShake's influence curve from start (0) to end (1) 
    public AnimationCurve curve => _curve;

    /// Determine if the shake is affected by TimeScale
    public bool isTimeScaled => _isTimeScaled;
}