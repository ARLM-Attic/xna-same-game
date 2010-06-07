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

		public int SelectedCount
		{
			get;
			private set;
		}

		public int SelectedValue
		{
			get;
			private set;
		}

		/// <summary>
		/// Triggered when the selection of blocks changes.
		/// </summary>
		public event EventHandler SelectedChanged;

		public Board(Game game)
		{
			this.game = game;

			this.blocks = new Block[Columns, Rows];

			for(int y = 0; y < Rows; y++)
				for(int x = 0; x < Columns; x++)
					this.blocks[x, y] = new Block(game);
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

			ScanStart(boardPosition);
		}

		private void ScanStart(Point boardPosition)
		{
			for(int y = 0; y < Rows; y++)
				for(int x = 0; x < Columns; x++)
					this.blocks[x, y].Selected = false;

			this.SelectedValue = 0;

			this.blocks[boardPosition.X, boardPosition.Y].Selected = true;
			this.SelectedCount = 1;

			Scan();

			if(this.SelectedCount > 1)
			{
				int multiplier = 1;

				for(int y = 0; y < Rows; y++)
					for(int x = 0; x < Columns; x++)
						if(this.blocks[x, y].Selected)
							multiplier *= (int)this.blocks[x, y].Multiplier;

				this.SelectedValue = ((int)Math.Pow((double)(this.SelectedCount - 2), 2.0) + 1) * multiplier;
			}
		}

		private void Scan()
		{
			int count = 0;

			for(int y = 0; y < Rows; y++)
			{
				for(int x = 0; x < Columns; x++)
				{
					if(this.blocks[x, y].Visible == true && this.blocks[x, y].Selected == false)
					{
						if(x > 0 && this.blocks[x - 1, y].Visible && this.blocks[x - 1, y].Selected && this.blocks[x - 1, y].Color == this.blocks[x, y].Color)
						{
							this.blocks[x, y].Selected = true;
							count++;
						}
						else if(x < Columns - 1 && this.blocks[x + 1, y].Visible && this.blocks[x + 1, y].Selected && this.blocks[x + 1, y].Color == this.blocks[x, y].Color)
						{
							this.blocks[x, y].Selected = true;
							count++;
						}
						else if(y > 0 && this.blocks[x, y - 1].Visible && this.blocks[x, y - 1].Selected && this.blocks[x, y - 1].Color == this.blocks[x, y].Color)
						{
							this.blocks[x, y].Selected = true;
							count++;
						}
						else if(y < Rows - 1 && this.blocks[x, y + 1].Visible && this.blocks[x, y + 1].Selected && this.blocks[x, y + 1].Color == this.blocks[x, y].Color)
						{
							this.blocks[x, y].Selected = true;
							count++;
						}
					}
				}
			}

			if(count > 0)
				Scan();

			this.SelectedCount += count;
		}

		/*
		public void RemoveSelected()
		{
			score += worth;

			for(int x = 0; x < BlockColumns; x++)
			{
				for(int y = 0; y < BlockRows; y++)
				{
					if(this.blocks[x, y].Selected == true)
					{
						this.blocks[x, y].Visible = false;
						this.blocks[x, y].Selected = false;
						this.blocksLeft--;
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
					if(this.blocks[x, y].Visible == false)
					{
						if(this.blocks[x, y - 1].Visible == true)
						{
							this.blocks[x, y].Visible = true;
							this.blocks[x, y].Color = this.blocks[x, y - 1].Color;
							this.blocks[x, y].Multiplier = this.blocks[x, y - 1].Multiplier;
							this.blocks[x, y].Y = this.blocks[x, y - 1].Y;
							this.blocks[x, y].Scale = this.blocks[x, y - 1].Scale;
							this.blocks[x, y - 1].Visible = false;
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
					if(this.blocks[x - 1, y].Visible == true)
					{
						empty = false;
						break;
					}
				}

				if(empty)
				{
					for(int y = 0; y < BlockRows; y++)
					{
						if(this.blocks[x, y].Visible == true)
						{
							this.blocks[x - 1, y].Visible = true;
							this.blocks[x - 1, y].Color = this.blocks[x, y].Color;
							this.blocks[x - 1, y].Multiplier = this.blocks[x, y].Multiplier;
							this.blocks[x - 1, y].Scale = this.blocks[x, y].Scale;
							this.blocks[x, y].Visible = false;

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
					if(blocks[x, y].Visible)
					{
						if(x > 0 && blocks[x - 1, y].Visible && blocks[x - 1, y].Color == blocks[x, y].Color)
							return false;
						else if(x < 24 && blocks[x + 1, y].Visible && blocks[x + 1, y].Color == blocks[x, y].Color)
							return false;
						else if(y > 0 && blocks[x, y - 1].Visible && blocks[x, y - 1].Color == blocks[x, y].Color)
							return false;
						else if(y < 14 && blocks[x, y + 1].Visible && blocks[x, y + 1].Color == blocks[x, y].Color)
							return false;
					}
				}
			}

			return true;
		}
		*/
	}
}
