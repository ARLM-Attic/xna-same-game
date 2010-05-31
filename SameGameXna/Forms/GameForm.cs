using System;
using System.Drawing;
using System.Windows.Forms;

namespace SameGameXna.Forms
{
	public class GameForm : Form
	{
		Game game;
		GameControl gameControl;

		public override Size MinimumSize
		{
			get { return new Size(800, 600); }
			set { }
		}
				
		public GameForm(Game game)
			: base()
		{
			this.game = game;

			this.Text = "Same Game Xna";
			this.Size = new Size(800, 600);
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;

			this.gameControl = new GameControl(this.game)
			{
				Dock = DockStyle.Fill
			};
			this.Controls.Add(gameControl);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			int addedWidth = 800 - this.gameControl.Width;
			int addedHeight = 600 - this.gameControl.Height;

			this.Size = new Size(this.Width + addedWidth, this.Height + addedHeight);
		}
	}
}
