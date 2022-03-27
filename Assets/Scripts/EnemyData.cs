using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "CreateEnemyData")]

public class EnemyData : ScriptableObject
{
    [Header("MOVEMENT CONFIG")]
    [Tooltip("Speed")]
    [SerializeField] private float speed = 2f;
    public float Speed{get {return speed;} set {speed = value;}}

    [Tooltip("Max Distance")]
    [SerializeField] private float maxDistance = 2f;
    public float MaxDistance{get {return maxDistance;} set {maxDistance = value;}}

    [Tooltip("Rotation Speed")]
    [SerializeField] private float rotationSpeed = 1f;
    public float RotationSpeed{get {return rotationSpeed;} set {rotationSpeed = value;}}

}
