using UnityEngine;

namespace XomracUtilities.Patterns
{
	public abstract class ServiceComponent : MonoBehaviour
	{ }

	public abstract class ServiceComponent<T> : ServiceComponent where T : ServiceLocator
	{
		public T ServiceLocator { get; private set; }
		
		protected virtual void Awake()
		{
			ServiceLocator = GetComponent<T>();
			if (ServiceLocator==null)
			{
				enabled = false;
			}
			Debug.Assert(ServiceLocator != null, $"No ServiceLocator found of type {typeof(T)}", this);
		}

		//just to make possible to enable/disable this script
		protected virtual void Start()
		{}
	}
	
	public abstract class SerializedServiceComponent<T> : ServiceComponent where T : SerializedServiceLocator
	{
		public T ServiceLocator { get; private set; }
		
		protected virtual void Awake()
		{
			ServiceLocator = GetComponent<T>();
			if (ServiceLocator==null)
			{
				enabled = false;
			}
			Debug.Assert(ServiceLocator != null, $"No ServiceLocator found of type {typeof(T)}", this);
		}

		//just to make possible to enable/disable this script
		protected virtual void Start()
		{}
	}
	

}