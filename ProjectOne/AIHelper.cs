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
                "Go to the gym", "Reading book", "Doing homework", "Hang out with friend", "Doing housework",
"Clean the house", "Cooking a meal", "Grocery shopping", "Walking the dog", "Meditating",
"Watching a movie", "Listening to music", "Practicing a musical instrument", "Learning a new language",
"Organizing the closet", "Washing the car", "Exercising at home", "Planning for the week",
"Writing a journal", "Calling family", "Checking emails", "Studying for exams", "Gardening",
"Playing video games", "Fixing something broken", "Going for a run", "Shopping for clothes",
"Painting or drawing", "Researching a topic", "Learning to cook a new dish"
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
