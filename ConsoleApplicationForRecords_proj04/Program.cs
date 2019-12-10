/* Visual Programming
 * Professor C. Boyd
 * Written by Joshua Yang
 * Student ID: 2355145
 * Section 11 - 11:45 am
 * Date 10/12/2019
 * CPSC-236
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplicationForRecords_proj04
{
    class Program
    {


        static void Main(string[] args)
        {

            // Display commands and infor for user
            Console.WriteLine("************ CPSC 236 Record Holder ************");
            Console.WriteLine("************ Written by Joshua Y. ************");
            Console.WriteLine("************ COMMANDS AVAILABLE ************");
            Console.WriteLine("");
            Console.WriteLine("     - Insert/Add/New: Creating a new Record.");
            Console.WriteLine("     - List/Records: View current Record.");
            Console.WriteLine("     - Delete/Remove: Delete a record.");
            Console.WriteLine("     - Exit/Kill/Save: Save and Quit.");
            Console.WriteLine("Please type in a command");
            Console.WriteLine("Example: Add or Delete");
            
            

            Console.WriteLine("");
            Console.WriteLine("");
            // Create a List with type Record to store all the loaded records from file and new records.
            // Then, at the end, all the records inside this list will be saved into the file
            List<Record> RecordsList = new List<Record>();

            Console.WriteLine("Preview of previous record from file records.txt:");
            if (File.Exists("records.txt")) // Check if file exists to load records
            {
                using (StreamReader reader = new StreamReader("records.txt"))
                {
                    string line;
                    int k = 0;
                    while((line = reader.ReadLine()) != null)
                    {
                        Record rec = new Record(k, line); // Create a new Record with data from file
                        RecordsList.Add(rec); // add to the list
                        k += 1;
                    }

                    reader.Close();

                }
            }

            if (RecordsList.Count > 0)
            {
                foreach (Record rec in RecordsList)
                {
                    Console.WriteLine(" - " + rec.key + ": " + rec.value);
                }
            }
            else
                Console.WriteLine(" - No old records found");

            string input;
            while ((input = Console.ReadLine()) != null) // Load input from console
            {
                if (input == "Delete" || input == "Remove" || input == "delete" || input == "remove")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Please insert the index you want to delete: ");

                    int idx;
                    if (Int32.TryParse(Console.ReadLine(), out idx) == true) // If input is int
                    {
                        bool deleted = false;
                        foreach (Record rec in RecordsList)
                        {
                            if (rec.key == idx) // if there exists a record with that index then delete
                            {
                                RecordsList.Remove(rec);
                                Console.WriteLine("Record with index: " + rec.key + " and value: " + rec.value + " | Deleted");
                                deleted = true;
                                break;
                            }
                        }
                        if (!deleted) // no record with that index found
                            Console.WriteLine("There is no record with that index.");

                        if (deleted)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Current records are:");
                            foreach (Record rec in RecordsList)
                                Console.WriteLine(" - " + rec.key + ": " + rec.value);
                        }
                    }
                }
                else if (input == "Insert" || input == "Add" || input == "New" || input == "insert" || input == "add" || input == "new") // create new record
                {
                    Console.WriteLine("Please write a new record:");

                    string recTxt = Console.ReadLine(); // read input

                    int k = 0;
                    foreach (Record rec in RecordsList)
                    {
                        if (rec.key >= k)
                            k = rec.key + 1; // the key of the new record is +1 than the last index
                    }

                    Record newRec = new Record(k, recTxt); // create new record
                    RecordsList.Add(newRec); // add to list

                    // Display info for user
                    Console.WriteLine("");
                    Console.WriteLine("New Record added: " + recTxt);
                    Console.WriteLine("");
                    Console.WriteLine("The current records to be saved are:");
                    foreach (Record rec in RecordsList)
                    {
                        Console.WriteLine(" - " + rec.key + ": " + rec.value);
                    }
                    Console.WriteLine("");

                }
                else if (input == "Exit" || input == "Kill" || input == "Save" || input == "exit" || input == "kill" || input == "save") // Exit app
                {
                    //When exiting, save all records
                    StreamWriter writer = new StreamWriter("records.txt");
                    foreach (Record rec in RecordsList)
                    {
                        writer.WriteLine(rec.value);
                    }
                    writer.Close();
                    Console.WriteLine("Record Saved:");
                    foreach (Record rec in RecordsList)
                    {
                        Console.WriteLine(" - " + rec.key + ": " + rec.value);
                    }
                    Console.WriteLine("");
                    Environment.Exit(0);
                }
                else if (input == "List" || input == "Records" || input == "list" || input == "records")
                {
                    Console.WriteLine("");
                    Console.WriteLine("The current records saved are:");
                    foreach (Record rec in RecordsList)
                    {
                        Console.WriteLine(" - " + rec.key + ": " + rec.value);
                    }
                    Console.WriteLine("");
                }
                else
                    Console.WriteLine("Invalid Input, please try again!");
            }
            Console.ReadLine();
        }

    }
}
