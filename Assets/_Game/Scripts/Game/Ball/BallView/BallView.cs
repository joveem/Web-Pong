using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using JovDK.Debug;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

public partial class BallView : MonoBehaviour
{

    [Space(5), Header("[ Dependencies ]"), Space(10)]

    [SerializeField] Animator _animator;


    [Space(5), Header("[ State ]"), Space(10)]

    [SerializeField] float _velocity;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] Transform _ballViewContainer;


    [Space(5), Header("[ Configs ]"), Space(10)]
    [SerializeField] ParticleSystem _ballExplosionParticlePrefab;
    [SerializeField] Vector3 _explosionPositionOffset = new Vector3(0f, 1f, 0f);
    [SerializeField] Vector3 _explosionPositionOffeset = new Vector3(0f, 1f, 0f);
    [SerializeField] Vector3 _explosionScale = new Vector3(5f, 5f, 5f);


    void Start()
    {

        SetupComponent();

    }

    void SetupComponent()
    {

        _animator.DoIfNull(() =>
            SafeActionsTools.TryGetComponent(this, out _animator), false);

    }

    void Update()
    {

        HandleVelocity();

    }

    void HandleVelocity()
    {

        _animator.DoIfNotNull(() =>
            _animator.SetFloat("velocity", _velocity));

    }

    public void SetVelocity(float velocity)
    {

        _velocity = velocity;

    }

    public void ShowBall()
    {

        _ballViewContainer.SetActiveIfNotNull(true);

    }

    public void HideBall()
    {

        _ballViewContainer.SetActiveIfNotNull(false);

    }

    public void ShowBallWithDrop()
    {

        ShowBall();

        _animator.DoIfNotNull(() =>
            _animator.SetTrigger("dropBall"));

    }

    public void GetExplosionData(
        out Vector3 explosionPosition,
        out Quaternion explosionRotation,
        out Vector3 explosionScale
        )
    {

        explosionPosition = _ballViewContainer.position +
                            _explosionPositionOffset;
        explosionRotation = Quaternion.LookRotation(Vector3.up);
        explosionScale = _explosionScale;

    }

}
