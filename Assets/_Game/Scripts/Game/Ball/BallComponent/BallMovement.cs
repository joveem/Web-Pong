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

public partial class Ball : MonoBehaviour
{

    void HandleMovement()
    {


        if (_rigidbody != null && _isActive)
        {

            HandleRotation();
            HandlePosition();

        }

    }

    void HandleRotation()
    {

        transform.rotation = Quaternion.Euler(_currentRotation);

    }

    void HandlePosition()
    {

        float maxBallVelocity = GameManager.instance.MaxBallVelocity;

        if (GameManager.instance.IsMatchStarted && IsInLocalControl())
            _destinationPosition += transform.forward * (maxBallVelocity * Time.fixedDeltaTime);

        /////////////////

        Vector3 targetMovementPositionOffset = _destinationPosition - _currentPosition;
        float deltaMaxBallVelocity = Time.deltaTime * maxBallVelocity;

        Vector3 finalPositionOffset;
        if (GameManager.instance.IsMatchStarted && IsInLocalControl())
        {

            finalPositionOffset = targetMovementPositionOffset;

        }
        else
        {

            targetMovementPositionOffset *= Time.deltaTime * maxBallVelocity;

            finalPositionOffset = targetMovementPositionOffset;

        }

        _currentPosition += finalPositionOffset;

        Vector3 convertedFinalMovement = _currentPosition;

        _rigidbody.MovePosition(convertedFinalMovement);

        _currentVelocity = finalPositionOffset.magnitude / deltaMaxBallVelocity;
        _ballView.SetVelocity(_currentVelocity);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsInLocalControl())
            HandleHit(collision);

    }

    void HandleHit(Collision collision)
    {

        // collision.GetContact(0);

        switch (collision.transform.tag)
        {

            case "player":
                {

                    if (SafeActionsTools.TryGetComponent(collision.transform, out Player hittedPlayer))
                    {

                        if (hittedPlayer.Index == _localPlayerIndex)
                            HandlePlayerHit(hittedPlayer, collision);
                        else
                            DebugExtension.DevLog("hitted "
                                + "other".ToColor(GoodCollors.red) + " Player!");

                    }

                    break;

                }

            case "scenario-side-wall":
                {

                    HandleSideHit(collision);

                    break;

                }

            case "scenario-back-wall":
                {

                    HandleBackHit(collision);
                    break;

                }

            default:
                {

                    string debugText = "UNEXPECTED TAG! ".ToColor(GoodCollors.orange) +
                                        "( tag = " + collision.transform.tag + "  )";

                    DebugExtension.DevLogWarning(debugText);
                    break;

                }

        }

    }

    [SerializeField] Transform debugTransform;

    void HandlePlayerHit(Player hittedPlayer, Collision collision)
    {

        string debugText = "hitted " +
                            "current".ToColor(GoodCollors.green) +
                            " Player!";

        DebugExtension.DevLog(debugText);

        _isFirstRoundHit = false;

        float xHitDistance = collision.transform.position.x - transform.position.x;
        float maxHitDistance = GameManager.instance.MaxHitDistance;
        float rebounceAngleFactor = xHitDistance / maxHitDistance;


        DebugExtension.DevLogWarning("xDistance = " + xHitDistance +
            " | collision.transform.position.x = " + collision.transform.position.x +
            " | transform.position.x = " + transform.position.x);

        Vector3 contactNormal = collision.contacts[0].normal;
        DebugExtension.DevLogWarning("contactNormal = " + contactNormal);
        DebugExtension.DevLogWarning("contactNormal.x = " + contactNormal.x);

        // side hit
        if (Mathf.Abs(contactNormal.x) > 0.01f)
        {

            DebugExtension.DevLogWarning("EXPLODE!".ToColor(GoodCollors.red));

            int scoringPlayerIndex = -1;

            switch (_localPlayerIndex)
            {

                case 0:
                    scoringPlayerIndex = 1;
                    break;

                case 1:
                    scoringPlayerIndex = 0;
                    break;

                default:
                    {

                        debugText =
                            "UNEXPECTED scoringPlayerIndex! ".ToColor(GoodCollors.red) +
                            "( scoringPlayerIndex = " + scoringPlayerIndex + " )";

                        DebugExtension.DevLogWarning(debugText);
                        break;

                    }

            }

            if (scoringPlayerIndex != -1)
                ApplyBallDestroing(scoringPlayerIndex);

        }
        else
        {

            debugTransform.DoIfNotNull(() =>
            {

                Quaternion lookRotation = Quaternion.LookRotation(contactNormal);

                debugTransform.rotation = lookRotation;

            });

            if (Mathf.Abs(xHitDistance) <= maxHitDistance)
            {

                Quaternion baseRotation = Quaternion.Euler(_playersRebounceBaseRotation[_currentControllerPlayerIndex]);
                bool needInvertRotation = _currentControllerPlayerIndex == 0;

                ApplyPlayerHitRotation(baseRotation, rebounceAngleFactor, needInvertRotation);

                debugText = "IN maxHitDistance! " +
                    "( maxHitDistance = " + maxHitDistance + " )";

                DebugExtension.DevLogWarning(debugText.ToColor(GoodCollors.green));
            }
            else
            {

                debugText = "OUT OF maxHitDistance! " +
                    "( maxHitDistance = " + maxHitDistance + " )";

                DebugExtension.DevLogWarning(debugText.ToColor(GoodCollors.red));

            }

            SwitchPlayer();

        }

    }

    void ApplyPlayerHitRotation(Quaternion baseRotation, float angleFactor, bool invertAngleFactor)
    {
        float rebounceAngle;
        bool mustRandomizeRebound = false;

        if (invertAngleFactor)
            angleFactor *= -1;

        if (_isFirstRoundHit)
            mustRandomizeRebound = Mathf.Abs(angleFactor) < 0.09f;

        if (_isFirstRoundHit && mustRandomizeRebound)
        {

            float maxAngleThird = _maxRebounceAngle / 3f;
            rebounceAngle = UnityEngine.Random.Range(-maxAngleThird, maxAngleThird);

        }
        else
            rebounceAngle = _maxRebounceAngle * angleFactor;

        // DebugExtension.DevLogWarning("_isFirstRoundHit = " + _isFirstRoundHit +
        //     " | mustRandomizeRebound = " + mustRandomizeRebound +
        //     " | rebounceAngle = " + rebounceAngle);

        Quaternion rebounceAngleOffSet = Quaternion.Euler(0f, rebounceAngle, 0f);
        Quaternion rebounceRotation = baseRotation * rebounceAngleOffSet;

        _currentRotation = rebounceRotation.eulerAngles;
        transform.rotation = rebounceRotation;

    }

    void ApplyBallDestroing(int scoringPlayerIndex)
    {

        // TODO:
        // send ball explosion
        // inclease opponent score

        if (scoringPlayerIndex >= 0)
        {

            if (scoringPlayerIndex != _localPlayerIndex)
            {
                GameManager.instance.RegisterPlayerScoring(scoringPlayerIndex);

                _ballView.DoIfNotNull(() =>
                {

                    _ballView.GetExplosionData(
                        out Vector3 explosionPosition,
                        out Quaternion explosionRotation,
                        out Vector3 explosionScale);

                    GameManager.instance.MakeExplosion(
                        explosionPosition,
                        explosionRotation,
                        explosionScale,
                        _currentControllerPlayerIndex
                    );

                    _ballView.HideBall();

                });


            }
            else
                ShowIncorrectBallDestroyingWarn(scoringPlayerIndex, _localPlayerIndex);

        }
        else
        {

            string debugText =
                "INVALID player index! ".ToColor(GoodCollors.orange) +
                "( scoringPlayerIndex = " + scoringPlayerIndex + " )";

            DebugExtension.DevLogWarning(debugText);

        }

    }

    void ShowIncorrectBallDestroyingWarn(int scoringPlayerIndex, int localPlayerIndex)
    {

        string debugText = "";

        debugText += "player is trying to destroy a " +
                        "UNDER ENEMY CONTROL ball!";

        debugText = debugText.ToColor(GoodCollors.orange);

        debugText += "( scoringPlayerIndex = " + scoringPlayerIndex +
                        " | _localPlayerIndex = " + localPlayerIndex + " )";

        DebugExtension.DevLogWarning(debugText);

    }

    void HandleSideHit(Collision collision)
    {

        Quaternion finalRotation = transform.rotation;

        // mirror the startRotation in X axys
        finalRotation.y *= -1f;

        _currentRotation = finalRotation.eulerAngles;
        transform.rotation = finalRotation;

    }

    void HandleBackHit(Collision collision)
    {

        string debugText =
            "hitted " +
            "back".ToColor(GoodCollors.red) + " wall! | " +
            "_currentRotation.y = " + _currentRotation.y;

        DebugExtension.DevLog(debugText);

        DebugExtension.DevLogWarning("EXPLODE!".ToColor(GoodCollors.red));

        int scoringPlayerIndex = -1;

        switch (_localPlayerIndex)
        {

            case 0:
                scoringPlayerIndex = 1;
                break;

            case 1:
                scoringPlayerIndex = 0;
                break;

            default:
                {

                    debugText =
                        "UNEXPECTED scoringPlayerIndex! ".ToColor(GoodCollors.red) +
                        "( scoringPlayerIndex = " + scoringPlayerIndex + " )";

                    DebugExtension.DevLogWarning(debugText);
                    break;

                }

        }

        if (scoringPlayerIndex != -1)
            ApplyBallDestroing(scoringPlayerIndex);

    }

}
