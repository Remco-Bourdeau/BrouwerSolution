using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BrouwersClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            //1 brouwer
            /*
            Console.Write("Id: ");
            var id = int.Parse(Console.ReadLine());
            var response = await client.GetAsync(
                    $"http://localhost:16436/brouwers/{id}");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var brouwer = await response.Content.ReadAsAsync<Brouwer>();
                    Console.WriteLine(brouwer.Naam);
                    break;
                case HttpStatusCode.NotFound:
                    Console.WriteLine("Brouwer niet gevonden");
                    break;
                default:
                    Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
                    break;
            }
            */
            //alle brouwers
            /*
            var response = await client.GetAsync(
                    $"http://localhost:16436/brouwers");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var brouwers = await response.Content.ReadAsAsync<List<Brouwer>>();
                    foreach(var brouwer in brouwers)
                        Console.WriteLine(brouwer.Naam);
                    break;
                case HttpStatusCode.NotFound:
                    Console.WriteLine("Brouwer niet gevonden");
                    break;
                default:
                    Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
                    break;
            }
            */
            //brouwers met begin naam
            /*
            Console.Write("Begin van de naam: ");
            var begin = Console.ReadLine();
            var response = await client.GetAsync(
                $"http://localhost:16436/brouwers/naam?begin={begin}");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var brouwers = await response.Content.ReadAsAsync<List<Brouwer>>();
                    foreach (var brouwer in brouwers)
                        Console.WriteLine(brouwer.Naam);
                    break;
                case HttpStatusCode.NotFound:
                    Console.WriteLine("Brouwer niet gevonden");
                    break;
                default:
                    Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
                    break;
            }
            */
            //een brouwer toevoegen
            /*
            var brouwer = new Brouwer();
            Console.Write("Naam: ");
            brouwer.Naam = Console.ReadLine();
            Console.Write("Postcode: ");
            brouwer.Postcode = int.Parse(Console.ReadLine());
            Console.Write("Gemeente");
            brouwer.Gemeente = Console.ReadLine();
            var response = await client.PostAsJsonAsync("http://localhost:16436/brouwers", brouwer);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                Console.Write("Brouwer is toegevoegd. ");
                Console.WriteLine($"Zijn URI is {response.Headers.Location}");
            }
            else
                Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
            */
            //een brouwer aanpassen - 1. opvragen 2. aanpassen
            Console.Write("Id: ");
            var id = int.Parse(Console.ReadLine());
            var uri = $"http://localhost:16436/brouwers/{id}";
            var response = await client.GetAsync(uri);
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var brouwer = await response.Content.ReadAsAsync<Brouwer>();
                    brouwer.Gemeente = brouwer.Gemeente.ToUpper();
                    response = await client.PutAsJsonAsync(uri, brouwer);
                    if (response.StatusCode == HttpStatusCode.OK)
                        Console.WriteLine("Brouwer gewijzigd.");
                    else
                        Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
                    break;
                case HttpStatusCode.NotFound:
                    Console.WriteLine("Brouwer niet gevonden");
                    break;
                default:
                    Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
                    break;
            }






        }
    }
}
