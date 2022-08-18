using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
	public class FileReader
	{
		private static FileReader instance;
		public static string filename = "input.txt";

		public static FileReader getInstance()
		{
			if (instance == null)
			{
				instance = new FileReader();
			}
			return instance;
		}

		private FileReader() { }

		public void ReadKnapsack()
		{

			StreamReader reader = null;
			if (!File.Exists(filename))
				File.Create(filename);
			reader = new StreamReader(filename);
            Console.WriteLine("Opened file with name " + filename);
			try {

				string sCurrentLine;

				sCurrentLine = reader.ReadLine();

				Knapsack.MAX_WEIGHT = Convert.ToDouble(sCurrentLine.Split(" ")[0]);
			
		} finally {
			if (reader!= null) reader.Close();
		}
	}

		public List<Item> ReadItems()
		{ 
		StreamReader br = null;
		string filePath = filename;
		try
		{
			List<Item> result = new List<Item>();
			string sCurrentLine;
			br = new StreamReader(filePath);
			List<string> lines = new List<string>();
		
			while ((sCurrentLine = br.ReadLine()) != null)
			{
				lines.Add(sCurrentLine);
			}
		
			for (int i = 1; i < lines.Count; i++)
			{
				string[] item = lines[i].Split(" ");
				string name = item[0];
				double weight = Convert.ToDouble(item[1]);
				double price = Convert.ToDouble(item[2]);
		
				result.Add(new QuantityItem(name, weight, price));
			}
			return result;
		}
		finally
		{
			if (br != null) br.Close();
		}
			
		}
	}
}
		