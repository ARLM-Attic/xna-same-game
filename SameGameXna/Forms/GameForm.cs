using System;
using System.Drawing;
using System.Windows.Forms;

namespace SameGameXna.Forms
{
	public class GameForm : Form
	{
		Game game;
		GameControl gameControl;
		GameMenu gameMenu;
		GameStatusBar gameStatusBar;

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
			this.Size = new Size(800, 576);
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
						
			this.gameControl = new GameControl(this.game)
			{
				Dock = DockStyle.Fill
			};
			this.Controls.Add(gameControl);

			this.gameMenu = new GameMenu(this.game)
			{
				Dock = DockStyle.Top
			};
			this.Controls.Add(this.gameMenu);

			this.gameStatusBar = new GameStatusBar(this.game)
			{
				Dock = DockStyle.Bottom
			};			
			this.Controls.Add(this.gameStatusBar);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			int addedWidth = 800 - this.gameControl.Width;
			int addedHeight = 576 - this.gameControl.Height;

			this.Size = new Size(this.Width + addedWidth, this.Height + addedHeight);
		}
	}
}
