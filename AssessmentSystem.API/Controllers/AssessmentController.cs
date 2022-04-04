using AssessmentSystem.API.Models.DTO;
using AssessmentSystem.MVC;
using AssessmentSystem.MVC.Manager;
using System.Collections.Generic;
using System.Web.Http;

namespace AssessmentSystem.API.Controllers
{
    public class AssessmentController : ApiController
    {

        AssessmentManager assessmentManager = new AssessmentManager();
        
        /// <summary>
        /// Retorna listagem de provas.
        /// </summary>
        /// <returns>Enumerável de provas.</returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<AssessmentDTO> GetAll()
        {
            return assessmentManager.GetAll();
        }

        /// <summary>
        /// Criação de prova.
        /// </summary>
        /// <param name="assessmentDTO"></param>
        /// <returns>Id da prova.</returns>
        [HttpPost]
        [Authorize]
        public int Create([FromBody] AssessmentDTO assessmentDTO)
        {
            return assessmentManager.Create(assessmentDTO);
        }

        /// <summary>
        /// Atualiza prova.
        /// </summary>
        /// <param name="assessmentDTO"></param>
        /// <param name="id"></param>
        [HttpPut]
        [Authorize]
        public void Update([FromBody] AssessmentDTO assessmentDTO, [FromUri] int id)
        {
            assessmentManager.Update(id, assessmentDTO);
        }

        /// <summary>
        /// Apaga prova.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Authorize]
        public void Delete([FromUri] int id)
        {
            assessmentManager.Delete(id);
        }
    }
}