using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SameGameXna.Xna
{
	public class GraphicsDeviceService : IGraphicsDeviceService, IDisposable
	{
		Game game;
		Control host;
		
		/// <summary>
		/// Gets a handle to the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/>.
		/// </summary>
		public GraphicsDevice GraphicsDevice
		{
			get;
			private set;
		}

		public event EventHandler DeviceCreated;
		public event EventHandler DeviceDisposing;
		public event EventHandler DeviceReset;
		public event EventHandler DeviceResetting;

		/// <summary>
		/// Constructor.
		/// </summary>
		public GraphicsDeviceService(Game game)
		{
			this.game = game;
			this.game.Services.AddService(typeof(IGraphicsDeviceService), this);
		}

		~GraphicsDeviceService()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(DeviceDisposing != null)
					DeviceDisposing(this, EventArgs.Empty);

				this.GraphicsDevice.Dispose();
			}

			this.GraphicsDevice = null;
		}

		/// <summary>
		/// Creates the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/>.
		/// </summary>
		/// <param name="control">The host control.</param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void CreateDevice(Control control, int width, int height)
		{
			this.host = control;

			PresentationParameters pp;

			pp = new PresentationParameters();

			pp.BackBufferWidth = control.ClientSize.Width;
			pp.BackBufferHeight = control.ClientSize.Height;
			pp.BackBufferFormat = SurfaceFormat.Color;

			pp.EnableAutoDepthStencil = true;
			pp.AutoDepthStencilFormat = DepthFormat.Depth24;

			this.GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware, control.Handle, pp);

			if(this.DeviceCreated != null)
				this.DeviceCreated(this, EventArgs.Empty);
		}

		public bool IsDeviceValid()
		{
            if(this.GraphicsDevice.GraphicsDeviceStatus == GraphicsDeviceStatus.Lost)
				return false;

			if(this.GraphicsDevice.GraphicsDeviceStatus == GraphicsDeviceStatus.NotReset)
			{
				try
				{
					this.GraphicsDevice.Reset();
				}
				catch(DeviceLostException)
				{
					return false;
				}
			}
            
            return true;
		}

		public void ResetDevice()
		{
			if(DeviceResetting != null)
				DeviceResetting(this, EventArgs.Empty);

			this.GraphicsDevice.Reset();

			if(DeviceReset != null)
				DeviceReset(this, EventArgs.Empty);
		}

		public void Present()
		{
			this.GraphicsDevice.Present(null, new Rectangle(0, 0, this.host.Width, this.host.Height), this.host.Handle);
		}
	}
}
