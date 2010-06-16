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

			var file = (ToolStripMenuItem)this.Items.Add("&File");
			file.DropDownItems.Add("&New Game", null, (s, e) => { this.game.NewGame(); });
			file.DropDownItems.Add("E&xit", null, (s, e) => { this.game.Exit(); });

			var settings = (ToolStripMenuItem)this.Items.Add("&Settings");
			
			var settingsAnimate = (ToolStripMenuItem)settings.DropDownItems.Add("&Animate Blocks", null, (s, e) => { this.game.Settings.Animate = !this.game.Settings.Animate; });
			settingsAnimate.Checked = this.game.Settings.Animate;
			this.game.Settings.AnimateChanged += (s, e) => { settingsAnimate.Checked = this.game.Settings.Animate; };
		}
	}
}
