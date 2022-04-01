using AssessmentSystem.API.Manager;
using AssessmentSystem.API.Models.DTO;
using AssessmentSystem.MVC.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AssessmentSystem.API.Controllers
{
    public class QuestionController : ApiController
    {
        QuestionManager questionManager = new QuestionManager();

        /// <summary>
        /// Retorna listagem de questões.
        /// </summary>
        /// <returns>Enumerável de provas.</returns>
        public IEnumerable<QuestionDTO> GetAll()
        {
            return questionManager.GetAll();
        }

        /// <summary>
        /// Criação de questão.
        /// </summary>
        /// <param name="questionDTO"></param>
        /// <returns>Id da prova.</returns>
        [HttpPost]
        public int Create([FromBody] QuestionDTO questionDTO)
        {
            return questionManager.Create(questionDTO);
        }

        /// <summary>
        /// Atualiza questão.
        /// </summary>
        /// <param name="questionDTO"></param>
        /// <param name="id"></param>
        [HttpPut]
        public void Update([FromBody] QuestionDTO questionDTO, [FromUri] int id)
        {
            questionManager.Update(id, questionDTO);
        }

        /// <summary>
        /// Apaga questão.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete([FromUri] int id)
        {
            questionManager.Delete(id);
        }
    }
}
