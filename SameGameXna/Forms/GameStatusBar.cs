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

			this.game.Board.StatsUpdated += (s, e) => { UpdateText(); };

			this.Items.Add("Score: 0");
			this.Items.Add("Selected: 0");
			this.Items.Add("Value: 0");
			this.Items.Add("Remaining: " + this.game.Board.Remaining);
			this.Items.Add("[Single Remove Available]");
		}
				
		public void UpdateText()
		{
			this.Items[0].Text = "Score: " + this.game.Board.Score.ToString("#,0");
			this.Items[1].Text = "Selected: " + this.game.Board.SelectedCount.ToString();
			this.Items[2].Text = "Value: " + this.game.Board.SelectedValue.ToString("#,0");
			this.Items[3].Text = "Remaining: " + this.game.Board.Remaining.ToString();

			if(this.game.Board.SingleRemoveAvailable)
				this.Items[4].Text = "[Single Remove Available]";
			else
				this.Items[4].Text = "";
		}
	}
}
