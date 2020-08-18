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
                    Console.Clear();
                    Tela.imprimirTabuleiro(partidaXadrez.Tab);
                    
                    Console.WriteLine( );
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicao().toPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicao().toPosicao();

                    partidaXadrez.executaMovimento(origem, destino);
                }
                
                
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

        }
    }
}
