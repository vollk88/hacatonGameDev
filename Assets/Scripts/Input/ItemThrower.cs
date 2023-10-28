using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Unit.Character.CharacterController;

namespace Input
{
    public class ItemThrower : AInput
    {
        private readonly Transform _throwThrowPoint;
        private readonly float _throwForce;
        public bool IsAiming { get; private set; }

        public ItemThrower(CharacterController characterController, Transform throwPoint,
            float throwForce) : base(characterController)
        {
            _throwThrowPoint = throwPoint;
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
            
            GameObject throwItem = Object.Instantiate(Character.GetThrowableObject(), 
                _throwThrowPoint.position, _throwThrowPoint.rotation);
            // GameObject throwItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // throwItem.transform.position = _throwThrowPoint.position;

            Rigidbody rb = throwItem.AddComponent<Rigidbody>();

            if (rb != null)
                rb.AddForce(CinemachineBrainTransform.forward * _throwForce, ForceMode.VelocityChange);
            IsAiming = false;
        }
        
        public override void SubscribeEvents()
        {
            InputManager.PlayerActions.Throw.started += Throw;
            
            InputManager.PlayerActions.Aiming.started += StartAiming;
            InputManager.PlayerActions.Aiming.canceled += CancelAiming;
        }

        public override void UnsubscribeEvents()
        {
            InputManager.PlayerActions.Throw.started -= Throw;
            
            InputManager.PlayerActions.Aiming.started -= StartAiming;
            InputManager.PlayerActions.Aiming.canceled -= CancelAiming;
        }
    }
}