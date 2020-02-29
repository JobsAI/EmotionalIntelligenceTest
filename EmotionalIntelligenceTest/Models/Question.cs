using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EmotionalIntelligenceTest.Models
{
    [Table("Questions")]
    public class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
        }

        public int ID { get; set; }
        public string Content { get; set; }
        
        public byte[] image{ get; set; }
      
        public virtual ICollection<Answer> Answer { get; set; }
    }
}

