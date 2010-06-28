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

		public static BlockMultiplier NextBlockMultipler(this Random random)
		{
			int i = random.Next(101);

			if(i % 20 == 0)
			{
				i = random.Next(3);

				switch(i)
				{
					case 0: return BlockMultiplier.x2;
					case 1: return BlockMultiplier.x3;
					case 2: return BlockMultiplier.x5;
				}
			}

			return BlockMultiplier.x1;
		}
	}
}
