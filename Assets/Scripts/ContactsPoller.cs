using UnityEngine;

public class ContactsPoller
{
    private const float CollisionThreshold = 0.5f;
    private const float GroundAngleThreshold = 0.7f;

    private readonly ContactPoint2D[] _contacts = new ContactPoint2D[5];
    private int _contactsCount;
    private readonly Collider2D _collider;

    public bool IsGrounded { get; private set; }
    public bool HasLeftContacts { get; private set; }
    public bool HasRightContacts { get; private set; }
    public Vector2 GroundNormal { get; private set; }

    public ContactsPoller(Collider2D collider)
    {
        _collider = collider;
    }

    public void FixedUpdate()
    {
        IsGrounded = false;
        HasLeftContacts = false;
        HasRightContacts = false;
        GroundNormal = Vector2.zero;

        _contactsCount = _collider.GetContacts(_contacts);

        for (var i = 0; i < _contactsCount; i++)
        {
            var normal = _contacts[i].normal;
            var contactPoint = _contacts[i].point;
            var colliderCenter = _collider.bounds.center;

            if (normal.y > GroundAngleThreshold && contactPoint.y < colliderCenter.y)
            {
                IsGrounded = true;
                GroundNormal = normal;
            }

            if (!(Mathf.Abs(normal.x) > CollisionThreshold)) continue;
            switch (normal.x)
            {
                case > CollisionThreshold when contactPoint.x < colliderCenter.x:
                    HasLeftContacts = true;
                    break;
                case < -CollisionThreshold when contactPoint.x > colliderCenter.x:
                    HasRightContacts = true;
                    break;
            }
        }
    }
}