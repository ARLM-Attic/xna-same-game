using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;

namespace SameGameXna
{
	/// <summary>
	/// Simple class for keeping track of high scores
	/// </summary>
	public class HighScores
	{
		public const int MaxHighScores = 10;

		[Serializable]
		public struct HighScore
		{
			public string name;
			public UInt64 score;
		}

		List<HighScore> highScores;

		/// <summary>
		/// Returns the nth high score. 
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public HighScore this[int i]
		{
			get { return this.highScores[i]; }
		}

		/// <summary>
		/// Gets the number of high scores.
		/// </summary>
		public int Count
		{
			get { return MaxHighScores; }
		}

		public HighScores()
		{
			this.highScores = new List<HighScore>(10);

			for(int i = 0; i < MaxHighScores; i++)
			{
				HighScore highScore = new HighScore();

				highScore.name = "Player " + (i + 1);
				highScore.score = (UInt64)((MaxHighScores - i) * 100);

				this.highScores.Add(highScore);
			}
		}

		public static HighScores LoadOrCreate()
		{
			HighScores highScores = new HighScores();

			if(File.Exists("highscores.xml"))
			{
				try
				{
					XmlSerializer serializer = new XmlSerializer(typeof(List<HighScore>));
					TextReader reader = new StreamReader("highscores.xml");

					List<HighScore> highScoreList = (List<HighScore>)serializer.Deserialize(reader);
					reader.Close();

					highScores.highScores = highScoreList;
				}
				catch(InvalidOperationException e)
				{

				}
			}

			return highScores;
		}

		public void Save()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<HighScore>));
			TextWriter writer = new StreamWriter("highscores.xml");

			serializer.Serialize(writer, this.highScores);
			writer.Close();
		}

		/// <summary>
		/// Determines whether a score ranks in the high scores.
		/// </summary>
		/// <param name="score"></param>
		/// <returns></returns>
		public bool IsHighScore(UInt64 score)
		{
			for(int i = 0; i < MaxHighScores; i++)
			{
				if(score > this.highScores[i].score)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Adds a name and a score to the list of high scores.
		/// </summary>
		/// <param name="name">The name for the high score.</param>
		/// <param name="score">The score.</param>
		public void AddHighScore(string name, UInt64 score)
		{
			for(int i = 0; i < MaxHighScores; i++)
			{
				if(score > this.highScores[i].score)
				{
					HighScore highScore = new HighScore();
					highScore.name = name;
					highScore.score = score;

					this.highScores.Insert(i, highScore);

					this.highScores.RemoveAt(MaxHighScores);

					break;
				}
			}
		}

		/// <summary>
		/// Returns the rank of the score. (1st, 2nd, 3rd...)
		/// </summary>
		/// <param name="score"></param>
		/// <returns></returns>
		public string GetRank(UInt64 score)
		{
			int rank = 0;
			for(int i = 0; i < MaxHighScores; i++)
			{
				if(score > this.highScores[i].score)
				{
					rank = i + 1;
					break;
				}
			}

			switch(rank)
			{
				case 1: return "1st"; 
				case 2: return "2nd";
				case 3: return "3rd";
				case 4: return "4th";
				case 5: return "5th";
				case 6: return "6th";
				case 7: return "7th";
				case 8: return "8th";
				case 9: return "9th";
				case 10: return "10th";
			}

			return "";
		}
	}
}
