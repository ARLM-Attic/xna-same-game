using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SameGameXna
{
	public class ServiceContainer : IServiceProvider
	{
		Dictionary<Type, Object> services;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ServiceContainer()
			: base()
		{
			this.services = new Dictionary<Type, object>();
		}

		/// <summary>
		/// Registers a service provider.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="provider"></param>
		public void AddService(Type type, Object provider)
		{
			if(null == type)
				throw new ArgumentNullException("type");

			if(null == provider)
				throw new ArgumentNullException("provider");

			if(this.services.ContainsKey(type))
				throw new InvalidOperationException("A provider is already registered the type " + type);

			var providerType = provider.GetType();

			if(!type.IsAssignableFrom(providerType))
				throw new InvalidOperationException(providerType + " is not an instance of " + type);

			this.services.Add(type, provider);
		}

		/// <summary>
		/// Attempts to create a requested service provider when none is currently registered.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		protected virtual object CreateService(Type type)
		{
			return null;
		}

		/// <summary>
		/// Returns a registered service provider.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public object GetService(Type type)
		{
			if(null == type)
				throw new ArgumentNullException("type");

			if(this.services.ContainsKey(type))
				return this.services[type];

			var provider = this.CreateService(type);

			if(null != provider)
			{
				this.AddService(type, provider);
				return provider;
			}

			return null;
		}

		/// <summary>
		/// Unregisters a service provider.
		/// </summary>
		/// <param name="type"></param>
		public void RemoveService(Type type)
		{
			if(null == type)
				throw new ArgumentNullException("type");

			this.services.Remove(type);
		}
	}
}
