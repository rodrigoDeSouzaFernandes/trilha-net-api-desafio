using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using trilhaNetApiDesafio.Exceptions;

namespace TrilhaApiDesafio.Services
{
    public class TarefaService
    {
        private readonly OrganizadorContext _context;

        public TarefaService(OrganizadorContext context)
        {
            _context = context;
        }

        public Tarefa ObterPorId(int id)
        {
            Tarefa tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                throw new NotFoundException("Tarefa não encontrada");
            }
            return tarefa;
        }

        public List<Tarefa> ObterTodos()
        {
            List<Tarefa> tarefas = _context.Tarefas.ToList();
            return tarefas;
        }

        public List<Tarefa> ObterPorTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("O título não pode ser vazio ou nulo.", nameof(titulo));
            }
            List<Tarefa> tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToList();
            return tarefas;
        }

        public List<Tarefa> ObterPorData(DateTime data)
        {
            if (data == DateTime.MinValue)
            {
                throw new ArgumentException("O campo \"Data\" é obrigatório.");
            }

            var inicioDia = data.Date;
            var fimDia = inicioDia.AddDays(1);

            List<Tarefa> tarefas = _context
                .Tarefas.Where(x => x.Data >= inicioDia && x.Data < fimDia)
                .ToList();

            return tarefas;
        }

        public List<Tarefa> ObterPorStatus(EnumStatusTarefa status)
        {
            List<Tarefa> tarefas = _context.Tarefas.Where(x => x.Status == status).ToList();
            return tarefas;
        }

        public Tarefa Criar(Tarefa tarefa)
        {
            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
                throw new ArgumentException("O título da tarefa é obrigatório.");

            if (tarefa.Data == DateTime.MinValue)
                throw new ArgumentException("A data da tarefa é obrigatória.");

            if (
                tarefa.Status != EnumStatusTarefa.Finalizado
                && tarefa.Status != EnumStatusTarefa.Pendente
            )
            {
                throw new ArgumentException("Status inválido");
            }

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return tarefa;
        }

        public void Atualizar(int id, Tarefa tarefa)
        {
            Tarefa tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
            {
                throw new NotFoundException("Não foi encontrada nenhuma tarefa com esse id");
            }

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                throw new NotFoundException("Id não encontrado");

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
        }
    }
}
