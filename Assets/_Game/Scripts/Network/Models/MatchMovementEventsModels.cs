using System;
using UnityEngine;
using JovDK.Models.Network;

namespace WebPong.Models.WebSocketEvents
{


    [Serializable]
    public class PlayerPositionEventData
    {

        /// <param name="destinationPosition">
        /// Player destination position (between -1f and 1f)
        /// </param>
        public PlayerPositionEventData(int playerIndex, float destinationPosition)
        {

            PlayerIndex = playerIndex;
            DestinationPosition = destinationPosition;

        }

        public int PlayerIndex = -1;
        public float DestinationPosition = 0f;

    }

    [Serializable]
    public class BallStateEventData
    {


        public BallStateEventData(
            int currentControllerPlayerIndex,
            Vector3 position,
            Vector3 eulerRotation,
            float velocity)
        {

            CurrentControllerPlayerIndex = currentControllerPlayerIndex;
            Position = new SerializableVector3(position);
            EulerRotation = new SerializableVector3(eulerRotation);
            Velocity = velocity;

        }

        public int CurrentControllerPlayerIndex;
        public SerializableVector3 Position;
        public SerializableVector3 EulerRotation;
        public float Velocity;

    }

    [Serializable]
    public class BallPositionEventData
    {

        public BallPositionEventData(Vector3 position, Vector3 eulerRotation, float velocity)
        {

            Position = new SerializableVector3(position);
            EulerRotation = new SerializableVector3(eulerRotation);
            Velocity = velocity;

        }

        public SerializableVector3 Position;
        public SerializableVector3 EulerRotation;
        public float Velocity;

    }

}
