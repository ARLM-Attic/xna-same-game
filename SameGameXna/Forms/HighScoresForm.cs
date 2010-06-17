using System;
using System.Windows.Forms;

namespace SameGameXna.Forms
{
	/// <summary>
	/// Form for displaying high scores.
	/// </summary>
	public class HighScoresForm : Form
	{
		Game game;

		public HighScoresForm(Game game)
			: base()
		{
			this.game = game;

			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Height = 280;
			this.MinimizeBox = false;
			this.MaximizeBox = false;
			this.Text = "High Scores";

			var panel = new TableLayoutPanel()
			{
				CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset,
				ColumnCount = 2,
				Dock = DockStyle.Fill,
				RowCount = this.game.HighScores.Count
			};
			this.Controls.Add(panel);

			for(int i = 0; i < this.game.HighScores.Count; i++)
			{
				var highScore = this.game.HighScores[i];
				panel.Controls.Add(new Label() { Text = highScore.name }, 0, i);
				panel.Controls.Add(new Label() { Text = highScore.score.ToString("#,0") }, 1, i);
			}
		}
	}
}
