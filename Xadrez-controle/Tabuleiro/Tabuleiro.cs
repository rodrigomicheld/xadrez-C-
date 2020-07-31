using System;
using System.Collections.Generic;
using System.Text;

namespace Tabuleiro {
    class Tabuleiro {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas, Peca[,] pecas) {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }
    }
}
