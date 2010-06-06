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
		}
	}
}
