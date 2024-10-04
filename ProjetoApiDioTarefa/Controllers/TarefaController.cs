using Microsoft.AspNetCore.Mvc;
using ProjetoApiDioTarefa.Context;
using ProjetoApiDioTarefa.Models;

namespace ProjetoApiDioTarefa.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController : ControllerBase
{
	private readonly OrganizadorContext _context;

	public TarefaController(OrganizadorContext context)
	{
		_context = context;
	}

	[HttpGet("{id}")] //TUDO FUNCIONANDO OK!
	public IActionResult ObterPorId(int id)
	{
		var consTarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
		if (consTarefa == null)
			return NotFound();
		return Ok(consTarefa);
	}

	[HttpGet("ObterTodos")] //TUDO FUNCIONANDO OK!
	public IActionResult ObterTodos()
	{
		var consTarefa = _context.Tarefas.ToList();
		return Ok(consTarefa);
	}

	[HttpGet("ObterPorTitulo")] // TUDO FUNCIONANDO OK!
	public IActionResult ObterPorTitulo(string titulo)
	{
		var consTarefa = _context.Tarefas.Where(t=> t.Titulo.Contains(titulo));
		if (consTarefa is null) 
			return NotFound();

		return Ok(consTarefa);
	}

	[HttpGet("ObterPorData")]  //TUDO FUNCIONANDO OK!
	public IActionResult ObterPorData(DateTime data)
	{
		var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
		return Ok(tarefa);
	}

	[HttpGet("ObterPorStatus")] //TUDO FUNCIONANDO OK!
	public IActionResult ObterPorStatus(EnumStatusTarefa status)
	{
		
		var tarefa = _context.Tarefas.Where(x => x.Status == status);
		return Ok(tarefa);
	}

	[HttpPost]//TUDO FUNCIONANDO OK!
	public IActionResult Criar(Tarefa tarefa)
	{
		if (tarefa.Data == DateTime.MinValue)
			return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

		_context.Add(tarefa);
		_context.SaveChanges();
		return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
	} 

	[HttpPut("{id}")]//TUDO FUNCIONANDO OK!
	public IActionResult Atualizar(int id, Tarefa tarefa)
	{
		var tarefaBanco = _context.Tarefas.Find(id);

		if (tarefaBanco == null)
			return NotFound();

		if (tarefa.Data == DateTime.MinValue)
			return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

		tarefaBanco.Titulo = tarefa.Titulo;
		tarefaBanco.Descricao = tarefa.Descricao;
		tarefaBanco.Data = tarefa.Data;
		tarefaBanco.Status = tarefa.Status;
		_context.Tarefas.Update(tarefaBanco);
		_context.SaveChanges();
		return Ok(tarefaBanco);
	}

	[HttpDelete("{id}")] 
	public IActionResult Deletar(int id)
	{
		var tarefaBanco = _context.Tarefas.Find(id);

		if (tarefaBanco == null)
			return NotFound();

		_context.Remove(tarefaBanco);
		_context.SaveChanges();
		return NoContent();
	}
}