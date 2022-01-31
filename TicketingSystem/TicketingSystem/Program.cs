using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace TicketingSystem
{
    public class TicketingSystem
    {
        public static void Main(string[] args)
        {
            string option = "";
            
            do
            {
                Console.WriteLine("Options \n (A) to display Tickets \n (B) to add a new ticket \n (X) to Exit");
                option = Console.ReadLine();

                if (option.ToUpper() == "A")
                {
                    string[] lines = ReadFromFile();

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (i != 0)
                        {
                            if (lines[i] != null)
                            {
                                string[] line = lines[i].Split(",");
                                
                                for (int j = 0; j < line.Length; j++)
                                {
                                    if (line[j].Contains('|'))
                                    {
                                        string[] watching = line[j].Split("|");

                                        for (int k = 0; k < watching.Length; k++)
                                        {
                                            Console.Write(watching[k] + ", ");
                                        }

                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        Console.Write(line[j] + " ");
                                    }
                                }
                            }
                            
                        }
                    }
                
                }
                else if (option.ToUpper() == "B")
                {
                    WriteToFile();
                }
                else if(option.ToUpper() != "X")
                {
                    Console.WriteLine("Invalid Command, please try again");
                }
            } while (option.ToUpper() != "X");
            
            
        }
        
        
        //Method for Reading from the files
        public static string[] ReadFromFile()
        {
            
            if (File.Exists("tickets.csv"))
            {
                StreamReader reader = new StreamReader("tickets.csv");
                
                var file = new List<string>();
                string line = "";
            
                while (line != null)
                {
                    line = reader.ReadLine();
                    file.Add(line);
                }
                reader.Close();
                string[] lines = file.ToArray();

                return lines;
            }
            else
            {
                return new string[1];
            }
            
        }

        //Method for Writing information to the file
        
        public static void WriteToFile()
        {
            Console.WriteLine("Enter the ticket ID");
            string ticketID = Console.ReadLine();
            
            Console.WriteLine("Enter in a summary");
            string summary = Console.ReadLine();

            Console.WriteLine("Enter the status");
            string status = Console.ReadLine();

            Console.WriteLine("Enter the priority");
            string priority = Console.ReadLine();

            Console.WriteLine("Enter the submitters name");
            string submitter = Console.ReadLine();

            Console.WriteLine("Who will this ticket be assigned to?");
            string assignedTo = Console.ReadLine();

            int i = 0;
            
            ArrayList watchers = new ArrayList();
            string watcher;
            StringBuilder watchersLine = new StringBuilder();

            do
            {
                Console.WriteLine("Enter the people you are watching this issue, then type (D) when you are done");
                 watcher = Console.ReadLine();

                if (watcher.ToUpper() != "D")
                {
                    watchers.Add(watcher);
                }

                i++;
            } while (watcher.ToUpper() != "D");

            
            for (int j = 0; j < watchers.Count; j++)
            {
                if (j != watchers.Count - 1)
                {
                    watchersLine.Append(watchers[j] + "|");
                }
                else
                {
                    watchersLine.Append(watchers[j]);
                }
                
                
            }

            var file = new List<string>();
            file = ReadFromFile().ToList();
            
            file[0] = "TicketID,Summary,Status,Priority,Submitter,Assigned,Watching";
                
            StreamWriter writer = new StreamWriter("tickets.csv");
            
            file.Add(ticketID + ", " + summary + ", " + status + ", " + priority + ", " + submitter + ", " + assignedTo + ", " + watchersLine);
            
            for (int j = 0; j < file.Count; j++)
            {
                writer.WriteLine(file[j]);
            }
            writer.Close();

        }
        
    }
    
}