using System;
using System.Security;
using Microsoft.SharePoint.Client;

namespace SharepointListRetriever
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SharePoint Online site URL:");
            string webSPOUrl = Console.ReadLine();
            Console.WriteLine("User Name:");
            string userName = Console.ReadLine();
            Console.WriteLine("Password:");
            SecureString password = FetchPasswordFromConsole();
            Services services = new Services(webSPOUrl,userName,password); 

            Console.ForegroundColor = ConsoleColor.White;
            var items = services.GetListData("SanTest Company");
            foreach (ListItem list in items)
            {
                Console.WriteLine("List title is: " + list["Title"].ToString());
            }
            Console.WriteLine("");
        }

        private static SecureString FetchPasswordFromConsole()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        var pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            var securePassword = new SecureString();
            //Convert string to secure string  
            foreach (char c in password)
                securePassword.AppendChar(c);
            securePassword.MakeReadOnly();
            return securePassword;
        }
    }
}
