using System;
using System.Threading;

namespace P23_JogoDaVelha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] tabuleiro = IniciaMatriz();

            Iniciajogo(tabuleiro);

            TextoSaida();
        }
        static string[,] IniciaMatriz()

        {
            /*
            * Função que inicia a matriz e retorna ela dentro
            * da variável tabuleiro
            */

            string[,] tabuleiro = new string[3, 3] { { "[ ]", "[ ]", "[ ]" }, { "[ ]", "[ ]", "[ ]" }, { "[ ]", "[ ]", "[ ]" }, };
            return tabuleiro;
        }
        static void CabeçalhoJogo(string[] nomes = null)

        {
            /*
            * Função que imprime na tela o cabeçalho do jogo
            * com as opções e nomes dos jogadores
            */

            Console.WriteLine("===============================================================");
            Console.WriteLine("              >>>>>>> JOGO DA VELHA <<<<<<<<");
            Console.WriteLine("===============================================================");
            Console.WriteLine("    Digite 'r' para recomeçar ou 's' para sair do jogo");
            Console.WriteLine("===============================================================");
            Console.WriteLine($"   Jogador(a) {nomes[0]} será representado pela letra 'X'");
            Console.WriteLine("===============================================================");
            Console.WriteLine($"   Jogador(a) {nomes[1]} será representado pela letra 'O'");
            Console.WriteLine("===============================================================");


        }
        static string[] InsereNomeJg()
        {
            /*
             * Função que recebe o nome dos jogadores, armazena eles em um vetor
             * e retorna este vetor.
             */

            string[] jogadores = new string[2];

            Console.WriteLine("\n\n\n\t\t\t\t ========== NOMES DOS JOGADORES ==========");
            Console.Write("\n\t\t\t\t Digite o nome do jogador(a) 1: ");
            jogadores[0] = Console.ReadLine().Trim().ToUpper();

            Console.Write("\n\t\t\t\t Digite o nome do jogador(a) 2: ");
            jogadores[1] = Console.ReadLine().Trim().ToUpper();

            //RETORNA O VETOR
            return jogadores;
        }
        static void Iniciajogo(string[,] tabuleiro)
        {
            /*
             * Função que inicia o jogo e direciona o usuário para as opções que ele
             * digitar.
             */

            string status;
            int rodada = 0;
            string[] nomes = new string[2];

            status = ImprimeInicio();
            Console.Clear();

            //CONDIÇÃO CASO O JOGADOR NÃO QUEIRA SAIR DO JOGO
            if (status != "sair")
            {
                nomes = InsereNomeJg();
                TextoIniciando();
                Console.Clear();
                CabeçalhoJogo(nomes);
                ImprimeTabuleiro(tabuleiro);
            }


            //LAÇO PARA CONTAR AS RODADAS
            do
            {
                rodada++;

                //CONDIÇÃO CASO O JOGADOR NÃO QUEIRA SAIR DO JOGO
                if (status != "sair")
                    status = ExecutaRodada(tabuleiro, rodada, nomes);

                //CONDIÇÃO CASO O JOGADOR QUEIRA REINICIAR O JOGO
                if (status == "reiniciar")
                {
                    TextoReiniciando();
                    nomes = InsereNomeJg();
                    Console.Clear();
                    CabeçalhoJogo(nomes);
                    rodada = 0;
                    tabuleiro = IniciaMatriz();
                    ImprimeTabuleiro(tabuleiro);
                }

            } while (status != "sair");
        }
        static string ExecutaRodada(string[,] tabuleiro, int rodada, string[] nomes)

        {
            /*
             * Função que recebe os valores inserido pelo usuário, ela retorna 'sair' caso o usuário queira encerrar o jogo,
             * retorna 'reiniciar' caso o usuário queira reiniciar o jogo, se o usuário inserir o valor na matriz, a função verifica
             * se a posição escolhida já está com um valor, caso esteja ela solicita para o usuário escolher outra posição na matriz,
             * caso a posição esteja vazia, ela insere o valor e verifica se há algum jogador vencedor para encerrar o jogo e mostrar
             * na tela o jogador vencedor, se não tiver nenhum vencedor, a função verifica se há espaço na matriz para inserir um novo valor
             * para continuar o jogo, se não houver, ela encerra o jogo mostrando na tela como o jogo terminou.
             */
            var rand = new Random();
            string jogadaLinha;
            string jogadaColuna;
            string status;
            int jgVencedor;
            string reiniciaJg;
            
            //LAÇO PARA OS DOIS JOGADORES EFETUAREM AS JOGADAS
            for (int jogador = 1; jogador <= 2; jogador++)
            {
                //IMPRIME QUAL RODADA OS JOGADORES ESTÃO
                ImprimeRodada(rodada);
                while (true)
                {
                    //LAÇO PARA INSERIR O VALOR DA LINHA
                    do
                    {
                        Console.Write($"\nJogador {nomes[jogador-1]}, para fazer sua jogada insira primeiro o número (1 a 3) da linha da sua jogada:  ");
                        try
                        {
                            jogadaLinha = Console.ReadLine().Trim().ToLower();
                        }
                        catch
                        {
                            jogadaLinha = "0";
                        }

                        if (jogadaLinha != "1" && jogadaLinha != "2" && jogadaLinha != "3" && jogadaLinha != "r" && jogadaLinha != "s")
                        {
                            Console.WriteLine("Posição da linha inválida!");
                        }
                    } while (jogadaLinha != "1" && jogadaLinha != "2" && jogadaLinha != "3" && jogadaLinha != "r" && jogadaLinha != "s");

                    //CONDIÇÃO CASO O JOGADOR QUEIRA REINICIAR NO MOMENTO DE INSERIR A POSIÇÃO LINHA
                    if (jogadaLinha == "r")
                        return "reiniciar";

                    //CONDIÇÃO CASO O JOGADOR QUEIRA SAIR NO MOMENTO DE INSERIR A POSIÇÃO LINHA
                    if (jogadaLinha == "s")
                        return "sair";

                    //LAÇO PARA INSERIR O VALOR DA COLUNA
                    do
                    {
                        Console.Write($"\nJogador {nomes[jogador-1]}, agora insira o número da coluna (1 a 3) para concluir esta jogada:  ");
                        try
                        {
                            jogadaColuna = Console.ReadLine().Trim().ToLower();
                        }
                        catch
                        {
                            jogadaColuna = "0";
                        }

                        if (jogadaColuna != "1" && jogadaColuna != "2" && jogadaColuna != "3" && jogadaColuna != "r" && jogadaColuna != "s")
                        {
                            Console.WriteLine("\nPosição da linha inválida!");
                        }
                    } while (jogadaColuna != "1" && jogadaColuna != "2" && jogadaColuna != "3" && jogadaColuna != "r" && jogadaColuna != "s");

                    //CONDIÇÃO CASO O JOGADOR QUEIRA REINICIAR NO MOMENTO DE INSERIR A POSIÇÃO COLUNA
                    if (jogadaColuna == "r")
                        return "reiniciar";

                    //CONDIÇÃO CASO O JOGADOR QUEIRA SAIR NO MOMENTO DE INSERIR A POSIÇÃO COLUNA
                    if (jogadaColuna == "s")
                        return "sair";

                    //IF PARA VERIFICAR SE A POSIÇÃO ESCOLHIDA DA JOGADA NÃO ESTÁ VAZIA
                    if (tabuleiro[int.Parse(jogadaLinha) - 1, int.Parse(jogadaColuna) - 1] != "[ ]")
                    {
                        Console.WriteLine("\nNesta posição a jogada ja foi realizada, tente a jogada novamente!");
                    }
                    else
                    {
                        // VERIFICA QUAL JOGADOR ESTA FAZENDO A JOGADA
                        VerificaJogador(jogador, tabuleiro, jogadaLinha, jogadaColuna);

                        //VERIFICA SE ALGUM JOGADOR VENCEU
                        jgVencedor = VerifVitJG(tabuleiro);
                        if (jgVencedor != 0)
                        {
                            Console.Clear();
                            CabeçalhoJogo(nomes);
                            ImprimeTabuleiro(tabuleiro);
                            ImprimeVencedor(jgVencedor, nomes);
                            reiniciaJg = ReiniciaJogo();
                            return reiniciaJg;
                        }
                        break;
                    }
                }
                Console.Clear();
                CabeçalhoJogo(nomes);
                ImprimeTabuleiro(tabuleiro);

                //VERIFICA SE O TABULEIRO ESTÁ COMPLETO
                status = VerificaTabuleiro(tabuleiro, rodada);

                //IF PARA SAIR DO JOGO 
                if (status == "sair")
                    return "sair";

                // IF PARA REINICIAR O JOGO
                if (status == "reiniciar")
                    return "reiniciar";
            }
            //RETORNO PARA CONTINUAR FAZENDO AS JOGADAS
            return "continua";
        }
        static void VerificaJogador(int jogador, string[,] tabuleiro,
           string jogadaLinha, string jogadaColuna)
        {
            /*
             * Função que verifica qual jogador esta fazendo a jogada, depois
             * ela insere o valor que representa o usuário na posição escolhida
             * pelo usuário.
             */

            //IF PARA VERIFICAR QUAL É O JOGADOR QUE ESTÁ FAZENDO A JOGADA E DEPOIS INSERE O VALOR NA MATRIZ
            if (jogador == 1)
            {
                tabuleiro[int.Parse(jogadaLinha) - 1, int.Parse(jogadaColuna) - 1] = "[X]";
                ImprimeTabuleiro(tabuleiro);
            }
            else
            {
                tabuleiro[int.Parse(jogadaLinha) - 1, int.Parse(jogadaColuna) - 1] = "[O]";
                ImprimeTabuleiro(tabuleiro);
            }
        }
        static string ReiniciaJogo()
        {
            /*
             * Função que verifica se usuário quer reiniciar o jogo,
             * caso ele queira, ela retorna 'reiniciar1, senão retorna 'sair'.
             */

            string reiniciaJg;

            //LAÇO QUE VERIFICA SE O USUÁRIO QUER JOGAR NOVAMENTE
            do
            {
                Console.Write("\n\t\tDeseja jogar novamente(S/N)?: ");
                reiniciaJg = Console.ReadLine().Trim().ToLower();

                if (reiniciaJg != "s" && reiniciaJg != "n")
                {
                    Console.Clear();
                    Console.Write("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t   OPÇÃO INVÁLIDA !!!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
            } while (reiniciaJg != "s" && reiniciaJg != "n");

            //RETORNO PARA REINICIAR O JOGO
            if (reiniciaJg == "s")
                return "reiniciar";

            //RETORNO PARA SAIR DO JOGO
            else
                return "sair";

        }
        static string VerificaTabuleiro(string[,] tabuleiro, int rodada)

        {
            /*
             * Função para verificar se o tabuleiro está vazio para continuar o jogo,
             * caso não esteja, ela imprime uma mensagem na tela
             */

            int cont = 0;
            string op;
            //FOR PARA PERCORRER TODA A MATRIZ
            for(int linha = 0; linha < 3; linha++)
            {
                for(int coluna = 0; coluna < 3; coluna++)
                {
                    //VERIFICA SE ALGUMA POSICÃO ESTA VAZIA
                    if (tabuleiro[linha, coluna] != "[O]" && tabuleiro[linha, coluna] != "[X]")
                    {
                        cont++;
                    }
                }
            }
            //CONDIÇÃO CASO O TABULEIRO ESTEJA COMPLETO RETORNA FALSE
            if (cont != 0)
                return "continua";
            else
            {
                Console.WriteLine("\n\t\t==============================");
                Console.WriteLine("\t\t  DEU VELHA, FIM DE JOGO!!!!");
                Console.WriteLine("\t\t==============================");
                ImprimeRodada(rodada);
                op = ReiniciaJogo();
                return op;
            }
                
        }
        static int VerifVitJG(string[,] tabuleiro)
        {
            /*
             * Esta função verifica a lógica do jogo para encontrar 
             * algum vencedor, ela retorna (1) caso o jogador 1 ganhe,
             * retorna (-1) para caso o jogador 2 ganhe e retorna (0)
             * caso ainda não tenha um vencedor
             */

            string jogador1 = "[X]";
            string jogador2 = "[O]";

            //CONDIÇÃO PARA VERIFICAR SE O JOGADOR 1 VENCEU
            if (String.Equals(tabuleiro[0,0], tabuleiro[0,1]) && String.Equals(tabuleiro[0,0], tabuleiro[0,2]) && String.Equals(tabuleiro[0,0], jogador1)
                || String.Equals(tabuleiro[1,0], tabuleiro[1,1]) && String.Equals(tabuleiro[1,0], tabuleiro[1,2]) && String.Equals(tabuleiro[1,0], jogador1)
                || String.Equals(tabuleiro[2,0], tabuleiro[2,1]) && String.Equals(tabuleiro[2,0], tabuleiro[2,2]) && String.Equals(tabuleiro[2,0], jogador1)
                || String.Equals(tabuleiro[0,0], tabuleiro[1,0]) && String.Equals(tabuleiro[0,0], tabuleiro[2,0]) && String.Equals(tabuleiro[0,0], jogador1)
                || String.Equals(tabuleiro[0,1], tabuleiro[1,1]) && String.Equals(tabuleiro[0,1], tabuleiro[2,1]) && String.Equals(tabuleiro[0,1], jogador1)
                || String.Equals(tabuleiro[0,2], tabuleiro[1,2]) && String.Equals(tabuleiro[0,2], tabuleiro[2,2]) && String.Equals(tabuleiro[0,2], jogador1)
                || String.Equals(tabuleiro[0,0], tabuleiro[1,1]) && String.Equals(tabuleiro[0,0], tabuleiro[2,2]) && String.Equals(tabuleiro[0,0], jogador1)
                || String.Equals(tabuleiro[2,0], tabuleiro[1,1]) && String.Equals(tabuleiro[2,0], tabuleiro[0,2]) && String.Equals(tabuleiro[2,0], jogador1))
            {
                 return 1;
            }
            //CONDIÇÃO PARA VERIFICAR SE O JOGADOR 2 VENCEU
            else if (String.Equals(tabuleiro[0, 0], tabuleiro[0, 1]) && String.Equals(tabuleiro[0, 0], tabuleiro[0, 2]) && String.Equals(tabuleiro[0, 0], jogador2)
               || String.Equals(tabuleiro[1, 0], tabuleiro[1, 1]) && String.Equals(tabuleiro[1, 0], tabuleiro[1,2]) && String.Equals(tabuleiro[1, 0], jogador2)
               || String.Equals(tabuleiro[2, 0], tabuleiro[2, 1]) && String.Equals(tabuleiro[2, 0], tabuleiro[2,2]) && String.Equals(tabuleiro[2, 0], jogador2)
               || String.Equals(tabuleiro[0, 0], tabuleiro[1, 0]) && String.Equals(tabuleiro[0, 0], tabuleiro[2,0]) && String.Equals(tabuleiro[0, 0], jogador2)
               || String.Equals(tabuleiro[0, 1], tabuleiro[1, 1]) && String.Equals(tabuleiro[0, 1], tabuleiro[2,1]) && String.Equals(tabuleiro[0, 1], jogador2)
               || String.Equals(tabuleiro[0, 2], tabuleiro[1, 2]) && String.Equals(tabuleiro[0, 2], tabuleiro[2,2]) && String.Equals(tabuleiro[0, 2], jogador2)
               || String.Equals(tabuleiro[0, 0], tabuleiro[1, 1]) && String.Equals(tabuleiro[0, 0], tabuleiro[2,2]) && String.Equals(tabuleiro[0, 0], jogador2)
               || String.Equals(tabuleiro[2, 0], tabuleiro[1, 1]) && String.Equals(tabuleiro[2, 0], tabuleiro[0,2]) && String.Equals(tabuleiro[2, 0], jogador2))
            {
                return -1;
            }
            //ENTRA NO ELSE CASO NINHUEM TENHA VENCIDO AINDA
            else
                return 0;
        }
        static void ImprimeTabuleiro(string[,] tabuleiro)

        {
            /*
             * Função para imprimir o tabuleiro foramatado na tela
             */

            Console.WriteLine("\n\t\t\t\tColunas");
            Console.WriteLine("\t\t\t\t1  2  3");

            //LAÇOS PARA FORMATAR O TABULEIRO
            for (int linha = 0; linha < 3; linha++)
            {
                for (int coluna = 0; coluna < 3; coluna++)
                {
                    if (coluna == 0)
                    {
                        Console.Write($"\t\t\tLinha{linha + 1} {tabuleiro[linha, coluna]}");
                    }
                    else
                    {
                        Console.Write($"{tabuleiro[linha, coluna]}");
                    }

                }
                Console.WriteLine();
            }



        }
        static void ImprimeVencedor(int jgVencedor, string[] nomes)
        {
            /*
             * Função que recebe um vaor int para imprimir na tela o
             * vencedor, Jogador 1 (int 1) e jogador 2 (int -1)
             */

            //VERIFICAÇÃO PARA CASO ALGUM JOGADOR GANHAR, ELE IMPRIME NA TELA O VENCEDOR
            if (jgVencedor == 1)
            {
                Console.WriteLine("\n\t\t=========================================");
                Console.WriteLine($"\t\t JOGADOR {nomes[0]} VENCEU, PARABÉNS!!!!!");
                Console.WriteLine("\t\t=========================================");
            }

            else if (jgVencedor == -1)
            {
                Console.WriteLine("\n\t\t=========================================");
                Console.WriteLine($"\t\t JOGADOR {nomes[1]} VENCEU, PARABÉNS!!!!!");
                Console.WriteLine("\t\t=========================================");
            }
        }
        static void ImprimeRodada(int rodada)
        {
            /*
             * Função para imprimir na tela a rodada do jogo
             */

            //IF PARA NÃO IMPRIMIR RODADA IGUAL A 0 NA TELA
            if (rodada != 0)
            {
                Console.WriteLine("==========");
                Console.WriteLine($"RODADA: #{rodada}");
                Console.WriteLine("==========");
            }

        }
        static string ImprimeInicio()
        {
            /*
             * Função para imprimir o inicio do jogo e ela retorna 'iniciar' caso o usuário queira começar
             * o jogo, e retorna 'sair caso ele não queira jogar e sair do programa.
             */

            string op;
            do
            {
                Console.WriteLine("\n\t\t\t===============================================================");
                Console.WriteLine("\t\t\t                >>>>>>> JOGO DA VELHA <<<<<<<<");
                Console.WriteLine("\t\t\t===============================================================");

                Console.Write("\t\t\t\t\tDeseja iniciar o jogo (S/N)?: ");
                op = Console.ReadLine().Trim().ToLower();

                //IF PARA MOSTRAR NA TELA A OPÇÃO INVALIDA CASO O USUÁRIO NÃO DIGITE 's' OU 'n' 
                if (op != "s" && op != "n")
                {
                    Console.Clear();
                    Console.Write("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t   OPÇÃO INVÁLIDA !!!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
            } while (op != "s" && op != "n");

            //RETORNA SAIR PARA ENCERRAR O JOGO
            if (op == "n")
                return "sair";

            //RETORNA INICIAR PARA COMEÇAR O JOGO
            return "iniciar";
        }
        static void TextoSaida()
        {
            /*
             * Função para imprimir na tela que o usuário está saindo do jogo
             */

            Console.Clear();
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t   SAINDO DO JOGO");
            Console.Write(". ");
            Thread.Sleep(800);
            Console.Write(". ");
            Thread.Sleep(800);
            Console.Write(". ");
            Thread.Sleep(800);
            Console.Clear();
        }
        static void TextoIniciando()
        {
            /*
             * Função que imprime na tela que o jogo está iniciando.
             */
            Console.Clear();
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t   INICIANDO O JOGO");
            Console.Write(". ");
            Thread.Sleep(800);
            Console.Write(". ");
            Thread.Sleep(800);
            Console.Write(". ");
            Thread.Sleep(800);
            Console.Clear();

        }
        static void TextoReiniciando()
        {
            /*
            * Função que imprime na tela que o jogo está reiniciando.
            */

            Console.Clear();
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t   REINICIANDO O JOGO");
            Console.Write(". ");
            Thread.Sleep(800);
            Console.Write(". ");
            Thread.Sleep(800);
            Console.Write(". ");
            Thread.Sleep(800);
            Console.Clear();

        }
    }
}
