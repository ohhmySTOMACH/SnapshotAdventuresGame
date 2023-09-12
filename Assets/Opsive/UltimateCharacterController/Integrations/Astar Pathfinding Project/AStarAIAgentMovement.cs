/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

using UnityEngine;
using Opsive.UltimateCharacterController.Character.Abilities.AI;
using Pathfinding;

namespace Opsive.UltimateCharacterController.Integrations.AstarPathfindingProject
{
    /// <summary>
    /// Moves the character according to the IAstarAI interface.
    /// </summary>
    public class AStarAIAgentMovement : PathfindingMovement
    {
        [Tooltip("The distance away from the destination when the agent is stopping.")]
        [SerializeField] protected float m_StoppingDistance = 0.2f;

        private IAstarAI m_IAstarAIAgent;

        private Vector2 m_InputVector;
        private Vector3 m_DeltaRotation;

        private Vector3 m_NextPosition;
        private Quaternion m_NextRotation;
        private bool m_PrevEnabled = true;

        public override Vector2 InputVector { get { return m_InputVector; } }
        public override Vector3 DeltaRotation { get { return m_DeltaRotation; } }
        public override bool HasArrived { get { return m_IAstarAIAgent.remainingDistance <= m_StoppingDistance; } }

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            m_IAstarAIAgent = m_GameObject.GetComponent<IAstarAI>();
            if (m_IAstarAIAgent == null) {
                Debug.LogError("Error: A component that implements IAstarAI must be added to the character " + m_GameObject.name);
                Enabled = false;
                return;
            }

            // The controller will do all of the movement.
            m_IAstarAIAgent.canMove = false;
        }

        /// <summary>
        /// Updates the ability.
        /// </summary>
        public override void Update()
        {
            m_IAstarAIAgent.MovementUpdate(Time.fixedDeltaTime, out m_NextPosition, out m_NextRotation);
            var localVelocity = m_Transform.InverseTransformDirection((m_NextPosition - m_Transform.position) / Time.fixedDeltaTime);
            localVelocity.y = 0;
            // Only move if a path exists.
            m_InputVector = Vector2.zero;
            if (m_IAstarAIAgent.hasPath && localVelocity.sqrMagnitude > 0.01f && m_IAstarAIAgent.remainingDistance > 0.01f) {
                // Only normalize if the magnitude is greater than 1. This will allow the character to walk.
                if (localVelocity.sqrMagnitude > 1) {
                    localVelocity.Normalize();
                }
                m_InputVector.x = localVelocity.x;
                m_InputVector.y = localVelocity.z;
            }

            var rotation = m_NextRotation * Quaternion.Inverse(m_Transform.rotation);
            m_DeltaRotation.y = Utility.MathUtility.ClampInnerAngle(rotation.eulerAngles.y);

            base.Update();
        }

        /// <summary>
        /// Ensure the move direction is valid.
        /// </summary>
        public override void ApplyPosition()
        {
            if (HasArrived) {
                // Prevent the character from jittering back and forth to land precisely on the target.
                var direction = m_Transform.InverseTransformPoint(m_IAstarAIAgent.destination);
                var moveDirection = m_Transform.InverseTransformDirection(m_CharacterLocomotion.MoveDirection);
                if (Mathf.Abs(moveDirection.x) > Mathf.Abs(direction.x)) {
                    moveDirection.x = direction.x;
                }
                if (Mathf.Abs(moveDirection.z) > Mathf.Abs(direction.z)) {
                    moveDirection.z = direction.z;
                }
                m_CharacterLocomotion.MoveDirection = m_Transform.TransformDirection(moveDirection);
            }
        }

        /// <summary>
        /// Notify IAStarAI of the final position and rotation after the movement is complete.
        /// </summary>
        public override void LateUpdate()
        {
            m_IAstarAIAgent.FinalizeMovement(m_Transform.position, m_Transform.rotation);
        }

        /// <summary>
        /// Sets the destination of the pathfinding agent.
        /// </summary>
        /// <param name="target">The position to move towards.</param>
        /// <returns>True if the destination was set.</returns>
        public override bool SetDestination(Vector3 target)
        {
            // Set the new destination if the ability is already active.
            if (m_IAstarAIAgent.hasPath && IsActive) {
                m_IAstarAIAgent.destination = target;
                return true;
            }

            // The NavMeshAgent must be enabled in order to set the destination.
            m_PrevEnabled = Enabled;
            Enabled = true;
            // Move towards the destination.
            if (m_IAstarAIAgent.isStopped) {
                m_IAstarAIAgent.destination = target;
                StartAbility();
                return true;
            }
            Enabled = m_PrevEnabled;
            return false;
        }

        /// <summary>
        /// Returns the destination of the pathfinding agent.
        /// </summary>
        /// <returns>The destination of the pathfinding agent.</returns>
        public override Vector3 GetDestination()
        {
            return m_IAstarAIAgent.destination;
        }

        /// <summary>
        /// Teleports the agent to the specified position.
        /// </summary>
        /// <param name="position">The position that the agent should teleport to.</param>
        public override void Teleport(Vector3 position)
        {
            m_IAstarAIAgent.Teleport(position);
        }
    }
}