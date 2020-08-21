using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez {
    class PartidaDeXadrez {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public PartidaDeXadrez() {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            colocarPeca();
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }
        
        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private void colocarPeca() {
            colocarNovaPeca('c', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('c', 2, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('d', 2, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('e', 2, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('e', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));

            colocarNovaPeca('c', 7,new Torre(Cor.Preta, Tab));
            colocarNovaPeca('c', 8,new Torre(Cor.Preta, Tab));
            colocarNovaPeca('d', 7,new Torre(Cor.Preta, Tab));
            colocarNovaPeca('e', 7,new Torre(Cor.Preta, Tab));
            colocarNovaPeca('e', 8,new Torre(Cor.Preta, Tab));
            colocarNovaPeca('d', 8,new Rei(Cor.Preta, Tab));
        }
        public void executaMovimento(Posicao origem, Posicao destino) {
            Peca p = Tab.retirarPeca(origem);
            p.incrementarQtdMovimento();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
        }
        public void realizaJogada(Posicao origem, Posicao destino) {
            executaMovimento(origem, destino);
            Turno++;
            mudaJogador();
        }

        public void validarPosicaoOrigem(Posicao pos) {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.peca(pos).Cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if (!Tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }
        public void validarPosicaoDestino(Posicao origem, Posicao destino) {
            if (!Tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválido!");
            }

        }
        private void mudaJogador() {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
    }
}
