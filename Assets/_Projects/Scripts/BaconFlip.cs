using UnityEngine;

public class BaconFlip : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float thrust = 6f;

    private PanController _panFlip;
    private HandController _handController;
    private Vector3 _initialPosition;
    
    public Rigidbody2D Rb => rb;
    
    public void OnInit(HandController handController)
    {
        _initialPosition = transform.localPosition;
        _handController = handController;
        
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    private void Flip(Vector2 forceDirection)
    {
        rb.AddForce(forceDirection * thrust, ForceMode2D.Impulse);
    }

    public void ResetBacon()
    {
        SetVelocityZero();
        transform.localPosition = _initialPosition;
        transform.localRotation = Quaternion.identity;
    }

    public void SetVelocityZero()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pan")) 
        {
            _panFlip = collision.gameObject.GetComponentInParent<PanController>();
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if(_panFlip != null && _panFlip.IsFlippingUp)
        {
            Flip(Vector2.up);
            _panFlip.SetIsFlippingUp(false);
        }

        if (other.gameObject.layer != LayerMask.NameToLayer("Level")) return;
        
        if(!_handController.IsDone()) return;
        _handController.SetIsDrop(false);
        SetVelocityZero();
        
        GameManager.Instance.OnWin();
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Pan"))
        {
            _panFlip = null;
        }
    }
}
