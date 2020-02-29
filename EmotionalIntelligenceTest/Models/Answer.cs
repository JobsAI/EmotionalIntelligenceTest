using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmotionalIntelligenceTest.Models
{
    [Table("Answers")]
    public partial class Answer
    {
       
        public int ID { get; set; }
        public string Content { get; set; }
        public bool correct { get; set; }
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }
    }
}
