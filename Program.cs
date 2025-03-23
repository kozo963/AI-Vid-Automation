using AI_Vid_Automation.Controller;
using AI_Vid_Automation.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Text;
internal class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter prompt");
            Console.WriteLine(DeepSeekController.GetAnswerAsync(Console.ReadLine()).Result);
            Console.WriteLine();
        }

    }
}