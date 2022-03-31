using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIEscola.Model;
using APIEscola.Repository.Interface;
using Microsoft.AspNetCore.Cors;
using System;

namespace APIEscola.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _alunoRepository;

        //injeção de dependencia 
        public AlunoController(IAlunoRepository alunorepo)
        {
            _alunoRepository = alunorepo;
        }


        // GET: AlunoController
        [HttpGet("/aluno")]
        public async Task<List<Aluno>> GetAlunos()
        {
            return await _alunoRepository.GetAlunosAsync();
        }

        [HttpPost("/aluno")]
        public async Task<JsonResult> CreateAlunos([FromBody] Aluno aluno)
        {
            if (aluno != null && !string.IsNullOrEmpty(aluno.nome))
            {
                var result = await _alunoRepository.SaveAsync(aluno);
                return new JsonResult ( new { sucesso = result }  );
            }
            else
            {
               throw new Exception(" Não foi cadastrado o Aluno");
            }

        }

        [HttpGet("/aluno/{id}")]
        public async Task<Aluno> Edit([FromRoute] int id)
        {
            return await _alunoRepository.GetAlunoByIdAsync(id);
        }

        [HttpPatch("/aluno")]
        public async Task<Aluno> EditConfirmed([FromBody] Aluno aluno)
        {
            if(await _alunoRepository.UpdateAlunoAsync(aluno))
            {
                return await _alunoRepository.GetAlunoByIdAsync((int)aluno.id);
            }
            else
            {
                return null;
            }
        }


        [HttpDelete("/aluno/{id}")]
        public async Task<JsonResult> Delete([FromRoute] int id)
        {
            if (id != 0)
            {
                return new JsonResult(new { sucesso = (await _alunoRepository.DeleteAsync(id)) });
            }
            else
            {
                return new JsonResult(new { sucesso = false }); ;
            }

        }
    }
}
