using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTanico
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();

        }

        static async Task MainAsync()
        {
            var client = new CimsClient();

            Console.WriteLine("\n******************");
            Console.WriteLine("Getting customers..");
            var customerResult = await client.GetCustomers();
            Console.WriteLine("..done");

            if (customerResult.Customers != null)
            {
                foreach (var aCustomer in customerResult.Customers)
                {
                    Console.WriteLine("Customer:\t"  + aCustomer.ToString());
                }
            }


            Console.WriteLine("\n******************");
            Console.WriteLine("Getting items..");
            var itemsResult = await client.GetItems();
            Console.WriteLine("..done");

            if (itemsResult.Items != null)
            {
                foreach (var anItem in itemsResult.Items)
                {
                    Console.WriteLine("Item:\t" + anItem.ToString());
                }
            }


            Console.WriteLine("\n******************");
            Console.WriteLine("Getting contracts..");
            var contractsResult = await client.GetContracts("100");
            Console.WriteLine("..done");

            if (contractsResult.Contracts != null)
            {
                foreach (var aContract in contractsResult.Contracts)
                {
                    Console.WriteLine("Contract:\t" + aContract.ToString());
                }
            }


            Console.ReadLine();
        }
    }
}
