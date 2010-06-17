using System;
using System.Windows.Forms;

namespace SameGameXna.Forms
{
	public class GameMenu : MenuStrip
	{
		Game game;

		/// <summary>
		/// Constructor.
		/// </summary>
		public GameMenu(Game game)
			: base()
		{
			this.game = game;

			var gameMenu = (ToolStripMenuItem)this.Items.Add("&Game");
			gameMenu.DropDownItems.Add("&New", null, (s, e) => { this.game.NewGame(); });
			gameMenu.DropDownItems.Add("&High Scores", null, (s, e) => { var form = new HighScoresForm(this.game); form.ShowDialog(this); });
			gameMenu.DropDownItems.Add("E&xit", null, (s, e) => { this.game.Exit(); });

			var settings = (ToolStripMenuItem)this.Items.Add("&Settings");
			
			var settingsAnimate = (ToolStripMenuItem)settings.DropDownItems.Add("&Animate Blocks", null, (s, e) => { this.game.Settings.Animate = !this.game.Settings.Animate; });
			settingsAnimate.Checked = this.game.Settings.Animate;
			this.game.Settings.AnimateChanged += (s, e) => { settingsAnimate.Checked = this.game.Settings.Animate; };
		}
	}
}
