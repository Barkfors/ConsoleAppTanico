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
                    Console.WriteLine("Customer:\t" + aCustomer.ToString());
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


            Console.WriteLine("\n******************");
            Console.WriteLine("Posting loadreport..");
            var deliveryResult = await client.PostLoadingReport(new CimsLoadReportRequest()
            {
                DeliveryRows = new CimsLoadReportRow[]
                    {
                        new CimsLoadReportRow()
                        {
                            OrderNo = "1000",
                            RowNo = 1,
                            ItemNo = "Item 1",
                            Location = "Location 1",
                            DeliveryDate = DateTime.Now,
                            Unit = "KG",
                            Quantity = 1204.0,
                            ReferenceNo = "Ref 1"
                        },
                        new CimsLoadReportRow()
                        {
                            OrderNo = "1000",
                            RowNo = 2,
                            ItemNo = "Item 2",
                            Location = "Location 2",
                            DeliveryDate = DateTime.Now,
                            Unit = "KG",
                            Quantity = 750.0,
                            ReferenceNo = "Ref 2"
                        }
                    }
            });
            Console.WriteLine("..done");

            if (deliveryResult.DeliveryRowResults != null)
            {
                foreach (var aDeliveryResult in deliveryResult.DeliveryRowResults)
                {
                    Console.WriteLine("LoadReportResult:\t" + aDeliveryResult.ToString());
                }
            }


            Console.WriteLine("\n******************");
            Console.WriteLine("Posting freightcosts..");
            var freightCostsResult = await client.PostFreightCosts(new CimsFreightCostRequest()
            {
                IdNo = "Idno 1",
                AdditionalCosts = new CimsFreightCostRow[]
                    {
                        new CimsFreightCostRow()
                        {
                            CostIdNo = "70",
                            Amount = 1200.50
                        },
                        new CimsFreightCostRow()
                        {
                            CostIdNo = "80",
                            Amount = 750.50
                        },
                    }
            });
            Console.WriteLine("..done");

            Console.WriteLine("FreightCostResult:\t" + freightCostsResult.ToString());


            Console.ReadLine();
        }
    }
}
