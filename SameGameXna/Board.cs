using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SameGameXna
{
	public class Board
	{
		public const int Columns = 25;
		public const int Rows = 18;
		
		Game game;
		Block[,] blocks;
		Texture2D blockTexture;

		public int Width
		{
			get { return 800; }
		}

		public int Height
		{
			get { return 576; }
		}

		public Board(Game game)
		{
			this.game = game;

			this.blocks = new Block[Columns, Rows];

			for(int y = 0; y < Rows; y++)
				for(int x = 0; x < Columns; x++)
					this.blocks[x, y] = new Block(game, this);
		}

		public void Initialize()
		{
			if(this.blockTexture == null)
				this.blockTexture = Texture2D.FromFile(this.game.GraphicsDevice, @"Resources\block.png");

			for(int y = 0; y < Rows; y++)
				for(int x = 0; x < Columns; x++)
					this.blocks[x, y].Initialize(new Point(x, y));
		}

		public void Update(TimeSpan elapsed)
		{
			for(int y = 0; y < Rows; y++)
				for(int x = 0; x < Columns; x++)
					this.blocks[x, y].Update(elapsed);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for(int y = Rows - 1; y >= 0; y--)
				for(int x = Columns - 1; x >= 0; x--)
					this.blocks[x, y].Draw(spriteBatch, this.blockTexture);
		}

		public void LeftClick(Point point)
		{
			var boardPosition = new Point(point.X / Block.Width, point.Y / Block.Height);
		}

		/*
		public void ScanBoardStart(Point point)
		{
			worth = 0;

			blocks[(int)Math.Floor((float)(point.X / BlockWidth))][(int)Math.Floor((float)(point.Y / BlockHeight))].Selected = true;
			selected = 1;

			ScanBoard();

			if(selected > 1)
			{
				int multiplier = 1;
				for(int x = 0; x < BlockColumns; x++)
					for(int y = 0; y < BlockRows; y++)
						if(blocks[x][y].Selected)
							multiplier *= blocks[x][y].Multiplier;

				worth = ((int)Math.Pow((double)(selected - 2), 2.0) + 1) * multiplier;
			}
		}

		private void ScanBoard()
		{
			int count = 0;

			for(int x = 0; x < BlockColumns; x++)
			{
				for(int y = 0; y < BlockRows; y++)
				{
					if(blocks[x][y].Visible == true && blocks[x][y].Selected == false)
					{
						if(x > 0 && blocks[x - 1][y].Visible && blocks[x - 1][y].Selected && blocks[x - 1][y].BlockColor == blocks[x][y].BlockColor)
						{
							blocks[x][y].Selected = true;
							count++;
						}
						else if(x < 24 && blocks[x + 1][y].Visible && blocks[x + 1][y].Selected && blocks[x + 1][y].BlockColor == blocks[x][y].BlockColor)
						{
							blocks[x][y].Selected = true;
							count++;
						}
						else if(y > 0 && blocks[x][y - 1].Visible && blocks[x][y - 1].Selected && blocks[x][y - 1].BlockColor == blocks[x][y].BlockColor)
						{
							blocks[x][y].Selected = true;
							count++;
						}
						else if(y < 14 && blocks[x][y + 1].Visible && blocks[x][y + 1].Selected && blocks[x][y + 1].BlockColor == blocks[x][y].BlockColor)
						{
							blocks[x][y].Selected = true;
							count++;
						}
					}
				}
			}

			if(count > 0)
				ScanBoard();

			selected += count;
		}

		public void RemoveSelected()
		{
			score += worth;

			for(int x = 0; x < BlockColumns; x++)
			{
				for(int y = 0; y < BlockRows; y++)
				{
					if(blocks[x][y].Selected == true)
					{
						blocks[x][y].Visible = false;
						blocks[x][y].Selected = false;
						blocksLeft--;
					}
				}
			}

			MoveBlocksDown();
		}

		private void MoveBlocksDown()
		{
			int moved = 0;

			for(int y = 1; y < BlockRows; y++)
			{
				for(int x = 0; x < BlockColumns; x++)
				{
					if(blocks[x][y].Visible == false)
					{
						if(blocks[x][y - 1].Visible == true)
						{
							blocks[x][y].Visible = true;
							blocks[x][y].Color = blocks[x][y - 1].Color;
							blocks[x][y].Multiplier = blocks[x][y - 1].Multiplier;
							blocks[x][y].Y = blocks[x][y - 1].Y;
							blocks[x][y].Scale = blocks[x][y - 1].Scale;
							blocks[x][y - 1].Visible = false;
							moved++;
						}
					}
				}
			}

			if(moved > 0)
				MoveBlocksDown();
			else
				MoveBlocksLeft();
		}

		private void MoveBlocksLeft()
		{
			int moved = 0;

			for(int x = BlockColumns - 1; x > 0; x--)
			{
				bool empty = true;
				for(int y = 0; y < 15; y++)
				{
					if(blocks[x - 1][y].Visible == true)
					{
						empty = false;
						break;
					}
				}

				if(empty)
				{
					for(int y = 0; y < BlockRows; y++)
					{
						if(blocks[x][y].Visible == true)
						{
							blocks[x - 1][y].Visible = true;
							blocks[x - 1][y].Color = blocks[x][y].Color;
							blocks[x - 1][y].Multiplier = blocks[x][y].Multiplier;
							blocks[x - 1][y].Scale = blocks[x][y].Scale;
							blocks[x][y].Visible = false;

							moved++;
						}
					}
				}
			}

			if(moved > 0)
				MoveBlocksLeft();
		}

		public bool IsGameOver()
		{
			if(blocksLeft == 0)
				return true;

			if(singleRemoves > 0)
				return false;

			for(int x = 0; x < BlockColumns; x++)
			{
				for(int y = 0; y < BlockRows; y++)
				{
					if(blocks[x][y].Visible)
					{
						if(x > 0 && blocks[x - 1][y].Visible && blocks[x - 1][y].BlockColor == blocks[x][y].BlockColor)
							return false;
						else if(x < 24 && blocks[x + 1][y].Visible && blocks[x + 1][y].BlockColor == blocks[x][y].BlockColor)
							return false;
						else if(y > 0 && blocks[x][y - 1].Visible && blocks[x][y - 1].BlockColor == blocks[x][y].BlockColor)
							return false;
						else if(y < 14 && blocks[x][y + 1].Visible && blocks[x][y + 1].BlockColor == blocks[x][y].BlockColor)
							return false;
					}
				}
			}

			return true;
		}
		*/
	}
}
