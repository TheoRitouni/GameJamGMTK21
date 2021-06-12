using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitstopData_0", menuName = "JamTool/HitstopData", order = 0)]
public class HitstopData : ScriptableObject
{
    [SerializeField] protected float _duration;
    [SerializeField] protected float _amount;

    public float duration => _duration;
    public float amount => _amount;

}
