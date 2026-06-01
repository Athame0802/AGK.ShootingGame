using System;
using UnityEngine;

public static class Layers
{
    public static readonly int UI = LayerMask.NameToLayer("UI");

    public static readonly int BackGround = LayerMask.NameToLayer("BackGround");

    public static readonly int Player = LayerMask.NameToLayer("Player");
    public static readonly int Enemy = LayerMask.NameToLayer("Enemy");

    public static readonly int PlayerBullet = LayerMask.NameToLayer("PlayerBullet");
    public static readonly int EnemyBullet = LayerMask.NameToLayer("EnemyBullet");

    public static readonly int Spawner = LayerMask.NameToLayer("Spawner");
    public static readonly int Item = LayerMask.NameToLayer("Item");
}