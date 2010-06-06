using System;
using Microsoft.Xna.Framework.Graphics;

namespace SameGameXna
{
	public enum BlockColor
	{
		Red,
		Green,
		Yellow,
		Blue
	}

	public static class BlockColorExtensions
	{
		public static Color ToXnaColor(this BlockColor blockColor)
		{
			switch(blockColor)
			{
				case BlockColor.Red: return Color.Red;
				case BlockColor.Blue: return Color.Blue;
				case BlockColor.Yellow: return Color.Yellow;
				case BlockColor.Green: return Color.Green;
			}

			return Color.White;
		}
	}
}
