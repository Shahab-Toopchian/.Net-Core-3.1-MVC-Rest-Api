using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data {
    public class MockCommanderRepo : ICommanderRepo {
        //test repo for static data

        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands () {
            var commands = new List<Command> {
                new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle and pan" },
                new Command { Id = 1, HowTo = "Cut bread", Line = "Get a knife", Platform = "Knife & chopping board" },
                new Command { Id = 2, HowTo = "Make cup of tea", Line = "Place tea bag", Platform = "Kettle" }
            };
            return commands;
        }

        public Command GetCommandById (int Id) {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle and pan" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}