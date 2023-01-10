using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommandConfig : ScriptableObject
{
    [SerializeField]
    public List<string> ChatContent;
}