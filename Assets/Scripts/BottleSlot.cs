using UnityEngine;

public class BottleSlot : MonoBehaviour
{
    public int slotIndex;

    public Bottle OccupyingBottle { get; set; } // 当前占据插槽的瓶子

    // 确保碰撞器是触发器（编辑模式下自动检查）
    //private void OnValidate()
    //{
    //    Collider collider = GetComponent<Collider>();
    //    if (collider != null && !collider.isTrigger)
    //    {
    //        collider.isTrigger = true; // 强制设为触发器，用于检测瓶子
    //    }
    //}
}