using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BottlePuzzleManager : MonoBehaviour
{
    public static BottlePuzzleManager Instance;

    public List<BottleSlot> allSlots; // 所有插槽（需要在Inspector中手动赋值，顺序与slotIndex对应）
    public TMP_Text feedbackText; // 显示提示信息的UI文本
    public float feedbackDuration = 3f; // 提示信息显示时间

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    public void CheckCompletion()
    {
        bool allFilled = true; // 是否所有插槽都有瓶子
        bool allCorrect = true; // 是否所有瓶子顺序正确

        foreach (var slot in allSlots)
        {
            // 检查是否有未填充的插槽
            if (slot.OccupyingBottle == null)
            {
                allFilled = false;
                allCorrect = false;
                continue;
            }

            // 关键修改：正确条件为瓶子ID等于插槽Index（均从0开始）
            if (slot.OccupyingBottle.bottleID != slot.slotIndex)
            {
                allCorrect = false;
            }
        }

        // 所有插槽填满后判断结果
        if (allFilled)
        {
            if (allCorrect)
            {
                ShowFeedback("恭喜！排序正确！");
                Instantiate(Resources.Load("successVfx"), transform.position + new Vector3(0,0.2f,0), Quaternion.identity);
            }
            else
            {
                ShowFeedback("顺序不正确，请重新调整");
            }
        }
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            CancelInvoke(nameof(HideFeedback));
            Invoke(nameof(HideFeedback), feedbackDuration);
        }
    }

    private void HideFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }
}