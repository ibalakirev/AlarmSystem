using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ControlerCriminal : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string SpeedX = nameof(SpeedX);

    [SerializeField] private float _speedWalk;
    [SerializeField] private Animator _animator; 

    private float _horizontal = 0f;
    private bool _isFaicingRight = true;
    private float _speedMultiplierWalk = 50f;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float valueGetAxis = 0;

        Move();

        if (_horizontal > valueGetAxis && _isFaicingRight == false)
        {
            Flip();
        }
        else if (_horizontal < valueGetAxis && _isFaicingRight == true)
        {
            Flip();
        }
    }

    private void Update()
    {
        _horizontal = Input.GetAxis(Horizontal);

        _animator.SetFloat(SpeedX, Mathf.Abs(_horizontal));
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_horizontal * _speedWalk * _speedMultiplierWalk * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

    private void Flip()
    {
        int multiplierCriminalScale = -1;
        Vector3 criminalScale = transform.localScale;

        _isFaicingRight = !_isFaicingRight;

        criminalScale.x *= multiplierCriminalScale;
        transform.localScale = criminalScale;
    }
}
