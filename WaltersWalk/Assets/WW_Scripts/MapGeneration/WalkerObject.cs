using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerObject : MonoBehaviour
{

    public Vector2 _position;
    public Vector2 _direction;
    public float _ChanceToChange;

    public WalkerObject(Vector2 _position, Vector2 _direction, float _ChanceToChange)
    {
        this._position = _position;
        this._ChanceToChange = _ChanceToChange;
        this._direction = _direction;
    }

    public Vector3 GlobalPosition()
    {
        return new Vector3(_position.x + .5f, 0, _position.y + .5f);
    }

}
