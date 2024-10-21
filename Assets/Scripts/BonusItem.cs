using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{
    public int bonusPoints = 100; // คะแนนโบนัสที่วัตถุนี้จะให้

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null) // ตรวจสอบว่าผู้เล่นชนกับวัตถุนี้หรือไม่
        {
            GameManager.Instance.AddBonusPoints(bonusPoints); // เพิ่มคะแนนโบนัสให้ผู้เล่น
            Destroy(gameObject); // ทำลายวัตถุโบนัสหลังจากเก็บ
        }
    }
}
