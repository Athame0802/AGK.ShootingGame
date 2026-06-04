using UnityEngine;

public interface IEnemy
{
    public bool IsBoss { get; }
    public bool IsSpawned { get; set; }

    public void OnTouchSpawnLine();
}