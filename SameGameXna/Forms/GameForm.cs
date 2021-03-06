﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace SameGameXna.Forms
{
	public class GameForm : Form, IGameWindow
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

		public event EventHandler Idle;
				
		public GameForm(Game game)
			: base()
		{
			this.game = game;

			this.Icon = SameGameXna.Properties.Resources.Icon;
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

		public void Run()
		{
			Application.EnableVisualStyles();
			Application.Idle += Application_Idle;
			Application.Run(this);
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			if(this.Idle != null)
				this.Idle(this, EventArgs.Empty);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			int addedWidth = 800 - this.gameControl.Width;
			int addedHeight = 576 - this.gameControl.Height;

			this.Size = new Size(this.Width + addedWidth, this.Height + addedHeight);
		}

		public void ShowMessage(GameMessages message, params object[] args)
		{
			string messageText = "";

			switch(message)
			{
				case GameMessages.AtLeast2BlocksMustBeSelectedToRemove:
					messageText = "At least 2 blocks must be selected to remove.";
					break;

				case GameMessages.Only1BlockMustBeSelectedToSingleRemove:
					messageText = "Only 1 block must be selected for single remove.";
					break;

				case GameMessages.SingleRemoveNotAvailable:
					messageText = "No more single removes available.";
					break;

				case GameMessages.GameOver:
					UInt64 score = (UInt64)args[0];
					messageText = "Game Over\nFinal Score: " + score.ToString("#,0");
					break;
			}

			MessageBox.Show(this, messageText, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public string CollectHighScoreName(int rank)
		{
			var newHighScoreForm = new NewHighScoreForm(rank);
			newHighScoreForm.ShowDialog(this);

			return newHighScoreForm.EnteredName;
		}
	}
}
