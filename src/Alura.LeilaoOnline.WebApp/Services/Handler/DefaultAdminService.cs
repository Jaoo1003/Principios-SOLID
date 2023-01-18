using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Dados.EfCore;
using Alura.LeilaoOnline.WebApp.Models;
using System;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Services.Handler {
    public class DefaultAdminService : IAdminService {

        private ILeilaoDao _dao;
        public DefaultAdminService(ILeilaoDao dao) {
            _dao = dao;
        }

        public void CadastrarLeilao(Leilao leilao) {
            _dao.Incluir(leilao);
        }

        public IEnumerable<Categoria> ConsultaCategorias() {
            return _dao.BuscarTodasCategorias();
        }

        public Leilao ConsultaLeilaoPorId(int id) {
            return _dao.BuscarLeilaoPorId(id);
        }

        public IEnumerable<Leilao> ConsultaLeiloes() {
            return _dao.BuscarTodosLeiloes();
        }

        public void FinalizaPregaoDoLeilaoComId(int id) {
            var leilao = _dao.BuscarLeilaoPorId(id);
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao) {
                leilao.Situacao = SituacaoLeilao.Finalizado;
                leilao.Termino = DateTime.Now;
                _dao.Alterar(leilao);
            }
        }

        public void IniciaPregaoDoLeilaoComId(int id) {
            var leilao = _dao.BuscarLeilaoPorId(id);
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Rascunho) {
                leilao.Situacao = SituacaoLeilao.Pregao;
                leilao.Inicio = DateTime.Now;
                _dao.Incluir(leilao);
            }
        }

        public void ModificarLeilao(Leilao leilao) {
            _dao.Alterar(leilao);
        }

        public void RemoverLeilao(Leilao leilao) {
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao) {
                _dao.Excluir(leilao);
            }
        }
    }
}
