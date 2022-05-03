using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

using JovDK.SafeActions;

public class PlayerView : MonoBehaviour
{


    // [Space(5), Header("[ Dependencies ]"), Space(10)]

    [Space(5), Header("[ State ]"), Space(10)]

    [SerializeField] float _velocity;
    [SerializeField] float currentVelocity;
    [SerializeField] float destinationBodyworkVelocity;
    [SerializeField] Quaternion _initialBodyworkRotation;


    [Space(5), Header("[ Parts ]"), Space(10)]

    [SerializeField] Transform bodyworkTransform;
    [SerializeField] Transform[] wheels;


    [Space(5), Header("[ Configs ]"), Space(10)]


    [SerializeField] bool invertGlobalRotation = false;

    [SerializeField] float bodyworkMaximumRotation = 5f;
    [SerializeField] float wheelsMaximumRotationVelocity = 45f;
    [SerializeField] float bodyWorkVelocitySensibility = 5f;

    private void Start()
    {

        SetupComponent();

    }

    void SetupComponent()
    {

        _initialBodyworkRotation = bodyworkTransform.rotation;

    }

    private void Update()
    {

        ApplyVelocity(_velocity);

    }

    public void SetVelocity(float velocity)
    {

        _velocity = velocity;

    }

    void ApplyVelocity(float velocity)
    {
        destinationBodyworkVelocity = velocity;

        currentVelocity = Mathf.Lerp(currentVelocity, destinationBodyworkVelocity, bodyWorkVelocitySensibility * Time.deltaTime);

        float velocityDifference = currentVelocity - destinationBodyworkVelocity;

        velocityDifference = Mathf.Clamp(velocityDifference, -1, 1);

        bodyworkTransform.DoIfNotNull(() =>
        {

            float xAxysOffsetRotation = bodyworkMaximumRotation *
                                        (!invertGlobalRotation ? velocityDifference : -velocityDifference);

            Quaternion rotationOffset = Quaternion.Euler(xAxysOffsetRotation, 0, 0);
            bodyworkTransform.rotation = _initialBodyworkRotation * rotationOffset;

        });

        float playerMaxVelocity = GameManager.instance.MaxPlayersInputVelocity;

        foreach (Transform wheel in wheels)
        {
            wheel.DoIfNotNull(() =>
            {

                float xAxysOffsetRotation = wheelsMaximumRotationVelocity * playerMaxVelocity * (!invertGlobalRotation ? velocity : -velocity);

                wheel.Rotate(xAxysOffsetRotation, 0f, 0f);

                // wheel.Glo(new Vector3(wheelsMaximumRotationVelocity * velocity, 0f, 0f));

            });
        }

    }
}
