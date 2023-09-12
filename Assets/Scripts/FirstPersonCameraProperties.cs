/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace SnapshotChronicles.MyCamera
{
    using Opsive.UltimateCharacterController.FirstPersonController.Items;
    using UnityEngine;

    /// <summary>
    /// Describes any first person perspective dependent properties for the Camera
    /// </summary>
    public class FirstPersonCameraProperties : FirstPersonItemProperties, ICameraPerspectiveProperties
    {
        [Tooltip("A reference to the light used by the flashlight.")]
        [SerializeField] protected GameObject m_Camera;

        [Opsive.Shared.Utility.NonSerialized] public GameObject Camera { get { return m_Camera; } set { m_Camera = value; } }

        protected override void Awake()
        {
            base.Awake();
            Debug.Log("Call FirstPersonCameraProperties.Awake()");
        }
    }
}