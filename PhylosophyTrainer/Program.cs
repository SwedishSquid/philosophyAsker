using System;
using System.Text;

namespace Phylosophy;

public class Program
{
    public static void Main()
    {
        var file = new FileInfo(@"E:\ВУЗ\phylosopyAsker.txt");

        if (!file.Exists)
        {
            throw new Exception("where is the file?");
        }


        using StreamWriter sw = file.AppendText();

        while (true)
        {
            var sb = new StringBuilder();
            while (true)
            {
                var str = Console.ReadLine();
                if (str == null || str == string.Empty)
                {
                    continue;
                }



                sb.Append(str);
                sb.Append("\r\n");

                if (str.Contains('['))
                {
                    break;
                }
            }

            if (sb.ToString().StartsWith("[stop"))
            {
                break;
            }

            sb.Append(']');
            sw.WriteLine(sb.ToString());
        }
    }
}