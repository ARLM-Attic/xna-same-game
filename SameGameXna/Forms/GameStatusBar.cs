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
		}
	}
}
