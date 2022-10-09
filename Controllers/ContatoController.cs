using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoMvcPottencial.Context;
using ProjetoMvcPottencial.Models;

namespace ProjetoMvcPottencial.Controllers
{
    public class ContatoController : Controller
    {
        private readonly AgendaContext _context;

        //Inserindo com Injeção de Dependência:
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        //Vai carregar os contatos vindo do db em forma de lista:
        public IActionResult Index()
        {
            var contatos = _context.Contatos.ToList();
            return View(contatos);
        }

        //No mvc é opcional o HttpGet porque ele já reconhece isso aqui
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Contato contato)
        {
            if (ModelState.IsValid)
            {
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return RedirectToAction(nameof(Index));

            return View(contato);
        }

        [HttpPost]
        public IActionResult Editar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id);

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            var contato = _context.Contatos.Find(id);

            if(contato == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(contato);
        }

        [HttpGet]
        public IActionResult Deletar(int id)
        {
            var contato = _context.Contatos.Find(id);

            if(contato == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(contato);
        }

        [HttpPost]
        public IActionResult Deletar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id);

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}