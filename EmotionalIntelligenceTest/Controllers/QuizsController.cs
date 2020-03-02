using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmotionalIntelligenceTest.Models;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EmotionalIntelligenceTest.Controllers
{
    [TestClass]
    public class QuizsController : Controller
    {
        private  ApplicationDBContext _context = new ApplicationDBContext();

        public QuizsController(ApplicationDBContext context)
        {
           // _context = context;
        }

        //[Route("Index")]
        //[Route("")]
        //[Route("~/")]
        [TestMethod]
        public IActionResult Index()
        {
            ViewBag.Question = _context.Questions.ToList();

            return View();
        }

        [HttpPost]
        //[Route("submit")]
        public IActionResult Submit(IFormCollection iformcollection)
        {
           
            int score = 0;
            Byte[] img = null;
            string[] questionIds = iformcollection["QuestionID"];
            Assert.IsNotNull(questionIds);
            var question = _context.Questions.ToList() ;  
          foreach (var QuestionID in questionIds)
            {
                int answerIdCorrect = _context.Questions.Find(int.Parse
                    (QuestionID)).Answer.Where(a => a.correct == true).FirstOrDefault().ID;
                try
                {                   
                    if (answerIdCorrect == int.Parse(iformcollection["question_"+QuestionID]))
                    {
                        int id = Convert.ToInt32(QuestionID);
                        score++;
                        img = question[id - 1].image;
                        Assert.IsNotNull(img);

                        Image x = (Bitmap)new ImageConverter().ConvertFrom(img);
                        ViewBag.image = x;

                    }
                }
                catch(ArgumentNullException ex)
                {

                }
              
                ViewBag.Image = img;
                ViewBag.score = score;
            }
           
            return View("Result");
        }
    }
}
