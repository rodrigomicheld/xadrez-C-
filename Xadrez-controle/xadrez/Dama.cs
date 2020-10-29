using tabuleiro;

namespace xadrez {
    class Dama:Peca {

        public Dama(Cor cor,Tabuleiro tab) : base(cor,tab) {
        }
        
        private bool podeMover(Posicao pos) {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }
        // movimento do bispo e o da torre forma os movimentos possiveis da dama
        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas,Tab.Colunas];

            Posicao pos = new Posicao(0,0);

            //acima
            pos.definirValores(Posicao.Linha - 1,Posicao.Coluna);
            while(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) {
                    break;
                }
                pos.Linha = pos.Linha - 1;
            }
            //esquerda
            pos.definirValores(Posicao.Linha,Posicao.Coluna - 1);
            while(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }
            //direita
            pos.definirValores(Posicao.Linha,Posicao.Coluna + 1);
            while(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }
            //abaixo
            pos.definirValores(Posicao.Linha + 1,Posicao.Coluna);
            while(Tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Linha,pos.Coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor) {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }
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
            return "D";
        }
    }
}
