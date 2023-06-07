using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace XomracUtilities.Patterns
{

	public abstract class SerializedServiceLocator : SerializedMonoBehaviour
	{
		public readonly Dictionary<Type, ServiceComponent> services = new();

		public abstract void PopulateDictionary();
		
		public T GetService<T>() where T : ServiceComponent
		{
			return (T)services[typeof(T)];
		}
		
		public T GetEnabledService<T>() where T : ServiceComponent
		{
			if (services[typeof(T)]!=null && services[typeof(T)].enabled )
			{
				return (T)services[typeof(T)];
			}
			return null;
		}
		
		
		public T AddService<T>(bool addEnabled=true) where T : ServiceComponent
		{
			if (services.ContainsKey(typeof(T)))
			{
				return (T)services[typeof(T)];
			}
			var newService = gameObject.AddComponent<T>();
			newService.enabled = addEnabled;
			services.Add(typeof(T),newService);
			return newService;
		}
		
		public void RemoveService<T>() where T : ServiceComponent
		{
			if (!services.ContainsKey(typeof(T)))
			{
				return;
			}
			var serviceToRemove = gameObject.GetComponent<T>();
			services.Remove(typeof(T));
			Destroy(serviceToRemove);
		}
	}

}