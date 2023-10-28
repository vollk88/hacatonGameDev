using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class ItemThrower : IInput
    {
        private readonly Unit.Character.CharacterController _characterController;
        private readonly Transform _throwThrowPoint;
        private readonly float _throwForce;
        public bool IsAiming { get; private set; }

        public ItemThrower(Transform throwPoint, 
            Unit.Character.CharacterController characterController, float throwForce)
        {
            _throwThrowPoint = throwPoint;
            _characterController = characterController;
            _throwForce = throwForce;
        }
        
        private void StartAiming(InputAction.CallbackContext context)
        {
            IsAiming = true;
        }
        
        private void CancelAiming(InputAction.CallbackContext context)
        {
            IsAiming = false;            
        }

        private void Throw(InputAction.CallbackContext context)
        {
            if(!IsAiming)
                return;
            
            GameObject grenade = Object.Instantiate(_characterController.GetThrowableObject(), _throwThrowPoint.position, _throwThrowPoint.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddForce(_throwThrowPoint.forward * _throwForce, ForceMode.VelocityChange);
            IsAiming = false;
        }
        
        public void SubscribeEvents()
        {
            InputManager.PlayerActions.Throw.started += Throw;
            
            InputManager.PlayerActions.Aiming.started += StartAiming;
            InputManager.PlayerActions.Aiming.canceled += CancelAiming;
        }

        public void UnsubscribeEvents()
        {
            InputManager.PlayerActions.Throw.started -= Throw;
            
            InputManager.PlayerActions.Aiming.started -= StartAiming;
            InputManager.PlayerActions.Aiming.canceled -= CancelAiming;
        }
    }
}