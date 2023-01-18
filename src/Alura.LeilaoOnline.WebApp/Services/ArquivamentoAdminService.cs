using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Services.Handler;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Services {
    public class ArquivamentoAdminService : IAdminService {

        private IAdminService _defaultService;

        public ArquivamentoAdminService(ILeilaoDao leilaoDao, ICategoriaDao categoriaDao) {
            _defaultService = new DefaultAdminService(leilaoDao, categoriaDao);
        }

        public void CadastrarLeilao(Leilao leilao) {
            _defaultService.CadastrarLeilao(leilao);
        }

        public IEnumerable<Categoria> ConsultaCategorias() {
            return _defaultService.ConsultaCategorias();
        }

        public Leilao ConsultaLeilaoPorId(int id) {
            return _defaultService.ConsultaLeilaoPorId(id);
        }

        public IEnumerable<Leilao> ConsultaLeiloes() {
            return _defaultService.ConsultaLeiloes()
                .Where(l => l.Situacao != SituacaoLeilao.Arquivado);
        }

        public void FinalizaPregaoDoLeilaoComId(int id) {
            _defaultService.FinalizaPregaoDoLeilaoComId(id);
        }

        public void IniciaPregaoDoLeilaoComId(int id) {
            _defaultService.IniciaPregaoDoLeilaoComId(id);
        }

        public void ModificarLeilao(Leilao leilao) {
            _defaultService.ModificarLeilao(leilao);
        }

        public void RemoverLeilao(Leilao leilao) {
            if (leilao != null && leilao.Situacao != SituacaoLeilao.Pregao) {
                leilao.Situacao = SituacaoLeilao.Arquivado;
                _defaultService.ModificarLeilao(leilao);
            }
        }
    }
}
