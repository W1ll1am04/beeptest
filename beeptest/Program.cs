using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace beeptest
{
    class Program
    {
        static void Main(string[] args)
        {
            restartcode:
            try
            {
                string jg = "settings.json";
                bool enable_sound = true;
                string message = null;

                Console.Title = "beeptest";

                if (!File.Exists(jg))
                {
                    string defaultSettings = "{\n   'enable_sound': true,\n   'message': null \n}";

                    StreamWriter stw = new StreamWriter("settings.json");
                    using (stw)
                    {
                        stw.Write(defaultSettings);
                        stw.Close();
                    }
                }
                if (File.Exists(jg))
                {
                    StreamReader strr = new StreamReader("settings.json");
                    JObject settings;
                    using (strr)
                    {
                        settings = JObject.Parse(strr.ReadToEnd());
                    }
                    enable_sound = (bool)settings["enable_sound"];
                    message = (string)settings["message"];
                    if (message == null || message == "") { message = "Do you feel it now mr crabs"; }
                    else { /* all fine here. */ }
                    if (enable_sound == true || enable_sound == false) { /* all fine here.*/ }
                    else { enable_sound = true; }
                }

                while (true)
                {
                    Random rnd = new Random();
                    int rph = rnd.Next(37, 700);
                    int rpw = rnd.Next(37, 700);

                    int rh = rnd.Next(1, 150);
                    int rw = rnd.Next(1, 300);
                    int sides = rnd.Next(1, rw);
                    int top = rnd.Next(1, rh);

                    Console.CursorVisible = false;

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(sides,top);
                    Console.WriteLine(message);
                    if (enable_sound == true)
                        Console.Beep(rph, rpw);
                    else
                    {
                        int selection = rnd.Next(0,3);
                        int sleep = 400;
                        switch (selection)
                        {
                            case 0:
                                sleep = 400;
                                break;
                            case 1:
                                sleep = 410;
                                break;
                            case 2:
                                sleep = 420;
                                break;
                            case 3:
                                sleep = 430;
                                break;
                            default:
                                sleep = 440;
                                break;
                        }
                        Thread.Sleep(sleep);
                    }
                }
            }
            catch (ArgumentOutOfRangeException){ goto restartcode; }
            catch (FormatException ex) { Console.WriteLine("An error occured while parsing settings.json: \n\n" + ex.Message+" \nType: " + ex.GetType()); Console.ReadKey(); }
        }
    }
}
