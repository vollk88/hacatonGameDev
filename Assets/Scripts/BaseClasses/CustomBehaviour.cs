using System;
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
            Type type = monoBehaviour.GetType();

            // Все поля, помеченные атрибутом GetOnObject
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.GetCustomAttributes(typeof(CustomBehaviour.GetOnObject), false).Length > 0)
                .ToArray();
        
            foreach (var field in fields)
            {
                Type dependencyType = field.FieldType;

                if (field.GetValue(monoBehaviour) is Component)
                {
                    Component component = monoBehaviour.GetComponent(dependencyType);
                    field.SetValue(monoBehaviour, component);
                }
                else
                {
                    field.SetValue(monoBehaviour, new());
                }
            }
        }
    }
    public class CustomBehaviour : MonoBehaviour
    {

        protected internal class GetOnObject : Attribute {}

        protected void Awake()
        {
            DependencyInjector.Inject(this);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
