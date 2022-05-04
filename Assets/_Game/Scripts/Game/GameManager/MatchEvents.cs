using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

using JovDK.SafeActions;
using JovDK.Debug;

using WebPong.Models.WebSocketEvents;

public partial class GameManager : MonoBehaviour
{

    public void RegisterPlayerScoring(int scoringPlayerIndex)
    {

        _networkManager.SendScoring(scoringPlayerIndex);

    }

    public void ApplyScore(int[] playersScores)
    {

        _matchScorePanel.DoIfNotNull(() =>
            _matchScorePanel.SetPlayersScores(playersScores));

    }

    public void MakeExplosion(
        Vector3 explosionPosition,
        Quaternion explosionRotation,
        Vector3 explosionScale,
        int currentPlayerIndex)
    {

        InstantiateExplosion(explosionPosition, explosionRotation, explosionScale);

        _ball.DoIfNotNull(() =>
            _ball.DisableBall());

        _networkManager.SendExplosion(
            explosionPosition,
            explosionRotation,
            explosionScale,
            currentPlayerIndex);

    }

    public void ApplyExplosion(
        Vector3 explosionPosition,
        Quaternion explosionRotation,
        Vector3 explosionScale)
    {

        InstantiateExplosion(explosionPosition, explosionRotation, explosionScale);

        _ball.DoIfNotNull(() =>
            _ball.DisableBall());

    }


    void InstantiateExplosion(
        Vector3 explosionPosition,
        Quaternion explosionRotation,
        Vector3 explosionScale)
    {

        _ballExplosionParticlePrefab.DoIfNotNull(() =>
        {

            ParticleSystem explosionInstance = Instantiate(
                _ballExplosionParticlePrefab,
                explosionPosition,
                explosionRotation);

            explosionInstance.transform.localScale = explosionScale;

        });

    }

    public Dictionary<int, float> timeByPingId = new Dictionary<int, float>();

    async void UpdateLatency()
    {

        int pingId = Random.Range(10000, 99999);

        while (timeByPingId.ContainsKey(pingId))
            pingId = Random.Range(10000, 99999);

        timeByPingId.Add(pingId, Time.time);

        _networkManager.SendPing(pingId);

    }

    public void ApplyPing(int pingId)
    {

        if (timeByPingId.TryGetValue(pingId, out float pingTimeValue))
        {

            float latencyTimeInSecond = Time.time - pingTimeValue;

            _matchScorePanel.SetLatency(latencyTimeInSecond);

            timeByPingId.Remove(pingId);

        }

    }

}
