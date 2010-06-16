using System;
using System.IO;
using System.Xml.Serialization;

namespace SameGameXna
{
	public class GameSettings
	{
		bool animate;

		public bool Animate
		{
			get { return this.animate; }

			set
			{
				if(value != this.animate)
				{
					this.animate = value;

					if(this.AnimateChanged != null)
						this.AnimateChanged(this, EventArgs.Empty);
				}
			}
		}

		public event EventHandler AnimateChanged;

		public GameSettings()
		{
			this.animate = true;
		}

		/// <summary>
		/// Loads from a file or creates a new instance.
		/// </summary>
		/// <returns></returns>
		public static GameSettings Load()
		{
			if(File.Exists("settings.xml"))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(GameSettings));
				TextReader reader = new StreamReader("settings.xml");

				GameSettings settings = (GameSettings)serializer.Deserialize(reader);
				reader.Close();

				return settings;
			}

			return new GameSettings();
		}

		/// <summary>
		/// Serializes the class to an xml file.
		/// </summary>
		public void Save()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(GameSettings));
			TextWriter writer = new StreamWriter("settings.xml");

			serializer.Serialize(writer, this);
			writer.Close();
		}
	}
}
