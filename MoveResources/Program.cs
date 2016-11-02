using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveResources
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Freelancers.FreelancersDataContext site = new Freelancers.FreelancersDataContext(new Uri("http://inside.office.palex/lqa/_vti_bin/ListData.svc"));

            site.Credentials = System.Net.CredentialCache.DefaultCredentials;

            var resourcesList = site.ResourceLists;

            Dictionary<string, Freelancers.ServicesItem> services = new Dictionary<string, Freelancers.ServicesItem>()
            {
                { "Translator",  GetService(site, "Translation") },
                { "Editor", GetService(site, "Editing") },
                { "Proofreader",  GetService(site, "Proofreading")},
                {"Copyeditor", GetService(site, "Copyediting") },
                {"LQA", GetService(site, "LQA") },
                {"SME", GetService(site, "SME") },
                {"FQA", GetService(site, "Bilingual FQA") },
                {"LSO", GetService(site, "LSO") }
            };
            int total = resourcesList.Count();
            int count = 0;

            foreach (var item in resourcesList)
            {
                AddService(site, services, item, item.TranslatorId, "Translator");
                AddService(site, services, item, item.EditorId, "Editor");
                AddService(site, services, item, item.ProofreaderId, "Proofreader");
                AddService(site, services, item, item.CopyeditorId, "Copyeditor");
                AddService(site, services, item, item.LQAId, "LQA");
                AddService(site, services, item, item.SMEId, "SME");
                AddService(site, services, item, item.FQAId, "FQA");
                AddService(site, services, item, item.LSOId, "LSO");
                site.SaveChanges();
                Console.Clear();
                Console.WriteLine("Обновлено элементов: {0} из {1}", ++count, total);

            }



            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void AddService(Freelancers.FreelancersDataContext site, Dictionary<string, Freelancers.ServicesItem> services, Freelancers.ResourceListsItem item, int? propertyId, string serviceName)
        {
            switch (propertyId)
            {
                case null:
                    break;
                case 1:
                    site.AddLink(item, "Main", services[serviceName]);
                    break;
                case 2:
                    site.AddLink(item, "Backup", services[serviceName]);
                    break;
                case 3:
                    site.AddLink(item, "Candidate", services[serviceName]);
                    break;
            }
        }

        private static Freelancers.ServicesItem GetService(Freelancers.FreelancersDataContext site, string ServiceName)
        {
            return (from i in site.Services where i.ServiceNameEn == ServiceName select i).First();
        }
    }
}
