using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ASS.RandomUserGenerator.Generators
{
    public class RandomNameGenerator
    {
        private readonly Random rand;
        private readonly List<string> male;
        private readonly List<string> female;
        private readonly List<string> last;
        private readonly string culture;

        public RandomNameGenerator(Random rand, string cultureInfo)
        {
            this.rand = rand;
            ListsOfNames l = new ListsOfNames();

            JsonSerializer serializer = new JsonSerializer();
            string fileName = cultureInfo == "EN" ? "names-en.json" : "names-hun.json";
            using (StreamReader reader = new StreamReader(Path.Combine("Files", fileName), Encoding.UTF8))
            using (JsonReader jreader = new JsonTextReader(reader))
            {
                l = serializer.Deserialize<ListsOfNames>(jreader);
            }

            male = new List<string>(l.Male);
            female = new List<string>(l.Female);
            last = new List<string>(l.Last);

            culture = cultureInfo;
        }

        public string Generate(Sex sex, int middle = 0)
        {
            string firstName, lastName;
            if (culture == "EN")
            {
                firstName = sex == Sex.Male ? male[rand.Next(male.Count)] : female[rand.Next(female.Count)];
                lastName = last[rand.Next(last.Count)];
            }
            else
            {
                lastName = sex == Sex.Male ? male[rand.Next(male.Count)] : female[rand.Next(female.Count)];
                firstName = last[rand.Next(last.Count)];
            }

            List<string> middles = new List<string>();

            for (int i = 0; i < middle; i++)
            {
                middles.Add(sex == Sex.Male ? male[rand.Next(male.Count)] : female[rand.Next(female.Count)]);
            }

            StringBuilder b = new StringBuilder();
            b.Append(firstName + " ");
            foreach (string m in middles)
            {
                b.Append(m + " ");
            }
            b.Append(lastName);

            return b.ToString();
        }
    }
}
