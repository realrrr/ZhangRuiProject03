using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlatePuzzleManager : MonoBehaviour
{
    public static PlatePuzzleManager Instance;

    public List<PlateSlot> allSlots; // 所有插槽（需要在Inspector中手动赋值，顺序与slotIndex对应）
    public TMP_Text feedbackText; // 显示提示信息的UI文本
    public float feedbackDuration = 3f; // 提示信息显示时间
    public GameObject tick;

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
        bool allFilled = true;
        bool allCorrect = true;

        foreach (var slot in allSlots)
        {
            if (slot.OccupyingBottle == null)
            {
                allFilled = false;
                allCorrect = false;
                continue;
            }

            if (slot.OccupyingBottle.bottleID != slot.slotIndex)
            {
                allCorrect = false;
            }
        }

        if (allFilled)
        {
            if (allCorrect)
            {
                tick.SetActive(true);
                ShowFeedback("correct!");
                Instantiate(Resources.Load("successVfx"), transform.position 
                    + new Vector3(0,0.2f,0), Quaternion.identity);
            }
            else
            {
                ShowFeedback("wrong, reorganize");
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