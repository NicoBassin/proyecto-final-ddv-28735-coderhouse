using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CreatePlayerData")]

public class PlayerData : ScriptableObject
{
    [Header("MOVEMENT CONFIG")]
    [Tooltip("Movement Speed")]
    [SerializeField] private float playerSpeed = 15f;
    public float PlayerSpeed{get {return playerSpeed;} set {playerSpeed = value;}}

    [Tooltip("Rotation Speed")]
    [SerializeField] private float rotationSpeed = 3f;
    public float RotationSpeed{get {return rotationSpeed;} set {rotationSpeed = value;}}

    [Tooltip("Jump Height")]
    [SerializeField] private float jumpHeight = 3f;
    public float JumpHeight{get {return jumpHeight;} set {jumpHeight = value;}}

    [Header("SHOOTING CONFIG")]
    [Tooltip("Shooting Cooldown")]
    [SerializeField] private float shootCooldownTime = 2f;
    public float ShootCooldownTime{get {return shootCooldownTime;} set {shootCooldownTime= value;}}
}
