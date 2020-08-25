using System;
using tabuleiro;
using xadrez;

namespace Xadrez_controle {
    class Program {
        static void Main(string[] args) {
            try
            {
                PartidaDeXadrez partidaXadrez = new PartidaDeXadrez();

                while (!partidaXadrez.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partidaXadrez);
                            
                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicao().toPosicao();

                        partidaXadrez.validarPosicaoOrigem(origem);

                        bool[,] possicoesPossiveis = partidaXadrez.Tab.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.imprimirTabuleiro(partidaXadrez.Tab, possicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicao().toPosicao();

                        partidaXadrez.validarPosicaoDestino(origem,destino);

                        partidaXadrez.realizaJogada(origem, destino);

                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }


                }
                Console.Clear();
                Tela.imprimirPartida(partidaXadrez);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

        }
    }
}
