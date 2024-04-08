using DR.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class PanController : MonoBehaviour
{
    public static event UnityAction OnFlipPan;
    
    [SerializeField] private float flipAngle = 15f;
    [SerializeField] private float smoothTime = 8f; 
    [SerializeField] private float tolerance = 1f;
    [SerializeField] private bool isFlippingUp = false;
    
    private Quaternion _initialRotation;
    private Quaternion _targetRotation; 
    public bool IsFlippingUp => isFlippingUp;
    
    private void Start()
    {
        _initialRotation = transform.rotation;
    }

    private void Update()
    {
        HandlePanFlip();
        
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * smoothTime);
    }
    
    private void HandlePanFlip()
    {
        if(Input.GetMouseButtonDown(0) && !Helpers.IsPointerOverUI())
        {
            if(!GameManager.Instance.levelController.isReady) return;
            OnFlipPan?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            FlipPanUp();
        }
        else
        {
            ResetPanRotation();
        }
    }

    private void FlipPanUp()
    {
        _targetRotation = Quaternion.Euler(new Vector3(0, 0, flipAngle));
        SetIsFlippingUp(!(Mathf.Abs(transform.eulerAngles.z - _targetRotation.eulerAngles.z) <= tolerance));
    }

    private void ResetPanRotation()
    {
        SetIsFlippingUp(false);
        _targetRotation = _initialRotation;
    }
    
    public void SetIsFlippingUp(bool value)
    {
        isFlippingUp = value;
    }
}
