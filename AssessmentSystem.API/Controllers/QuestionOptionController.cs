using AssessmentSystem.API.Manager;
using AssessmentSystem.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AssessmentSystem.API.Controllers
{
    public class QuestionOptionController : ApiController
    {
        QuestionOptionManager questionManager = new QuestionOptionManager();

        /// <summary>
        /// Retorna listagem de alternativas.
        /// </summary>
        /// <returns>Enumerável de provas.</returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<QuestionOptionDTO> GetAll()
        {
            return questionManager.GetAll();
        }

        /// <summary>
        /// Criação de alternativa.
        /// </summary>
        /// <param name="questionOptionDTO"></param>
        /// <returns>Id da prova.</returns>
        [HttpPost]
        [Authorize]
        public int Create([FromBody] QuestionOptionDTO questionOptionDTO)
        {
            return questionManager.Create(questionOptionDTO);
        }

        /// <summary>
        /// Atualiza alterativa.
        /// </summary>
        /// <param name="questionOptionDTO"></param>
        /// <param name="id"></param>
        [HttpPut]
        [Authorize]
        public void Update([FromBody] QuestionOptionDTO questionOptionDTO, [FromUri] int id)
        {
            questionManager.Update(id, questionOptionDTO);
        }

        /// <summary>
        /// Apaga alternativa.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Authorize]
        public void Delete([FromUri] int id)
        {
            questionManager.Delete(id);
        }
    }
}
