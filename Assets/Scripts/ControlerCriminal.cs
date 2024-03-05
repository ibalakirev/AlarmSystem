using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ControlerCriminal : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Ground = nameof(Ground);
    private const string SpeedX = nameof(SpeedX);

    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedJump;
    [SerializeField] private Animator _animator; 

    private float _horizontal = 0f;
    private bool _isFaicingRight = true;
    private bool _isGround = false;
    private bool _isJump = false;
    private float _speedMultiplierWalk = 50f;
    private float _speedMultiplierJump = 1000f;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis(Horizontal);

        _animator.SetFloat(SpeedX, Mathf.Abs(_horizontal));

        if (Input.GetKeyDown(KeyCode.Space) && _isGround == true)
        {
            _isJump = true;
        }
    }

    private void FixedUpdate()
    {
        float valueGetAxis = 0;

        Move();

        if (_isJump == true)
        {
            Jump();
        }

        if (_horizontal > valueGetAxis && _isFaicingRight == false)
        {
            Flip();
        }
        else if (_horizontal < valueGetAxis && _isFaicingRight == true)
        {
            Flip();
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_horizontal * _speedWalk * _speedMultiplierWalk * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, _speedJump * _speedMultiplierJump * Time.fixedDeltaTime));

        _isGround = false;
        _isJump = false;
    }

    private void Flip()
    {
        int multiplierCriminalScale = -1;
        Vector3 criminalScale = transform.localScale;

        _isFaicingRight = !_isFaicingRight;

        criminalScale.x *= multiplierCriminalScale;
        transform.localScale = criminalScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Ground))
        {
            _isGround = true;
        }
    }
}
