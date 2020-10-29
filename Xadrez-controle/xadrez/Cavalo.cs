
using tabuleiro;

namespace Xadrez {
    class Cavalo:Peca {

        public Cavalo(Cor cor,Tabuleiro tab) : base(cor,tab) {
        }

        private bool podeMover(Posicao pos) {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas,Tab.Colunas];

            Posicao pos = new Posicao(0,0);

            //movimentos em L;
            pos.definirValores(Posicao.Linha + 1,Posicao.Coluna + 2);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha + 1,Posicao.Coluna - 2);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha - 1,Posicao.Coluna + 2);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha - 1,Posicao.Coluna - 2);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha -2,Posicao.Coluna + 1);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha - 2,Posicao.Coluna - 1);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha + 2,Posicao.Coluna + 1);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            pos.definirValores(Posicao.Linha + 2,Posicao.Coluna - 1);
            if(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
            }
            return mat;
        }

        public override string ToString() {
            return "C";
        }
    }
}

