using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
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
            else
            {
                field.SetValue(monoBehaviour, Activator.CreateInstance(dependencyType));
            }
        }
    }
    public class CustomBehaviour : MonoBehaviour
    {

        protected internal class GetOnObject : Attribute {}

        protected virtual void Awake()
        {
            DependencyInjector.Inject(this);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
