using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu()]
public class TargetTypeCollection : ScriptableObject {

    [SerializeField]
    public List<WhackTarget> targetTypes;

}
