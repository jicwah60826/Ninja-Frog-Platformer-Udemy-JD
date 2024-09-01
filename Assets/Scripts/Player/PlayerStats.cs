using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int level;

    [Header("Move")]
    public float moveSpeed;
    public float runSpeed;

    [Header("Move")]
    public float jumpForce;

    public int additionalJumps;

    public float healthReChargeRate;

    [Header("Knockback")]
    public float knockbackLength;
    public float knockbackForce;

    public float invincLength = 1f;
}
