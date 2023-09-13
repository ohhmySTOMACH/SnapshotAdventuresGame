using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Items.Actions;
using UnityEngine;

namespace SnapshotChronicles.Camera
{
    /// <summary>
    /// A Camera that can take pictures
    /// </summary>
    public class CameraAction : UsableItem
    {
        private ICameraPerspectiveProperties cameraPerpectiveProperties;
  
        protected override void Awake() {
            base.Awake();

            cameraPerpectiveProperties = m_ActivePerspectiveProperties as ICameraPerspectiveProperties;
        }

        protected override void Start() {
            base.Start();

            if (cameraPerpectiveProperties == null) {
                cameraPerpectiveProperties = m_ActivePerspectiveProperties as ICameraPerspectiveProperties;

                if (cameraPerpectiveProperties == null) {
                    Debug.LogError("Error: The First/Third Person Flashlight Item Properties component cannot be found for the Item " + name + "." +
                                   "Ensure the component exists and the component's Action ID matches the Action ID of the Item (" + m_ID + ")");
                }
            }
        }

        public override int GetConsumableItemIdentifierAmount() {
            return -1;
        }

        public override bool CanUseItem(ItemAbility itemAbility, UseAbilityState abilityState) {
            if (!base.CanUseItem(itemAbility, abilityState)) {
                return false;
            }
            return true;
        }

        public override void StartItemUse(ItemAbility itemAbility) {
            base.StartItemUse(itemAbility);
            Debug.Log("CameraAction.StartItemUse");
            cameraPerpectiveProperties.Camera.GetComponent<CameraCapture>().OnCapture();
        }

        public void ToggleFlashlight(bool active) {
            cameraPerpectiveProperties.Camera.SetActive(active);
        }

        public override void StartUnequip() {
            base.StartUnequip();
        }

        protected override void OnChangePerspectives(bool firstPersonPerspective) {
            base.OnChangePerspectives(firstPersonPerspective);

            var active = cameraPerpectiveProperties.Camera.activeSelf;
            cameraPerpectiveProperties.Camera.SetActive(false);
            cameraPerpectiveProperties = m_ActivePerspectiveProperties as ICameraPerspectiveProperties;
            if (active) {
                cameraPerpectiveProperties.Camera.SetActive(true);
            }
        }

        protected override void OnDestroy() {
            base.OnDestroy();
        }
    }
}