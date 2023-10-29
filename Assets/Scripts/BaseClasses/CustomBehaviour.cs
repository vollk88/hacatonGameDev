using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;
using Debug = UnityEngine.Debug;

namespace BaseClasses
{

    public static class DependencyInjector
    {
        public static void Inject(CustomBehaviour monoBehaviour)
        {
            Type? type = monoBehaviour.GetType();
            List<FieldInfo> fields = new();
            GetAllFieldsWithAttribute(type, ref fields);

            foreach (var field in fields)
            {
                SetField(field, monoBehaviour);
            }
        }

        private static void GetAllFieldsWithAttribute(Type? type, ref List<FieldInfo> fields)
        {
            while (type != typeof(CustomBehaviour))
            {
                if (type != null)
                {
                    fields.AddRange(type
                        .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Where(field => field.GetCustomAttributes(typeof(CustomBehaviour.GetOnObject), false).Any()));

                    type = type.BaseType;
                }
            }
        }

        private static void SetField(FieldInfo field, CustomBehaviour monoBehaviour)
        {
            Type dependencyType = field.FieldType;

            if (dependencyType.IsSubclassOf(typeof(Component)))
            {
                Component component = monoBehaviour.GetComponent(dependencyType);
                field.SetValue(monoBehaviour, component);
            }
            else if (dependencyType.IsInterface)
            {
                monoBehaviour.gameObject.GetComponents<Component>().ToList().ForEach(component =>
                {
                    if (dependencyType.IsInstanceOfType(component))
                    {
                        field.SetValue(monoBehaviour, component);
                    }
                });
            }
            else
            {
                field.SetValue(monoBehaviour, Activator.CreateInstance(dependencyType));
            }
        }
    }

    public class CustomBehaviour : MonoBehaviour
    {

        public static readonly Dictionary<Type, List<CustomBehaviour>> Instances = new();

        protected internal class GetOnObject : Attribute
        {
        }

        protected virtual void AddInstance()
        {
            Type type = GetType();
            if (!Instances.ContainsKey(type))
            {
                Instances.Add(type, new List<CustomBehaviour>());
            }

            Instances[type].Add(this);
        }

        protected virtual void RemoveInstance()
        {
            Type type = GetType();
            if (Instances.TryGetValue(type, out List<CustomBehaviour> instance))
            {
                instance.Remove(this);
            }
        }

        protected virtual void OnEnable()
        {
            AddInstance();
        }

        protected virtual void OnDisable()
        {
            RemoveInstance();
        }

        protected virtual void Awake()
        {
            DependencyInjector.Inject(this);
        }

        public static CharacterController GetCharacterController()
        {
            if (CustomBehaviour.Instances.ContainsKey(typeof(CharacterController)) == false)
            {
                // Debug.LogError("No CharacterController found");
                return null;
            }

            return CustomBehaviour.Instances[typeof(CharacterController)][0] as CharacterController;
        }
    }
}
