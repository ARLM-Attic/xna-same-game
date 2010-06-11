using System;
using System.Windows.Forms;
using SameGameXna.Xna;

namespace SameGameXna.Forms
{
	public class GameControl : Control
	{
		Game game;
				
		public GameControl(Game game)
			: base()
		{
			this.game = game;
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.game.GraphicsDeviceService.CreateDevice(this, 800, 576);
			this.game.Initialize();
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
			if(this.game.GraphicsDeviceService.GraphicsDevice != null && this.game.GraphicsDeviceService.IsDeviceValid())
				this.game.Draw();
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);

			if(e.Button == MouseButtons.Left)
				this.game.Board.LeftClick(new Microsoft.Xna.Framework.Point(e.Location.X, e.Location.Y));
		}

		protected override void  OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			if(e.Button == MouseButtons.Left)
				this.game.Board.DoubleLeftClick(new Microsoft.Xna.Framework.Point(e.Location.X, e.Location.Y));
		}
	}
}
