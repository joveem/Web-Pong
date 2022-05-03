using System;
using UnityEngine;
using JovDK.Models.Network;

namespace WebPong.Models.WebSocketEvents
{

    [Serializable]
    public class ScoringRequestEventData
    {

        public ScoringRequestEventData(int scoringPlayerIndex)
        {

            ScoringPlayerIndex = scoringPlayerIndex;

        }

        public int ScoringPlayerIndex = -1;

    }

    [Serializable]
    public class ScoreUpdateEventData
    {

        public ScoreUpdateEventData(int[] playersScores)
        {

            PlayersScores = playersScores;

        }

        public int[] PlayersScores = new int[] { };

    }

    [Serializable]
    public class BallExplosionEventData
    {

        public BallExplosionEventData(
            Vector3 position,
            Quaternion rotation,
            Vector3 scale,
            int receiverIndex

        )
        {

            Position = new SerializableVector3(position);
            EulerRotation = new SerializableVector3(rotation.eulerAngles);
            Scale = new SerializableVector3(scale);
            ReceiverPlayerIndex = receiverIndex;

        }

        public SerializableVector3 Position;
        public SerializableVector3 EulerRotation;
        public SerializableVector3 Scale;
        public int ReceiverPlayerIndex = -1;

    }

}
