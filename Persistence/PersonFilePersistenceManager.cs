using Core.Interfaces;
using Entity;

namespace Persistence
{
    public class PersonFilePersistenceManager : IPersistentPersonGateway
    {
        public Person LoadPerson(string hashNumber)
        {
            if (hashNumber != null)
            {
                Person person = FilePersistenceUtility.LoadJsonDataFromFile<Person>($"{hashNumber}.txt");
                return person;
            }
            return null;
        }

        public void SavePerson(Person person)
        {
            string filePath = person.Hash + ".txt";
            FilePersistenceUtility.SaveObjectToTextFile<Person>(filePath, person);
        }
    }
}
