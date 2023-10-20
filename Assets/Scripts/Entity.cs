using UnityEngine;

/// <summary>
/// base class for all interactive playable objects ont the scene
/// </summary>
public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// object's name for user
    /// </summary>
    [SerializeField] private string m_Nickname;
    public string Nickname => m_Nickname;
}
