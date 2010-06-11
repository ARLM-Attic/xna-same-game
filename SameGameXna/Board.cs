using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SameGameXna
{
	public class Board
	{
		const int width = 800;
		const int height = 576;

		public const int Columns = 25;
		public const int Rows = 18;
		public const int TotalBlocks = Columns * Rows;
		
		public const float TotalRemoveAnimationDuration = 0.2f;

		Game game;
		Block[,] blocks;
		Texture2D blockTexture;

		bool removeAnimationInProgress;
		TimeSpan removeAnimationDuation;

		public int Width
		{
			get { return width; }
		}

		public int Height
		{
			get { return height; }
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

		public int Score
		{
			get;
			private set;
		}

		public int Remaining
		{
			get;
			private set;
		}

		/// <summary>
		/// Triggered when the selection of blocks changes.
		/// </summary>
		public event EventHandler StatsUpdated;

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

			this.SelectedCount = 0;
			this.SelectedValue = 0;
			this.Score = 0;
			this.Remaining = TotalBlocks;

			if(this.StatsUpdated != null)
				this.StatsUpdated(this, EventArgs.Empty);
		}

		public void Update(TimeSpan elapsed)
		{
			if(this.removeAnimationInProgress)
				this.removeAnimationDuation += elapsed;

			for(int y = 0; y < Rows; y++)
			{
				for(int x = 0; x < Columns; x++)
				{
					if(this.removeAnimationInProgress && this.blocks[x, y].Selected)
						this.blocks[x, y].Scale = 1.0f - ((float)this.removeAnimationDuation.TotalSeconds / TotalRemoveAnimationDuration);

					this.blocks[x, y].Update(elapsed);
				}
			}

			if(this.removeAnimationInProgress && this.removeAnimationDuation.TotalSeconds >= TotalRemoveAnimationDuration)
			{
				RemoveSelected();
				this.removeAnimationInProgress = false;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for(int y = Rows - 1; y >= 0; y--)
				for(int x = Columns - 1; x >= 0; x--)
					if(this.blocks[x, y].Visible)
						this.blocks[x, y].Draw(spriteBatch, this.blockTexture);
		}

		public void LeftClick(Point point)
		{
			var boardPosition = new Point(point.X / Block.Width, point.Y / Block.Height);
			ScanStart(boardPosition);
		}

		public void DoubleLeftClick(Point point)
		{
			var boardPosition = new Point(point.X / Block.Width, point.Y / Block.Height);
			BeginRemoveAnimation();
		}

		private void ScanStart(Point boardPosition)
		{
			for(int y = 0; y < Rows; y++)
				for(int x = 0; x < Columns; x++)
					this.blocks[x, y].Selected = false;

			this.SelectedValue = 0;

			if(this.blocks[boardPosition.X, boardPosition.Y].Visible)
			{
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
			else
			{
				this.SelectedCount = 0;
			}

			if(this.StatsUpdated != null)
				this.StatsUpdated(this, EventArgs.Empty);
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

		private void BeginRemoveAnimation()
		{
			this.removeAnimationInProgress = true;
			this.removeAnimationDuation = TimeSpan.Zero;
		}

		private void RemoveSelected()
		{
			this.Score += this.SelectedValue;

			for(int y = 0; y < Rows; y++)
			{
				for(int x = 0; x < Columns; x++)
				{
					if(this.blocks[x, y].Selected == true)
					{
						this.blocks[x, y].Visible = false;
						this.blocks[x, y].Selected = false;
						this.Remaining--;
					}
				}
			}

			MoveBlocksDown();

			this.SelectedCount = 0;
			this.SelectedValue = 0;

			if(this.StatsUpdated != null)
				this.StatsUpdated(this, EventArgs.Empty);
		}

		private void MoveBlocksDown()
		{
			int moved = 0;

			for(int y = 1; y < Rows; y++)
			{
				for(int x = 0; x < Columns; x++)
				{
					if(!this.blocks[x, y].Visible )
					{
						if(this.blocks[x, y - 1].Visible)
						{
							this.blocks[x, y].Visible = true;
							this.blocks[x, y].Color = this.blocks[x, y - 1].Color;
							this.blocks[x, y].Multiplier = this.blocks[x, y - 1].Multiplier;
							this.blocks[x, y].Position = this.blocks[x, y - 1].Position;
							this.blocks[x, y].Scale = 1.0f;
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

			for(int x = Columns - 1; x > 0; x--)
			{
				bool empty = true;
				for(int y = 0; y < Rows; y++)
				{
					if(this.blocks[x - 1, y].Visible == true)
					{
						empty = false;
						break;
					}
				}

				if(empty)
				{
					for(int y = 0; y < Rows; y++)
					{
						if(this.blocks[x, y].Visible == true)
						{
							this.blocks[x - 1, y].Visible = true;
							this.blocks[x - 1, y].Color = this.blocks[x, y].Color;
							this.blocks[x - 1, y].Multiplier = this.blocks[x, y].Multiplier;
							this.blocks[x - 1, y].Position = this.blocks[x, y].Position;
							this.blocks[x - 1, y].Scale = 1.0f;
							this.blocks[x, y].Visible = false;
														
							moved++;
						}
					}
				}
			}

			if(moved > 0)
				MoveBlocksLeft();
		}

		public bool HasGameEnded()
		{
			return false;

			/*
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
			*/
		}
	}
}
