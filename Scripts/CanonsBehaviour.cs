using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonsBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform _target;
    public Spawner[] _cannonsTip;
    public MissileBehaviour _projectilePrefab;

    public float _attackSpeedModifier = 10f;
    float _lerpedValue;

    Vector3 _targetDirection;
    Vector3 _localLookVector = Vector3.up;
    Vector3 _rotationAxis = Vector3.forward;
    Vector3 _worldLookAxis;

    float _delta;
    float _rotationStep;

    public float _lookTowardSpeed;

    [Range(.1f,10f)]
    public float _shootReload = 5f;
    float _currentShootReload;

    private void Awake()
    {
        _currentShootReload = _shootReload;
        CountdownShoot();
    }

    private void Update()
    {
        _worldLookAxis = transform.TransformDirection(_localLookVector).normalized;
        _targetDirection = (_target.position - transform.position).normalized;

        Debug.Log((_lerpedValue * _attackSpeedModifier).ToString());
        //if (Mathf.Abs(_delta) == 180f)
        if (Vector3.Dot(_worldLookAxis, _targetDirection) == -1) // might want to change for previous condition
        {
            _worldLookAxis = (_worldLookAxis + transform.right * 0.01f);
        }

        _rotationAxis = Vector3.Cross(_targetDirection, _worldLookAxis);

        _delta = Vector3.SignedAngle(_worldLookAxis, _targetDirection, _rotationAxis);
        _rotationStep = Mathf.Min(Mathf.Abs(_delta), _lookTowardSpeed * Time.deltaTime) * Mathf.Sign(_delta);

        transform.Rotate(_rotationAxis, _rotationStep, Space.World);
    }

    void ShootReloadMuliplier()
    {
        if (_lerpedValue * _attackSpeedModifier > 0 && _lerpedValue * _attackSpeedModifier < _shootReload - _shootReload/5)
            _currentShootReload = _shootReload - (_lerpedValue * _attackSpeedModifier);
    }

    void CountdownShoot()
    {
        Invoke("Shoot",_currentShootReload);
    }

    void Shoot()
    {
        for (int i = 0; i < _cannonsTip.Length; i++)
        {
            GameObject tempSpawn = _cannonsTip[i].Spawn(_projectilePrefab.gameObject);
        }
        _lerpedValue = FindObjectOfType<Timer>()._lerpedValue;
        ShootReloadMuliplier();
        CountdownShoot();
    }
}
