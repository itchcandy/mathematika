using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileHandlingNamespace{
	[Serializable]
	public class Configuration{
		int HighScore;
		bool isSoundEnabled;

		public Configuration(){
			HighScore = 0;
			isSoundEnabled = true;
		}

		public bool getSoundStatus(){
			return isSoundEnabled;
		}

		public int getHighScore(){
			return HighScore;
		}

		public void toggleSound(){
			isSoundEnabled = !isSoundEnabled;
		}

		public bool isScoreHigher(int hs){
			return HighScore < hs;
		}

		public void updateHighScore(int hs){
			HighScore = hs;
		}
	}
		
	public class Settings{
		static string fileName = Application.persistentDataPath + "datafile";

		public static void InitConf(){
			Configuration c = new Configuration();
			using (Stream stream = File.Open(fileName, FileMode.Create))
			{
				BinaryFormatter b = new BinaryFormatter();
				b.Serialize(stream, c);
			}
		}

		public static Configuration ReadConf(){
			Configuration c = new Configuration();
			Stream stream;
			try
			{
				stream = File.Open(fileName, FileMode.Open);
			}
			catch(Exception e)
			{
				InitConf();
				stream = File.Open(fileName, FileMode.Open);

			}

			BinaryFormatter b = new BinaryFormatter();
			c = (Configuration)b.Deserialize(stream);
			stream.Close();
			return c;
		}

		public static void UpdateConf(Configuration c){
			using (Stream stream = File.Open(fileName, FileMode.Create))
			{
				BinaryFormatter b = new BinaryFormatter();
				b.Serialize(stream, c);
			}
		}

		public static void ResetConf(){
			if(File.Exists(fileName))
				File.Delete(fileName);
			InitConf();
		}
	}
}