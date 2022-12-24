using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;

namespace PhylosophyAsker;

public class Task
{
    public string Quest { get; set; }
    public string[] Variants { get; set; }
    public int[] Ans { get; set; }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("to finish and look statistics type in 'stop' after answering a question");
        var questions = Parce();
        var results = FromBeginnig(questions);
        Console.WriteLine($"successful are {results.success} of {results.amount}");
        Console.WriteLine($"which is {results.success * 100 / (double)results.amount}%");
    }

    private static (int success, int amount) RandomOrder(List<Task> questions)
    {
        var success = 0;
        var amount = 0;
        var random = new Random();
        while (true)
        {
            var question = questions[random.Next(questions.Count)];
            amount++;
            var isRight = AskQuestion(question);
            if (isRight) { success++; }
            var input = Console.ReadLine();
            if (input == "stop" || input == "[stop")
            {
                break;
            }
        }
        return (success, amount);
    }

    private static (int success, int amount) FromBeginnig(List<Task> questions)
    {
        var success = 0;
        var amount = 0;
        foreach (var question in questions)
        {
            amount++;
            var isRight = AskQuestion(question);
            if (isRight) { success++; }
            var input = Console.ReadLine();
            if (input == "stop" || input == "[stop")
            {
                break;
            }
        }
        return (success, amount);
    }

    private static bool AskQuestion(Task question)
    {
        Console.WriteLine(question.Quest);
        for (int i = 0; i < question.Variants.Length; i++)
        {
            Console.WriteLine($"{i}: {question.Variants[i]}");
        }
        var userOutput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        bool isRight = false;
        if (userOutput.Length == question.Ans.Length)
        {
            isRight = true;
            for (int i = 0; i < userOutput.Length; i++)
            {
                if (question.Ans[i].ToString() != userOutput[i])
                {
                    isRight = false;
                }
            }
        }
        if (isRight)
        {
            Console.WriteLine("OK");
        }
        else
        {
            Console.WriteLine("NOPEnopeNOPEnopeNOPEnopeNOPEnopeNOPEnopeNOPEnope");
            foreach (var a in question.Ans)
            {
                Console.WriteLine(question.Variants[a]);
            }
            Console.WriteLine("this was the answer");
        }
        return isRight;
    }

    private static List<Task> Parce()
    {
        var file = new FileInfo(@"phylosopyAsker.txt");

        if (!file.Exists)
        {
            throw new Exception("where is the file?");
        }

        using StreamReader sr = file.OpenText();

        string text = sr.ReadToEnd();
        //Console.WriteLine(text);
        var pseudoQuestions = text.Split(new char[] { ']' });
        var Questions = new List<Task>();
        var splitters1 = new char[] { '\r', '\n' };
        var splitters2 = new char[] { '\r', '\n', ' ', '[', ']' };
        foreach (var pseudoQuestion in pseudoQuestions)
        {
            if (pseudoQuestion.Length > 10)
            {
                var lines = pseudoQuestion.Split(splitters1, StringSplitOptions.RemoveEmptyEntries);
                var vars = new List<string>();
                for (int i = 1; i < lines.Length - 1; i++)
                {
                    vars.Add(lines[i]);
                }
                var ans = new List<int>();
                foreach (var num in lines.Last().Split(splitters2, StringSplitOptions.RemoveEmptyEntries))
                {
                    ans.Add(int.Parse(num));
                }
                var t = new Task()
                {
                    Quest = lines[0],
                    Variants = vars.ToArray(),
                    Ans = ans.ToArray(),
                };

                Questions.Add(t);
            }
        }
        return Questions;
    }
}