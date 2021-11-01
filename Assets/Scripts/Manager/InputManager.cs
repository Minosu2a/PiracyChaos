using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    #region Fields
    private Vector3 _moveDir = Vector3.zero;
    private Vector3 _rotDir = Vector3.zero;

    [Header("Mouse")]
    [SerializeField] private bool _mouseActivated = true;

    [Header("Action")]
    [SerializeField] private KeyCode _mainFireInput = KeyCode.Mouse0;






    #endregion Fields

    #region Property
    public Vector3 MoveDir => _moveDir.normalized;
    public Vector3 RotDir => _rotDir.normalized;


    public bool MouseActivated
    {
        get
        {
            return _mouseActivated;
        }
        set
        {
            _mouseActivated = value;
        }
    }



    #endregion Property

    #region Methods
    public void Initialize()
    {

    }


    protected override void Update()
    {
        #region Movement & Rotation
        _moveDir.x = Input.GetAxis("Horizontal");
        _moveDir.z = Input.GetAxis("Vertical");


        if (_mouseActivated == true)
        {

        }
        else
        {
            _rotDir.x = Input.GetAxis("RotationX");
            _rotDir.z = Input.GetAxis("RotationZ");
        }
        #endregion Movement & Rotation

        if (Input.GetKey(_mainFireInput))
        {

        }



    }
    #endregion Methods


}