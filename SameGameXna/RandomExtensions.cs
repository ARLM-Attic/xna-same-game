using System;

namespace SameGameXna
{
	public static class RandomExtensions
	{
		public static BlockColor NextBlockColor(this Random random)
		{
			int i = random.Next(4);

			switch(i)
			{
				case 0: return BlockColor.Red;
				case 1: return BlockColor.Green;
				case 2: return BlockColor.Yellow;
				case 3: return BlockColor.Blue;
			}

			return BlockColor.Red;
		}
	}
}
