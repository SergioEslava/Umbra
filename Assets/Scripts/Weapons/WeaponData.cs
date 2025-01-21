using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Basic Info")]
    public string weaponName;
    public Sprite weaponIcon;

    [Header("Stats")]
    public float damage;
    public float fireRate; // Attacks per second
    public float range; // For ranged weapons, could be in units
    public float impactRadius; // For less frustation while shooting

    [Header("Additional Settings")]
    public GameObject weaponPrefab; // Model or object to spawn
    public AudioClip attackSound; // Sound when the weapon is used

    [TextArea]
    public string description; // Optional description of the weapon
}
