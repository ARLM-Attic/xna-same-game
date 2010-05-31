using System;
using System.Windows.Forms;
using SameGameXna.Xna;

namespace SameGameXna.Forms
{
	public class GameControl : Control
	{
		Game game;

		public GraphicsDeviceService GraphicsDeviceService
		{
			get;
			private set;
		}

		public GameControl(Game game)
			: base()
		{
			this.game = game;

			this.GraphicsDeviceService = new GraphicsDeviceService(this.game);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.GraphicsDeviceService.CreateDevice(this, 800, 600);			
		}

		/// <summary>
		/// Overridden to do nothing.
		/// </summary>
		/// <param name="pevent"></param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if(this.GraphicsDeviceService.GraphicsDevice != null && this.GraphicsDeviceService.IsDeviceValid())
			{
				var graphics = this.GraphicsDeviceService.GraphicsDevice;

				graphics.Clear(Microsoft.Xna.Framework.Graphics.Color.CornflowerBlue);

				this.GraphicsDeviceService.Present();
			}
		}
	}
}
