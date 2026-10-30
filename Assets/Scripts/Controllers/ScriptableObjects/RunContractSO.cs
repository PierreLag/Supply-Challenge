using UnityEngine;

[CreateAssetMenu(fileName = "RunContractSO", menuName = "Scriptable Objects/RunContractSO")]
public class RunContractSO : ScriptableObject
{
    public StatEnum statChange;
    public float changeValue;
    public string specialTag;
    public string description;
}
