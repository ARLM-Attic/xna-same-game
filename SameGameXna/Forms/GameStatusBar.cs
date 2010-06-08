using System;
using System.Windows.Forms;

namespace SameGameXna.Forms
{
	public class GameStatusBar : StatusStrip
	{
		Game game;

		public GameStatusBar(Game game)
			: base()
		{
			this.game = game;

			this.SizingGrip = false;

			this.game.Board.SelectedChanged += (s, e) => { UpdateText(); };
		}
				
		public void UpdateText()
		{
			this.Items.Clear();
			this.Items.Add("Selected: " + this.game.Board.SelectedCount.ToString());
		}
	}
}
