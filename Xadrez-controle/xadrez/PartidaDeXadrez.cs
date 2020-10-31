using System;
using System.Collections.Generic;
using System.Dynamic;
using tabuleiro;
using Xadrez;

namespace xadrez {
    class PartidaDeXadrez {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }
        public Peca vuleravelEmPassant { get; private set; }

        public PartidaDeXadrez() {
            Tab = new Tabuleiro(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            vuleravelEmPassant = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            colocarPeca();
        }
        public void colocarNovaPeca(char coluna,int linha,Peca peca) {
            Tab.colocarPeca(peca,new PosicaoXadrez(coluna,linha).toPosicao());
            Pecas.Add(peca);
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capturadas) {
                if(x.Cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public bool testeXequemate(Cor cor) {
            if(!estaEmXeque(cor)) {
                return false;
            }
            foreach(Peca x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis();
                for(int i = 0; i < Tab.Linhas; i++) {
                    for(int j = 0; j < Tab.Colunas; j++) {
                        if(mat[i,j]) {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i,j);
                            Peca pecaCapturada = executaMovimento(origem,destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem,destino,pecaCapturada);
                            if(!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Pecas) {
                if(x.Cor == cor) {

                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private void colocarPeca() {
            colocarNovaPeca('a',1,new Torre(Cor.Branca,Tab));
            colocarNovaPeca('b',1,new Cavalo(Cor.Branca,Tab));
            colocarNovaPeca('c',1,new Bispo(Cor.Branca,Tab));
            colocarNovaPeca('d',1,new Dama(Cor.Branca,Tab));
            colocarNovaPeca('e',1,new Rei(Cor.Branca,Tab, this));
            colocarNovaPeca('f',1,new Bispo(Cor.Branca,Tab));
            colocarNovaPeca('g',1,new Cavalo(Cor.Branca,Tab));
            colocarNovaPeca('h',1,new Torre(Cor.Branca,Tab));
            colocarNovaPeca('a',2,new Peao(Cor.Branca,Tab,this));
            colocarNovaPeca('b',2,new Peao(Cor.Branca,Tab,this));
            colocarNovaPeca('c',2,new Peao(Cor.Branca,Tab,this));
            colocarNovaPeca('d',2,new Peao(Cor.Branca,Tab,this));
            colocarNovaPeca('e',2,new Peao(Cor.Branca,Tab,this));
            colocarNovaPeca('f',2,new Peao(Cor.Branca,Tab,this));
            colocarNovaPeca('g',2,new Peao(Cor.Branca,Tab,this));
            colocarNovaPeca('h',2,new Peao(Cor.Branca,Tab,this));

            colocarNovaPeca('a',8,new Torre(Cor.Preta,Tab));
            colocarNovaPeca('b',8,new Cavalo(Cor.Preta,Tab));
            colocarNovaPeca('c',8,new Bispo(Cor.Preta,Tab));
            colocarNovaPeca('d',8,new Dama(Cor.Preta,Tab));
            colocarNovaPeca('e',8,new Rei(Cor.Preta,Tab, this));
            colocarNovaPeca('f',8,new Bispo(Cor.Preta,Tab));
            colocarNovaPeca('g',8,new Cavalo(Cor.Preta,Tab));
            colocarNovaPeca('h',8,new Torre(Cor.Preta,Tab));
            colocarNovaPeca('a',7,new Peao(Cor.Preta,Tab,this));
            colocarNovaPeca('b',7,new Peao(Cor.Preta,Tab,this));
            colocarNovaPeca('c',7,new Peao(Cor.Preta,Tab,this));
            colocarNovaPeca('d',7,new Peao(Cor.Preta,Tab,this));
            colocarNovaPeca('e',7,new Peao(Cor.Preta,Tab,this));
            colocarNovaPeca('f',7,new Peao(Cor.Preta,Tab,this));
            colocarNovaPeca('g',7,new Peao(Cor.Preta,Tab,this));
            colocarNovaPeca('h',7,new Peao(Cor.Preta,Tab,this));

        }
        public Peca executaMovimento(Posicao origem,Posicao destino) {
            Peca p = Tab.retirarPeca(origem);
            p.incrementarQtdMovimento();
            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p,destino);
            if(pecaCapturada != null) {
                Capturadas.Add(pecaCapturada);
            }
            // Jogada Especial roque pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemTorre = new Posicao(origem.Linha,origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha,origem.Coluna + 1);
                Peca t = Tab.retirarPeca(origemTorre);
                t.incrementarQtdMovimento();
                Tab.colocarPeca(t,destinoTorre);
            }

            // Jogada Especial roque grande
            if(p is Rei && destino.Coluna == origem.Coluna - 2) {
                Posicao origemTorre = new Posicao(origem.Linha,origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha,origem.Coluna - 1);
                Peca t = Tab.retirarPeca(origemTorre);
                t.incrementarQtdMovimento();
                Tab.colocarPeca(t,destinoTorre);
            }
            
            //jogada especial en passant
            if (p is Peao) {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null) {
                    Posicao posicaoPeao;
                    if(p.Cor == Cor.Branca) {
                        posicaoPeao = new Posicao(destino.Linha + 1,destino.Coluna);
                    }
                    else {
                        posicaoPeao = new Posicao(destino.Linha - 1,destino.Coluna);
                    }
                    pecaCapturada = Tab.retirarPeca(posicaoPeao);
                    Capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }
        private Peca rei(Cor cor) {
            foreach(Peca x in pecasEmJogo(cor)) {
                if(x is Rei) {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor) {
            Peca pecaRei = rei(cor);
            foreach(Peca x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();
                if(mat[pecaRei.Posicao.Linha,pecaRei.Posicao.Coluna] == true) {
                    return true;
                }
            }
            return false;
        }
        public void desfazMovimento(Posicao origem,Posicao destino,Peca pecaCapturada) {
            Peca p = Tab.retirarPeca(destino);
            p.decrementarQtdMovimento();
            if(pecaCapturada != null) {
                Tab.colocarPeca(pecaCapturada,destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.colocarPeca(p,origem);

            // Jogada Especial roque pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemTorre = new Posicao(origem.Linha,origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha,origem.Coluna + 1);
                Peca t = Tab.retirarPeca(destinoTorre);
                t.decrementarQtdMovimento();
                Tab.colocarPeca(t,origemTorre);
            }

            // Jogada Especial roque grande
            if(p is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemTorre = new Posicao(origem.Linha,origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha,origem.Coluna - 1);
                Peca t = Tab.retirarPeca(destinoTorre);
                t.decrementarQtdMovimento();
                Tab.colocarPeca(t,origemTorre);
            }

            //jogada especial en passant
            if(p is Peao) {
                if(origem.Coluna != destino.Coluna && pecaCapturada == vuleravelEmPassant) {
                    Peca peao = Tab.retirarPeca(destino);
                    Posicao posicaoPeao;
                    if(p.Cor == Cor.Branca) {
                        posicaoPeao = new Posicao(3,destino.Coluna);
                    }
                    else {
                        posicaoPeao = new Posicao(4,destino.Coluna);
                    }
                    Tab.colocarPeca(peao, posicaoPeao);
                }
            }
        }
        public void realizaJogada(Posicao origem,Posicao destino) {
            Peca pecaCapturada = executaMovimento(origem,destino);

            if(estaEmXeque(JogadorAtual)) {
                desfazMovimento(origem,destino,pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");

            }
            
            Peca p = Tab.peca(destino);

            //jogada especial promocao
            if (p is Peao) {
                if((p.Cor == Cor.Branca && destino.Linha==0) || (p.Cor == Cor.Preta && destino.Linha == 7)) {
                    bool result = false;
                    Console.WriteLine();
                    Console.Write("Qual peça deseja substituir seu Peão 1-Dama 2-Cavalo 3-Bispo 4-Torre: ");
                    int opcao = int.Parse(Console.ReadLine());

                    while(!result) {
                        if(opcao < 1) {
                            result = false;
                            Console.Write("Opção inválida! Informe uma das seguintes opções 1-Dama 2-Cavalo 3-Bispo 4-Torre: ");
                            opcao = int.Parse(Console.ReadLine());
                        }
                        else if(opcao > 4) {
                            result = false;
                            Console.Write("Opção inválida! Informe uma das seguintes opções 1-Dama 2-Cavalo 3-Bispo 4-Torre: ");
                            opcao = int.Parse(Console.ReadLine());
                        }
                        else
                            result = true;

                    }                    
                    p = Tab.retirarPeca(destino);
                    Pecas.Remove(p);
                    
                    switch(opcao) {
                        case 1:
                            Peca dama = new Dama(p.Cor,Tab);
                            Tab.colocarPeca(dama,destino);
                            Pecas.Add(dama);
                            break;
                        case 2:
                            Peca cavalo = new Cavalo(p.Cor,Tab);
                            Tab.colocarPeca(cavalo,destino);
                            Pecas.Add(cavalo);
                            break;
                        case 3:
                            Peca bispo = new Bispo(p.Cor,Tab);
                            Tab.colocarPeca(bispo,destino);
                            Pecas.Add(bispo);
                            break;
                        case 4:
                            Peca torre = new Torre(p.Cor,Tab);
                            Tab.colocarPeca(torre,destino);
                            Pecas.Add(torre);
                            break;
                    }
                        

                    }
                }

            if(estaEmXeque(adversaria(JogadorAtual))) {
                Xeque = true;
            }
            else {
                Xeque = false;
            }
            if(testeXequemate(adversaria(JogadorAtual))) {
                Terminada = true;
            }
            else {
                Turno++;
                mudaJogador();
            }
                        
            // jogada especial en passant
            if (p is Peao && (destino.Linha == origem.Linha-2 || destino.Linha == origem.Linha + 2)) {
                vuleravelEmPassant = p;
            }
            else {
                vuleravelEmPassant = null;
            }
        }

        public void validarPosicaoOrigem(Posicao pos) {
            if(Tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(JogadorAtual != Tab.peca(pos).Cor) {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if(!Tab.peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }
        public void validarPosicaoDestino(Posicao origem,Posicao destino) {
            if(!Tab.peca(origem).movimentoPossivel(destino)) {
                throw new TabuleiroException("Posição de destino inválido!");
            }

        }
        private Cor adversaria(Cor cor) {
            if(cor == Cor.Branca) {
                return Cor.Preta;
            }
            else {
                return Cor.Branca;
            }
        }
        private void mudaJogador() {
            if(JogadorAtual == Cor.Branca) {
                JogadorAtual = Cor.Preta;
            }
            else {
                JogadorAtual = Cor.Branca;
            }
        }
    }
}
