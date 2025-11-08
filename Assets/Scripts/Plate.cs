using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class Plate : MonoBehaviour
{
    public int bottleID;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private PlateSlot currentSlot;

    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // 初始状态：不在插槽中，启用物理
        rb.isKinematic = false;
        rb.useGravity = true;

        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (currentSlot != null)
        {
            currentSlot.OccupyingBottle = null;
            currentSlot = null;
        }

        rb.isKinematic = false;
    }

    public void SnapToSlot(PlateSlot slot)
    {
        if (currentSlot != null)
        {
            currentSlot.OccupyingBottle = null;
        }

        currentSlot = slot;
        slot.OccupyingBottle = this;

        // 移动到插槽位置
        transform.SetParent(slot.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // 放入插槽后，禁用物理（固定位置）
        rb.isKinematic = true;

        Instantiate(Resources.Load("snapVfx"), transform.position, Quaternion.identity);
    }

    private void OnTriggerStay(Collider other)
    {
        // 只有在未被抓取的状态下才检测吸附
        if (!grabInteractable.isSelected && other.TryGetComponent(out PlateSlot slot))
        {
            // 插槽为空时才吸附
            if (slot.OccupyingBottle == null)
            {
                audioSource.Play();
                SnapToSlot(slot);
                PlatePuzzleManager.Instance.CheckCompletion();
            }
        }
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }
}