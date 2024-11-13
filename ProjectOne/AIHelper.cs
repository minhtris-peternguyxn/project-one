using System.Collections.Generic;
using System.Linq;

namespace ProjectOne
{
    public class AIHelper
    {
        private List<string> sampleTasks;

        public AIHelper()
        {

            sampleTasks = new List<string>
            {
                "Go to the gym", "Reading book", "Doing HomeWork",
                "Hang Out With Friend", "Doing Housework", "Clean the house"
            };
        }

        public string SuggestTask(List<Todo> existingTasks)
        {
            var recommendedTasks = sampleTasks.Except(existingTasks.Select(t => t.Desc)).ToList();

            if (recommendedTasks.Any())
            {
               
                return recommendedTasks[new Random().Next(recommendedTasks.Count)];
            }

            return "Doing new mission!";
        }
    }
}
