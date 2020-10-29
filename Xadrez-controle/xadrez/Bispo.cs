
using tabuleiro;

namespace Xadrez {
    class Bispo:Peca {

        public Bispo (Cor cor,Tabuleiro tab) : base(cor,tab) {
        }

        private bool podeMover(Posicao pos) {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas,Tab.Colunas];

            Posicao pos = new Posicao(0,0);

            //nordeste
            pos.definirValores(Posicao.Linha - 1,Posicao.Coluna + 1);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            //sudeste
            pos.definirValores(Posicao.Linha + 1,Posicao.Coluna + 1);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            //sudoeste
            pos.definirValores(Posicao.Linha + 1,Posicao.Coluna - 1);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            //noroeste
            pos.definirValores(Posicao.Linha - 1,Posicao.Coluna - 1);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            return mat;
        }

        public override string ToString() {
            return "B";
        }
        
    }
}
