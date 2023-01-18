using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Dados.EfCore;
using Alura.LeilaoOnline.WebApp.Models;
using System;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Services.Handler {
    public class DefaultAdminService : IAdminService {

        private ILeilaoDao _leilaoDao;
        private ICategoriaDao _categoriaDao;
        public DefaultAdminService(ILeilaoDao dao, ICategoriaDao categoriaDao) {
            _leilaoDao = dao;
            _categoriaDao = categoriaDao;
        }

        public void CadastrarLeilao(Leilao leilao) {
            _leilaoDao.Incluir(leilao);
        }

        public IEnumerable<Categoria> ConsultaCategorias() {
            return _categoriaDao.BuscarTodos();
        }

        public Leilao ConsultaLeilaoPorId(int id) {
            return _leilaoDao.BuscarPorId(id);
        }

        public IEnumerable<Leilao> ConsultaLeiloes() {
            return _leilaoDao.BuscarTodos();
        }

        public void FinalizaPregaoDoLeilaoComId(int id) {
            var leilao = _leilaoDao.BuscarPorId(id);
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao) {
                leilao.Situacao = SituacaoLeilao.Finalizado;
                leilao.Termino = DateTime.Now;
                _leilaoDao.Alterar(leilao);
            }
        }

        public void IniciaPregaoDoLeilaoComId(int id) {
            var leilao = _leilaoDao.BuscarPorId(id);
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Rascunho) {
                leilao.Situacao = SituacaoLeilao.Pregao;
                leilao.Inicio = DateTime.Now;
                _leilaoDao.Incluir(leilao);
            }
        }

        public void ModificarLeilao(Leilao leilao) {
            _leilaoDao.Alterar(leilao);
        }

        public void RemoverLeilao(Leilao leilao) {
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao) {
                _leilaoDao.Excluir(leilao);
            }
        }
    }
}
