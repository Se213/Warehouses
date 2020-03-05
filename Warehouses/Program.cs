//Author: Sean Epley
using System.IO;
using System;
using System.Collections.Generic; //imports

namespace WareHouses
{

    public class Warehouse //required Warehouse class to make Warehouse objects
    {
        public String Name;
        public int Part_102 { get; set; }
        public int Part_215 { get; set; }
        public int Part_410 { get; set; }
        public int Part_525 { get; set; }
        public int Part_711 { get; set; }
        public Warehouse(String name)
        {
            Name = name;
        }
    }
    class Inventory
    {
        static void Main(string[] args)
        {
            List<Warehouse> locations = new List<Warehouse>(); //create a list to hold warehouse objects
            locations.Add(new Warehouse("Atlanta")); //add each new warehouse object by name to the list after it's created
            locations.Add(new Warehouse("Baltimore"));
            locations.Add(new Warehouse("Chicago"));
            locations.Add(new Warehouse("Denver"));
            locations.Add(new Warehouse("Ely"));
            locations.Add(new Warehouse("Fargo"));



            Console.WriteLine("\n \nStart of day stock... \n "); // print start stock
            string[] Invents = File.ReadAllLines("Inventory.txt"); //make a list of every line in the inventory.txt file
            for (int i = 0; i < locations.Count; i++) // loop once for each warehouse
            {
                string[] itemQty = Invents[i].Split(','); // create a list of strings split by commas
                int Part_102 = int.Parse(itemQty[0].Trim(' ')); // assign each part with the corresponding number after trimming whitespace
                int Part_215 = int.Parse(itemQty[1].Trim(' '));
                int Part_410 = int.Parse(itemQty[2].Trim(' '));
                int Part_525 = int.Parse(itemQty[3].Trim(' '));
                int Part_711 = int.Parse(itemQty[4].Trim(' '));
                locations[i].Part_102 = Part_102; // update each warehouse object to reflect the inventory on the text file
                locations[i].Part_215 = Part_215;
                locations[i].Part_410 = Part_410;
                locations[i].Part_525 = Part_525;
                locations[i].Part_711 = Part_711;
                Console.WriteLine("Warehouse located in " + locations[i].Name); //print the name of warehouse and how many of each part it has
                Console.WriteLine("Part 102 = " + locations[i].Part_102);
                Console.WriteLine("Part 215 = " + locations[i].Part_215);
                Console.WriteLine("Part 410 = " + locations[i].Part_410);
                Console.WriteLine("Part 525 = " + locations[i].Part_525);
                Console.WriteLine("Part 711 = " + locations[i].Part_711 + "\n");
            }

            string[] transaction = File.ReadAllLines("Transactions.txt"); // make a string list of every line in transactions.txt
            for (int j = 0; j < transaction.Length; j++) //loop once for every line
            {
                String line = transaction[j]; // the current line is transaction at index j
                string[] record = line.Split(','); //split the current line on commas
                string prefix = record[0]; // take the prefix of the lines P or S

                if (prefix == "P") //if statement for Purchase
                {
                    int index = 0;
                    int total = GetStock(locations[0], record[1].Trim(' '));
                    for (int i = 0; i < locations.Count; i++) //find the warehouse with the lowest stock of the item
                    {
                        if (total > GetStock(locations[i], record[1].Trim(' '))) //when this warehouse is found take note of its index so it can be called in addstock
                        {
                            total = GetStock(locations[i], record[1].Trim(' '));
                            index = i;
                        }
                    }
                    AddStock(locations[index], record[1].Trim(' '), int.Parse(record[2].Trim(' '))); //call AddStock to put the new stock where it needs to go

                }
                else if (prefix == "S") // if statement for sale
                {
                    int index = 0;
                    int total = GetStock(locations[0], record[1].Trim(' '));
                    for (int i = 0; i < locations.Count; i++) //find the warehouse with the most stock of the item
                    {
                        if (total < GetStock(locations[i], record[1].Trim(' '))) // when this warehouse is found take note of its index
                        {
                            total = GetStock(locations[i], record[1].Trim(' '));
                            index = i;
                        }
                    }
                    ReduceStock(locations[index], record[1].Trim(' '), int.Parse(record[2].Trim(' '))); //call ReduceStock to remove the sold items from the store with the most
                }
            }
            Console.WriteLine("\n\nEnd of day stock... \n"); //print end of day stats
            for (int i = 0; i < locations.Count; i++)
            {
                Console.WriteLine("Warehouse Located in " + locations[i].Name); // print warehouse name followed by its stock of each part
                Console.WriteLine("Part 102 = " + locations[i].Part_102);
                Console.WriteLine("Part 215 = " + locations[i].Part_215);
                Console.WriteLine("Part 410 = " + locations[i].Part_410);
                Console.WriteLine("Part 525 = " + locations[i].Part_525);
                Console.WriteLine("Part 711 = " + locations[i].Part_711 + "\n");
            }
        }
        private static void AddStock(Warehouse warehouse, String part, int count) // Adds stock to a warehouse
        {
            if (part == "102")
            {
                warehouse.Part_102 += count;
            }
            else if (part == "215")
            {
                warehouse.Part_215 += count;
            }
            else if (part == "410")
            {
                warehouse.Part_410 += count;
            }
            else if (part == "525")
            {
                warehouse.Part_525 += count;
            }
            else if (part == "711")
            {
                warehouse.Part_711 += count;
            }
        }
        private static void ReduceStock(Warehouse warehouse, String part, int count) //Removes stock from a warehouse once it's sold
        {
            if (part == "102")
            {
                warehouse.Part_102 -= count;
            }
            else if (part == "215")
            {
                warehouse.Part_215 -= count;
            }
            else if (part == "410")
            {
                warehouse.Part_410 -= count;
            }
            else if (part == "525")
            {
                warehouse.Part_525 -= count;
            }
            else if (part == "711")
            {
                warehouse.Part_711 -= count;
            }
        }
        private static int GetStock(Warehouse warehouse, String part) //Get the stock of a particular part in a specific warehouse
        {
            int numToReturn = 0;
            if (part == "102")
            {
                numToReturn = warehouse.Part_102;
            }
            else if (part == "215")
            {
                numToReturn = warehouse.Part_215;
            }
            else if (part == "410")
            {
                numToReturn = warehouse.Part_410;
            }
            else if (part == "525")
            {
                numToReturn = warehouse.Part_525;
            }
            else if (part == "711")
            {
                numToReturn = warehouse.Part_711;
            }
            return numToReturn;
        }
    }
}