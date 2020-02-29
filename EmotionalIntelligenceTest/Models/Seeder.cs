using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalIntelligenceTest.Models
{
    public class Seeder
    {
        private ApplicationDBContext _context;
        public Seeder(ApplicationDBContext context)
        {
            _context = context;
        }
        public void Seed()
        {


            _context.Database.EnsureCreated();
            if (_context.Questions.Any())
            {
                return;
            }
            var question = new Question[]
            {
                new Question{Content="This face is expressing?"},
                new Question{ Content="This face is expressing?"}

            };
            foreach (Question q in question)
            {
                _context.Questions.Add(q);
            }
            _context.AddRange();
            _context.SaveChanges();

            if (_context.Answers.Any())
            {
                return;
            }
            var answer = new Answer[]
            {
                new Answer{Content="sadness", correct=true, QuestionID=1 },
                new Answer{Content="amusement", correct=false, QuestionID=2 }
            };
            foreach (Answer a in answer)
            {
                _context.Answers.Add(a);
            }
            _context.AddRange();
            _context.SaveChanges();
        }




    }
}
