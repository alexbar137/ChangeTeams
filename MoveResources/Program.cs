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

            var feedbacksList = site.Feedbacks;

            Dictionary<string, int> teams = new Dictionary<string, int>()
            {
                {"Microsoft", 4},
                {"Business", 3},
                {"ML Med",  2},
                {"ML Tech", 1},
                {"Ru Tech", 5 }
            };

            int total = feedbacksList.Count();
            int count = 0;


            foreach (var item in feedbacksList)
            {
                if(item.TeamNewId == null)
                {
                    item.TeamNewId = teams[item.TeamValue];
                    site.UpdateObject(item);
                    site.SaveChanges();
                }
                Console.Clear();
                Console.WriteLine("Обработано элементов: {0} из {1}", ++count, total);
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

    }
}
