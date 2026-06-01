using UnityEngine;

public interface IEnemy
{
    public bool IsSpawned { get; set; }
    public bool IsBoss { get; }
    public bool IsEnabled { set; }
}