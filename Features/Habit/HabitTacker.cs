using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SociallyAnxiousHub.Features
{
    public class HabitTracker
    {
        // In-memory list to store habits
        private readonly List<Habit> _habits = new List<Habit>();

        // Method to add a new habit
        public void AddHabit(Habit habit) => _habits.Add(habit);

        // Method to remove a habit by its ID
        public void RemoveHabit(Guid habitId) => _habits.RemoveAll(h => h.Id == habitId);
        
        // Method to get all habits
        public List<Habit> GetAllHabits() => _habits;
    }
}