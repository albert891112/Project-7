using System.Text.Json;
using TestDataGenerator.Classes;

namespace TestDataGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //更該成需要的"型別"
            Category[] data;

            // 想要輸出的檔名
            string fileName = "Categories.json";

            
            data = Enumerable.Range(1, 10).Select(x => new Category //這裡要改成需要的型別
            {
                //這裡建立輸出的資料
                Id = x,
                Name = $"Category {x}"


            }).ToArray();

            OutputJsonFile(data, fileName);

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        private static void OutputJsonFile(Category[] data, string fileName)
        {
            string jsonString = JsonSerializer.Serialize(data);

            File.WriteAllText(fileName, jsonString);

            Console.WriteLine(File.ReadAllText(fileName));
        }
    }
}